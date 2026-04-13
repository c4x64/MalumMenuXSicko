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
            Screen.width  / 2f - windowWidth  / 2f,
            Screen.height / 2f - windowHeight / 2f,
            windowWidth,
            windowHeight
        );
    }

    private void Update()
    {
        // 1. Listen for the Delete key specifically
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            isGUIActive = !isGUIActive;
            Debug.Log("[MalumMenu] Menu Toggled: " + isGUIActive); 
        }

        // 2. RGB cycling
        if (CheatToggles.rgbMode)
        {
            hue += Time.deltaTime * 0.1f; // Slowed down for better aesthetics
            if (hue > 1f) hue -= 1f;
        }

        // 3. Keep existing game logic checks
        if (CheatToggles.stealthMode != MalumMenu.inStealthMode)
        {
            MalumMenu.inStealthMode = CheatToggles.stealthMode;
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "MainMenu" || scene.name == "MatchMaking")
                SceneManager.LoadScene(scene.name);
        }

        if (CheatToggles.panicMode) Utils.Panic();

        // (Keeping all your other logic checks for Player/Ship/Host status below...)
        // ... [Rest of your Update logic remains the same] ...
    }

   public void OnGUI()
{
    // 1. Safety check
    if (!isGUIActive || MalumMenu.isPanicked) return;

    // 2. Apply Theme (Replaces the broken InitStyles call)
    GUIStylePreset.ApplyToSkin();

    // 3. Optional: Set Accent Color (Replacing the crashy ApplyUIColor)
    if (CheatToggles.rgbMode) 
    {
        GUI.backgroundColor = Color.HSVToRGB(hue, 0.85f, 1f); 
    }
    else 
    {
        GUI.backgroundColor = GUIStylePreset.AccentBlue;
    }

    // 4. Reset style cache to ensure textures are fresh
    GUIStylePreset.Reset();

    // 5. Draw the Window
    _windowRect = GUI.Window(
        (int)WindowId.MenuUI,
        _windowRect,
        (GUI.WindowFunction)WindowFunction,
        "  ◈  MalumMenu  v" + MalumMenu.malumVersion,
        GUIStylePreset.WindowStyle
    );
}
    public void WindowFunction(int windowID)
    {
        // Top-border accent line
        var accentRect = new Rect(0, 0, windowWidth, 2);
        GUI.DrawTexture(accentRect, MakeAccentTex());

        GUILayout.BeginHorizontal();

        // Sidebar
        float sidebarW = windowWidth * 0.20f;
        GUILayout.BeginVertical(GUILayout.Width(sidebarW));
        GUI.DrawTexture(new Rect(0, 2, sidebarW, windowHeight), SidebarTex);

        GUILayout.Space(6f);
        for (var i = 0; i < _tabs.Count; i++)
        {
            var style = (_selectedTab == i) ? GUIStylePreset.TabButtonActive : GUIStylePreset.TabButton;
            if (GUILayout.Button(_tabs[i].name, style, GUILayout.Height(32)))
                _selectedTab = i;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        // Vertical Separator
        GUILayout.Box("", GUIStylePreset.Separator, GUILayout.Width(1f), GUILayout.ExpandHeight(true));
        GUILayout.Space(10f);

        // Content
        GUILayout.BeginVertical(GUILayout.Width(windowWidth * 0.80f - 16f));
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