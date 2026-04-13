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
    public static float hue; // For RGB mode

    // ── Cached dark sidebar texture ───────────────────────────────
    private static Texture2D _sidebarTex;
    private static Texture2D SidebarTex
    {
        get
        {
            if (_sidebarTex == null)
            {
                _sidebarTex = new Texture2D(1, 1);
                _sidebarTex.SetPixel(0, 0, GUIStylePreset.BgSidebar);
                _sidebarTex.Apply();
            }
            return _sidebarTex;
        }
    }

    private void Start()
    {
        // Add all tabs on start
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

        // Centre the window
        _windowRect = new(
            Screen.width  / 2f - windowWidth  / 2f,
            Screen.height / 2f - windowHeight / 2f,
            windowWidth,
            windowHeight
        );
    }

    // Apply theme overrides to default skin — runs every frame so all
    // bare GUILayout.Toggle / Button / HorizontalSlider calls are themed.
    public void InitStyles()
    {
        GUIStylePreset.ApplyToSkin();
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

        if (CheatToggles.openConfig)   { Utils.OpenConfigFile();          CheatToggles.openConfig   = false; }
        if (CheatToggles.reloadConfig) { MalumMenu.Plugin.Config.Reload(); CheatToggles.reloadConfig = false; }

        if (CheatToggles.saveProfile)
        {
            CheatToggles.saveProfile = false;
            CheatToggles.SaveTogglesToProfile();
        }
        if (CheatToggles.loadProfile)
        {
            CheatToggles.LoadTogglesFromProfile();
            CheatToggles.loadProfile = false;
        }

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

    // 1. Draw the "Blur" over the game first
    UIHelpers.ApplyBlurEffect();

    // 2. Apply colors and reset styles
    UIHelpers.ApplyUIColor();
    GUIStylePreset.Reset();

    // 3. Draw the actual window on top of the blur
    _windowRect = GUI.Window(
        (int)WindowId.MenuUI,
        _windowRect,
        (GUI.WindowFunction)WindowFunction,
        "  ◈  MalumMenu",
        GUIStylePreset.WindowStyle
    );
}

    public void WindowFunction(int windowID)
    {
        // Thin blue top-border accent line
        var accentRect = new Rect(0, 0, windowWidth, 2);
        GUI.DrawTexture(accentRect, MakeAccentTex());

        GUILayout.BeginHorizontal();

        // ── Sidebar (tab buttons) ─────────────────────────────────
        float sidebarW = windowWidth * 0.20f;
        GUILayout.BeginVertical(GUILayout.Width(sidebarW));

        // Dark sidebar backing
        GUI.DrawTexture(new Rect(0, 2, sidebarW, windowHeight), SidebarTex);

        GUILayout.Space(6f);
        for (var i = 0; i < _tabs.Count; i++)
        {
            // Selected tab gets the lit-up blue style
            var style = (_selectedTab == i) ? GUIStylePreset.TabButtonActive : GUIStylePreset.TabButton;

            if (GUILayout.Button(_tabs[i].name, style, GUILayout.Height(32)))
                _selectedTab = i;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        // ── Thin blue separator ───────────────────────────────────
        GUILayout.Box("", GUIStylePreset.Separator, GUILayout.Width(1f), GUILayout.ExpandHeight(true));
        GUILayout.Space(10f);

        // ── Content area ──────────────────────────────────────────
        GUILayout.BeginVertical(GUILayout.Width(windowWidth * 0.80f - 16f));
        GUILayout.Space(4f);

        if (_selectedTab >= 0 && _selectedTab < _tabs.Count)
        {
            GUILayout.Label(_tabs[_selectedTab].name, GUIStylePreset.TabTitle);
            // Thin blue rule under title
            GUILayout.Box("", GUIStylePreset.Separator, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
            GUILayout.Space(4f);
            _tabs[_selectedTab].Draw();
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUI.DragWindow();
    }

    // ── 1-pixel blue accent bar texture ──────────────────────────
    private static Texture2D _accentTex;
    private static Texture2D MakeAccentTex()
    {
        if (_accentTex == null)
        {
            _accentTex = new Texture2D(1, 1);
            _accentTex.SetPixel(0, 0, GUIStylePreset.AccentBlue);
            _accentTex.Apply();
        }
        return _accentTex;
    }
}
