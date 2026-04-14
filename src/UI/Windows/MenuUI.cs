using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MalumMenu;

public class MenuUI : MonoBehaviour
{
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

        _windowBgTex  = MakeSolid(0.078f, 0.078f, 0.078f);
        _sidebarTex   = MakeSolid(0.090f, 0.090f, 0.090f);
        _accentTex    = MakeSolid(0.10f,  0.45f,  1.00f);
        _separatorTex = MakeSolid(0.10f,  0.45f,  1.00f, 0.4f);
    }

    private void Update()
    {
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

        if (CheatToggles.saveProfile) { CheatToggles.saveProfile = false; CheatToggles.SaveTogglesToProfile(); }
        if (CheatToggles.loadProfile) { CheatToggles.LoadTogglesFromProfile(); CheatToggles.loadProfile = false; }

        if (!Utils.isPlayer)
        {
            CheatToggles.setFakeRole = false; CheatToggles.killAll = false;
            CheatToggles.telekillPlayer = false; CheatToggles.killAllCrew = false;
            CheatToggles.killAllImps = false; CheatToggles.teleportPlayer = false;
            CheatToggles.spectate = false; CheatToggles.freecam = false;
            CheatToggles.killPlayer = false; CheatToggles.fakeRevive = false;
            CheatToggles.callMeeting = false;
        }
        if (!Utils.isShip)
        {
            CheatToggles.sabotageMap = false; CheatToggles.unfixableLights = false;
            CheatToggles.completeMyTasks = false; CheatToggles.kickVents = false;
            CheatToggles.reportBody = false; CheatToggles.closeMeeting = false;
            CheatToggles.reactorSab = false; CheatToggles.oxygenSab = false;
            CheatToggles.commsSab = false; CheatToggles.elecSab = false;
            CheatToggles.mushSab = false; CheatToggles.closeAllDoors = false;
            CheatToggles.openAllDoors = false; CheatToggles.spamCloseAllDoors = false;
            CheatToggles.spamOpenAllDoors = false; CheatToggles.mushSpore = false;
            MalumCheats.StopShipAnimCheats();
        }
        if (!Utils.isHost && !Utils.isFreePlay)
        {
            CheatToggles.killAll = false; CheatToggles.telekillPlayer = false;
            CheatToggles.killAllCrew = false; CheatToggles.killAllImps = false;
            CheatToggles.killPlayer = false; CheatToggles.ejectPlayer = false;
            CheatToggles.noKillCd = false; CheatToggles.killAnyone = false;
            CheatToggles.killVanished = false; CheatToggles.forceStartGame = false;
            CheatToggles.skipMeeting = false; CheatToggles.voteImmune = false;
            CheatToggles.noGameEnd = false; CheatToggles.showProtectMenu = false;
            CheatToggles.showRolesMenu = false; CheatToggles.noOptionsLimits = false;
        }
        if (!Utils.isMeeting)
        {
            CheatToggles.skipMeeting = false;
            CheatToggles.ejectPlayer = false;
        }
    }

    public void OnGUI()
    {
        if (!isGUIActive || MalumMenu.isPanicked) return;

        GUIStylePreset.ApplyToSkin();

        if (CheatToggles.rgbMode)
            GUI.backgroundColor = Color.HSVToRGB(hue, 0.85f, 1f);
        else
            GUI.backgroundColor = GUIStylePreset.AccentBlue;

        // Wrap in try-catch to suppress unstripping errors
        try
        {
            _windowRect = GUI.Window(
                (int)WindowId.MenuUI,
                _windowRect,
                (System.Action<int>)DrawWindow,
                new GUIContent("  MalumMenu  v" + MalumMenu.malumVersion),
                GUIStylePreset.WindowStyle
            );
        }
        catch (System.NotSupportedException) 
        { 
            // Silently ignore IL2CPP unstripping errors
        }
    }

    // Mark this method to prevent IL2CPP from trying to strip it
    [Il2CppInterop.Runtime.Attributes.HideFromIl2Cpp]
    private void DrawWindow(int id)
    {
        // 2-px blue accent bar at top
        GUI.DrawTexture(new Rect(0f, 0f, windowWidth, 2f), _accentTex);
        // Full window background
        GUI.DrawTexture(new Rect(0f, 2f, windowWidth, windowHeight), _windowBgTex);

        GUILayout.BeginHorizontal();

        // ── Sidebar ───────────────────────────────────────────────
        float sw = windowWidth * 0.20f;
        GUILayout.BeginVertical(GUILayout.Width(sw));
        GUI.DrawTexture(new Rect(0f, 2f, sw, windowHeight), _sidebarTex);

        GUILayout.Space(6f);
        for (int i = 0; i < _tabs.Count; i++)
        {
            bool active = (_selectedTab == i);

            // Blue left-edge indicator for active tab
            if (active)
                GUI.DrawTexture(new Rect(0f, GUILayoutUtility.GetLastRect().yMax, 3f, 32f), _accentTex);

            GUIStyle style = active ? GUIStylePreset.TabButtonActive : GUIStylePreset.TabButton;
            if (GUILayout.Button(_tabs[i].name, style, GUILayout.Height(32)))
                _selectedTab = i;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        // ── Thin blue separator ───────────────────────────────────
        GUILayout.Box("", GUIStylePreset.Separator, GUILayout.Width(1f), GUILayout.ExpandHeight(true));
        GUILayout.Space(8f);

        // ── Content ───────────────────────────────────────────────
        GUILayout.BeginVertical(GUILayout.Width(windowWidth * 0.80f - 18f));
        GUILayout.Space(4f);

        if (_selectedTab >= 0 && _selectedTab < _tabs.Count)
        {
            GUILayout.Label(_tabs[_selectedTab].name, GUIStylePreset.TabTitle);
            GUILayout.Box("", GUIStylePreset.Separator, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
            GUILayout.Space(4f);
            _tabs[_selectedTab].Draw();
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUI.DragWindow();
    }
}