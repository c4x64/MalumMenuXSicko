using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MalumMenu;

public class MenuUI : MonoBehaviour
{
    public static int windowHeight = 560;
    public static int windowWidth = 720;
    private Rect _windowRect;

    public static bool isGUIActive = false;
    private List<ITab> _tabs = new();
    private int _selectedTab;
    public static float hue;

    // Pre-built textures - built ONCE in Start, never in WindowFunction
    private static Texture2D _sidebarTex;
    private static Texture2D _windowBgTex;
    private static Texture2D _accentTex;
    private static Texture2D _accentTexCached;
    private static Color     _lastAccentColor;

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

        _windowRect = new(
            Screen.width / 2f - windowWidth / 2f,
            Screen.height / 2f - windowHeight / 2f,
            windowWidth,
            windowHeight
        );

        // Build ALL textures here, never inside WindowFunction
        _sidebarTex  = GUIStylePreset.MakeTex(1, 1, GUIStylePreset.BgSidebar);
        _windowBgTex = GUIStylePreset.MakeTex(1, 1, GUIStylePreset.BgBase);
        _accentTex   = GUIStylePreset.MakeTex(1, 1, GUIStylePreset.AccentBlue);
        
        // Cache initial accent
        _accentTexCached = _accentTex;
        _lastAccentColor = GUIStylePreset.AccentBlue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Utils.StringToKeycode(MalumMenu.menuKeybind.Value)))
        {
            isGUIActive = !isGUIActive;
            if (MalumMenu.menuOpenOnMouse.Value)
            {
                Vector2 mousePosition = Input.mousePosition;
                _windowRect.position = new Vector2(mousePosition.x, Screen.height - mousePosition.y);
            }
        }
        if (CheatToggles.rgbMode) { hue = (hue + Time.deltaTime * 0.3f) % 1f; }
        if (CheatToggles.stealthMode != MalumMenu.inStealthMode)
        {
            MalumMenu.inStealthMode = CheatToggles.stealthMode;
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "MainMenu" || scene.name == "MatchMaking") { SceneManager.LoadScene(scene.name); }
        }
        if (CheatToggles.panicMode) Utils.Panic();
        var stamp = ModManager.Instance.ModStamp;
        if (stamp) stamp.enabled = !(MalumMenu.inStealthMode || MalumMenu.isPanicked);
        if (CheatToggles.openConfig) { Utils.OpenConfigFile(); CheatToggles.openConfig = false; }
        if (CheatToggles.reloadConfig) { MalumMenu.Plugin.Config.Reload(); CheatToggles.reloadConfig = false; }
        if (CheatToggles.saveProfile) { CheatToggles.saveProfile = false; CheatToggles.SaveTogglesToProfile(); }
        if (CheatToggles.loadProfile) { CheatToggles.LoadTogglesFromProfile(); CheatToggles.loadProfile = false; }
        if (!Utils.isPlayer) { CheatToggles.setFakeRole = false; CheatToggles.killAll = false; CheatToggles.telekillPlayer = false; CheatToggles.killAllCrew = false; CheatToggles.killAllImps = false; CheatToggles.teleportPlayer = false; CheatToggles.spectate = false; CheatToggles.freecam = false; CheatToggles.killPlayer = false; CheatToggles.fakeRevive = false; CheatToggles.callMeeting = false; }
        if (!Utils.isShip) { CheatToggles.sabotageMap = false; CheatToggles.unfixableLights = false; CheatToggles.completeMyTasks = false; CheatToggles.kickVents = false; CheatToggles.reportBody = false; CheatToggles.closeMeeting = false; CheatToggles.reactorSab = false; CheatToggles.oxygenSab = false; CheatToggles.commsSab = false; CheatToggles.elecSab = false; CheatToggles.mushSab = false; CheatToggles.closeAllDoors = false; CheatToggles.openAllDoors = false; CheatToggles.spamCloseAllDoors = false; CheatToggles.spamOpenAllDoors = false; CheatToggles.mushSpore = false; MalumCheats.StopShipAnimCheats(); }
        if (!Utils.isHost && !Utils.isFreePlay) { CheatToggles.killAll = false; CheatToggles.telekillPlayer = false; CheatToggles.killAllCrew = false; CheatToggles.killAllImps = false; CheatToggles.killPlayer = false; CheatToggles.ejectPlayer = false; CheatToggles.noKillCd = false; CheatToggles.killAnyone = false; CheatToggles.killVanished = false; CheatToggles.forceStartGame = false; CheatToggles.skipMeeting = false; CheatToggles.voteImmune = false; CheatToggles.noGameEnd = false; CheatToggles.showProtectMenu = false; CheatToggles.showRolesMenu = false; CheatToggles.noOptionsLimits = false; }
        if (!Utils.isMeeting) { CheatToggles.skipMeeting = false; CheatToggles.ejectPlayer = false; }

        // ✅ Update accent texture in Update, NOT in WindowFunction
        UpdateAccentTexture();
    }

    // ✅ Update the accent texture outside of any GUI callback
    private void UpdateAccentTexture()
    {
        Color newColor;
        if (CheatToggles.rgbMode)
        {
            newColor = Color.HSVToRGB(hue, 1f, 1f);
        }
        else
        {
            var configHtmlColor = MalumMenu.menuHtmlColor.Value;
            if (!ColorUtility.TryParseHtmlString(configHtmlColor, out newColor))
            {
                if (!configHtmlColor.StartsWith("#"))
                    ColorUtility.TryParseHtmlString("#" + configHtmlColor, out newColor);
            }
        }

        // Only rebuild if color actually changed
        if (newColor != _lastAccentColor)
        {
            _lastAccentColor = newColor;
            _accentTexCached = GUIStylePreset.MakeTex(1, 1, newColor);
        }
    }

    public void OnGUI()
    {
        if (!isGUIActive || MalumMenu.isPanicked) return;

        GUIStylePreset.ApplyToSkin();

        // Apply color for window chrome
        UIHelpers.ApplyUIColor();

        _windowRect = GUI.Window(
            (int)WindowId.MenuUI,
            _windowRect,
            (GUI.WindowFunction)WindowFunction,
            "MalumMenu v" + MalumMenu.malumVersion,
            GUIStylePreset.WindowStyle
        );
    }

    public void WindowFunction(int windowID)
    {
        // ✅ Use pre-built cached textures - NO MakeTex calls here
        GUI.DrawTexture(new Rect(0, 24, _windowRect.width, _windowRect.height - 24), _windowBgTex);
        GUI.DrawTexture(new Rect(0, 24, windowWidth * 0.20f, _windowRect.height - 24), _sidebarTex);
        GUI.DrawTexture(new Rect(0, 24, _windowRect.width, 2), _accentTexCached);

        GUILayout.BeginHorizontal();

        // Left sidebar (20% width)
        GUILayout.BeginVertical(GUILayout.Width(windowWidth * 0.20f));
        for (var i = 0; i < _tabs.Count; i++)
        {
            bool isActive = (_selectedTab == i);
            GUIStyle style = isActive ? GUIStylePreset.TabButtonActive : GUIStylePreset.TabButton;

            if (isActive)
                GUI.DrawTexture(new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 7, 3f, 21f), _accentTexCached);

            if (GUILayout.Button(_tabs[i].name, style, GUILayout.Height(35)))
                _selectedTab = i;
        }
        GUILayout.EndVertical();

        // Vertical separator
        GUILayout.Box("", GUIStylePreset.Separator, GUILayout.Width(1f), GUILayout.ExpandHeight(true));
        GUILayout.Space(10f);

        // Right content area
        GUILayout.BeginVertical();
        if (_selectedTab >= 0 && _selectedTab < _tabs.Count)
        {
            GUILayout.Label(_tabs[_selectedTab].name, GUIStylePreset.TabTitle);
            _tabs[_selectedTab].Draw();
        }
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUI.DragWindow();
    }
}