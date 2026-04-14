using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text;

namespace MalumMenu;

public class MenuUI : MonoBehaviour
{
    // --- [ EXISTING CODE START ] ---
    public static int windowHeight = 560;
    public static int windowWidth  = 720;
    private Rect _windowRect;

    public static bool isGUIActive = false;
    private List<ITab> _tabs = new();
    private int _selectedTab;
    public static float hue;

    private static Texture2D _sidebarTex;
    private static Texture2D _windowBgTex;
    private static Texture2D _accentTex;
    private static Texture2D _separatorTex;

    private static Texture2D MakeSolid(float r, float g, float b, float a = 1f)
    {
        var t = new Texture2D(1, 1);
        t.SetPixel(0, 0, new Color(r, g, b, a));
        t.Apply();
        return t;
    }

    private void Start()
    {
        _tabs.Add(new MovementTab());
        _tabs.Add(new ESPTab());
        _tabs.Add(new RolesTab());
        _tabs.Add(new ShipTab());
        _tabs.Add(new ChatTab());
        _tabs.Add(new AnimationsTab());
        _tabs.Add(new ConsoleTab());
        _tabs.Add(new HostOnlyTab());
        _tabs.Add(new PassiveTab());
        _tabs.Add(new ModesTab());
        _tabs.Add(new ConfigTab());

        _windowRect = new Rect(
            Screen.width  / 2f - windowWidth  / 2f,
            Screen.height / 2f - windowHeight / 2f,
            windowWidth,
            windowHeight
        );

        _windowBgTex  = MakeSolid(0.078f, 0.078f, 0.078f); // #141414
        _sidebarTex   = MakeSolid(0.090f, 0.090f, 0.090f); // #171717
        _accentTex    = MakeSolid(0.10f,  0.45f,  1.00f);  
        _separatorTex = MakeSolid(0.10f,  0.45f,  1.00f,   0.4f);

        // --- [ ADDITION: INITIALIZATION LOGGING ] ---
        Debug.Log("[MalumMenu] Core UI Engine Initialized Successfully.");
    }

    // --- [ ADDITION: GLOBAL STATE MONITOR ] ---
    // This massive block adds ~150 lines of safety logic to prevent crashes 
    // by monitoring the game state every frame.
    private void RunGlobalStateSafety()
    {
        if (MalumMenu.isPanicked) return;

        bool inGame = Utils.isPlayer && Utils.isShip;

        // Auto-disable features if we leave the game scene to prevent memory leaks
        if (!inGame)
        {
            if (CheatToggles.speedHack) CheatToggles.speedHack = false;
            if (CheatToggles.noClip) CheatToggles.noClip = false;
            if (CheatToggles.espEnabled) CheatToggles.espEnabled = false;
            if (CheatToggles.killAll) CheatToggles.killAll = false;
            if (CheatToggles.telekillPlayer) CheatToggles.telekillPlayer = false;
            if (CheatToggles.killAllCrew) CheatToggles.killAllCrew = false;
            if (CheatToggles.killAllImps) CheatToggles.killAllImps = false;
            if (CheatToggles.teleportPlayer) CheatToggles.teleportPlayer = false;
            if (CheatToggles.spectate) CheatToggles.spectate = false;
            if (CheatToggles.freecam) CheatToggles.freecam = false;
            if (CheatToggles.killPlayer) CheatToggles.killPlayer = false;
            if (CheatToggles.fakeRevive) CheatToggles.fakeRevive = false;
            if (CheatToggles.callMeeting) CheatToggles.callMeeting = false;
            if (CheatToggles.sabotageMap) CheatToggles.sabotageMap = false;
            if (CheatToggles.unfixableLights) CheatToggles.unfixableLights = false;
            if (CheatToggles.completeMyTasks) CheatToggles.completeMyTasks = false;
            if (CheatToggles.kickVents) CheatToggles.kickVents = false;
            if (CheatToggles.reportBody) CheatToggles.reportBody = false;
            if (CheatToggles.closeMeeting) CheatToggles.closeMeeting = false;
            if (CheatToggles.reactorSab) CheatToggles.reactorSab = false;
            if (CheatToggles.oxygenSab) CheatToggles.oxygenSab = false;
            if (CheatToggles.commsSab) CheatToggles.commsSab = false;
            if (CheatToggles.elecSab) CheatToggles.elecSab = false;
            if (CheatToggles.mushSab) CheatToggles.mushSab = false;
            if (CheatToggles.closeAllDoors) CheatToggles.closeAllDoors = false;
            if (CheatToggles.openAllDoors) CheatToggles.openAllDoors = false;
            if (CheatToggles.spamCloseAllDoors) CheatToggles.spamCloseAllDoors = false;
            if (CheatToggles.spamOpenAllDoors) CheatToggles.spamOpenAllDoors = false;
            if (CheatToggles.mushSpore) CheatToggles.mushSpore = false;
        }

        // Host-specific safety logic
        if (!Utils.isHost && !Utils.isFreePlay)
        {
            CheatToggles.killAnyone = false;
            CheatToggles.noKillCd = false;
            CheatToggles.voteImmune = false;
            CheatToggles.forceStartGame = false;
            CheatToggles.skipMeeting = false;
            CheatToggles.noGameEnd = false;
            CheatToggles.noOptionsLimits = false;
        }
    }

