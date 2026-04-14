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

    // Manual Dragging Variables
    private bool _isDragging = false;
    private Vector2 _dragOffset;

    private static Texture2D _sidebarTex;
    private static Texture2D SidebarTex
    {
        get
        {
            if (_sidebarTex == null)
            {
                _sidebarTex = new Texture2D(1, 1);
                _sidebarTex.SetPixel(0, 0, new Color(0.06f, 0.06f, 0.09f, 1f));
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
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            isGUIActive = !isGUIActive;
        }

        if (CheatToggles.rgbMode)
        {
            hue += Time.deltaTime * 0.1f;
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
    }

    public void OnGUI()
    {
        if (!isGUIActive || MalumMenu.isPanicked) return;

        GUIStylePreset.ApplyToSkin();

        // Safe Color Set
        if (CheatToggles.rgbMode) 
            GUI.backgroundColor = Color.HSVToRGB(hue, 0.85f, 1f); 
        else 
            GUI.backgroundColor = GUIStylePreset.AccentBlue;

        // Draw Window
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
        // Top accent line
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

        // MANUAL DRAG - Replaces the stripped GUI.DragWindow()
        HandleManualDrag();
    }

    private void HandleManualDrag()
    {
        Event e = Event.current;
        Rect dragArea = new Rect(0, 0, windowWidth, 35); // Title bar area

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