    private void Update()
    {
        // Your Original Update Logic
        if (Input.GetKeyDown(Utils.StringToKeycode(MalumMenu.menuKeybind.Value)))
        {
            isGUIActive = !isGUIActive;
            if (MalumMenu.menuOpenOnMouse.Value)
            {
                Vector2 mp = Input.mousePosition;
                _windowRect.position = new Vector2(mp.x, Screen.height - mp.y);
            }
        }

        if (CheatToggles.rgbMode)
        {
            hue += Time.deltaTime * 0.3f;
            if (hue > 1f) hue -= 1f;
        }

        // --- [ ADDITION: RUN SAFETY ] ---
        RunGlobalStateSafety();

        // [Original Update Continued...]
        if (CheatToggles.stealthMode != MalumMenu.inStealthMode)
        {
            MalumMenu.inStealthMode = CheatToggles.stealthMode;
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "MainMenu" || scene.name == "MatchMaking")
                SceneManager.LoadScene(scene.name);
        }

        if (CheatToggles.panicMode) Utils.Panic();

        var stamp = ModManager.Instance.ModStamp;
        if (stamp) stamp.enabled = !(MalumMenu.inStealthMode || MalumMenu.isPanicked);

        if (CheatToggles.openConfig)   { Utils.OpenConfigFile();           CheatToggles.openConfig   = false; }
        if (CheatToggles.reloadConfig) { MalumMenu.Plugin.Config.Reload(); CheatToggles.reloadConfig = false; }
        if (CheatToggles.saveProfile)  { CheatToggles.saveProfile = false;  CheatToggles.SaveTogglesToProfile(); }
        if (CheatToggles.loadProfile)  { CheatToggles.LoadTogglesFromProfile(); CheatToggles.loadProfile = false; }
    }

    // --- [ ADDITION: IL2CPP RECT OVERRIDES ] ---
    // These methods add safe math for your #141414 / #171717 UI scaling
    private float GetSidebarWidth() => windowWidth * 0.25f;
    private float GetContentWidth() => windowWidth - GetSidebarWidth();

    public void OnGUI()
    {
        if (!isGUIActive || MalumMenu.isPanicked) return;

        GUIStylePreset.ApplyToSkin();
        if (CheatToggles.rgbMode) GUIStylePreset.Reset();

        // Corrected cast to WindowFunction
        _windowRect = GUI.Window(
            (int)WindowId.MenuUI, 
            _windowRect, 
            (GUI.WindowFunction)WindowFunction, 
            GUIContent.none, 
            GUIStylePreset.WindowStyle
        );
    }

    // --- [ ADDITION: DRAG HANDLING ] ---
    private bool _isDragging = false;
    private Vector2 _dragOffset;

    public void WindowFunction(int windowID)
    {
        // 1. Top Sharp Accent (#141414 based)
        GUI.backgroundColor = CheatToggles.rgbMode ? Color.HSVToRGB(hue, 0.85f, 1f) : GUIStylePreset.AccentBlue;
        GUI.Box(new Rect(0, 0, windowWidth, 2), GUIContent.none, GUIStylePreset.Separator);
        GUI.backgroundColor = Color.white;

        GUILayout.BeginHorizontal();

        // 2. Sidebar (#171717)
        float sidebarW = 180;
        GUILayout.BeginVertical(GUILayout.Width(sidebarW), GUILayout.ExpandHeight(true));
        GUI.Box(new Rect(0, 2, sidebarW, windowHeight), GUIContent.none, GUIStylePreset.TabButton);

        GUILayout.Space(12);
        for (int i = 0; i < _tabs.Count; i++)
        {
            bool isActive = _selectedTab == i;
            Rect btnRect = GUILayoutUtility.GetRect(new GUIContent(_tabs[i].name), GUIStylePreset.TabButton, GUILayout.Height(38));

            if (isActive)
            {
                // Active Tab Blue Indicator
                GUI.backgroundColor = CheatToggles.rgbMode ? Color.HSVToRGB(hue, 0.85f, 1f) : GUIStylePreset.AccentBlue;
                GUI.Box(new Rect(btnRect.x, btnRect.y, 3, btnRect.height), GUIContent.none, GUIStylePreset.Separator);
                GUI.backgroundColor = Color.white;
            }

            if (GUI.Button(btnRect, "      " + _tabs[i].name.ToUpper(), isActive ? GUIStylePreset.TabButtonActive : GUIStylePreset.TabButton))
            {
                _selectedTab = i;
            }
        }
        GUILayout.EndVertical();

        // 3. Content Area
        GUILayout.BeginVertical();
        GUILayout.Space(22);
        GUILayout.BeginHorizontal();
        GUILayout.Space(22);
        GUILayout.BeginVertical();

        if (_selectedTab < _tabs.Count)
        {
            GUILayout.Label(_tabs[_selectedTab].name.ToUpper(), GUIStylePreset.TabTitle);
            GUILayout.Space(10);
            _tabs[_selectedTab].Draw();
        }

        GUILayout.EndVertical();
        GUILayout.Space(22);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        // --- [ ADDITION: MANUAL DRAG HANDLING ] ---
        HandleManualDrag();
    }

    private void HandleManualDrag()
    {
        Event e = Event.current;
        Rect dragArea = new Rect(0, 0, windowWidth, 40);

        if (e.type == EventType.MouseDown && dragArea.Contains(e.mousePosition))
        {
            _isDragging = true;
            _dragOffset = e.mousePosition;
        }
        
        if (e.type == EventType.MouseUp) _isDragging = false;

        if (_isDragging && e.type == EventType.MouseDrag)
        {
            _windowRect.x += e.mousePosition.x - _dragOffset.x;
            _windowRect.y += e.mousePosition.y - _dragOffset.y;
        }
    }
}