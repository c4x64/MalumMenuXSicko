using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    // --- MODERN COLOR PALETTE ---
    private static Color GlassBlack = new Color(0.02f, 0.02f, 0.02f, 0.94f); 
    private static Color NeonBlue = new Color(0.0f, 0.5f, 1.0f, 1.0f);       
    private static Color TextDim = new Color(0.5f, 0.5f, 0.5f);

    private static GUIStyle _separator;
    private static GUIStyle _normalButton; // Now modern flat button
    private static GUIStyle _normalToggle; // Now iPhone style
    private static GUIStyle _tabButton;
    private static GUIStyle _tabTitle;
    private static GUIStyle _tabSubtitle;

    // Helper to create solid color textures
    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i) pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    // This makes the Window itself Black & Minimal
    public static GUIStyle WindowStyle
    {
        get
        {
            GUIStyle style = new GUIStyle(GUI.skin.window);
            style.normal.background = MakeTex(2, 2, GlassBlack);
            style.onNormal.background = MakeTex(2, 2, GlassBlack);
            style.border = new RectOffset(0, 0, 0, 0);
            style.padding = new RectOffset(0, 0, 0, 0);
            return style;
        }
    }

    public static GUIStyle Separator
    {
        get
        {
            if (_separator == null)
            {
                _separator = new GUIStyle(GUI.skin.box)
                {
                    normal = { background = MakeTex(2, 2, NeonBlue) },
                    margin = new RectOffset(0, 0, 4, 4),
                    fixedHeight = 1
                };
            }
            return _separator;
        }
    }

    public static GUIStyle NormalButton
    {
        get
        {
            if (_normalButton == null)
            {
                _normalButton = new GUIStyle(GUI.skin.button)
                {
                    fontSize = 13,
                    border = new RectOffset(0, 0, 0, 0),
                    normal = { background = MakeTex(2, 2, new Color(0.1f, 0.1f, 0.1f)), textColor = Color.white },
                    hover = { background = MakeTex(2, 2, new Color(0.15f, 0.15f, 0.15f)), textColor = NeonBlue },
                    active = { background = MakeTex(2, 2, NeonBlue), textColor = Color.white }
                };
            }
            return _normalButton;
        }
    }

    // Renamed visually to iPhone Style, but kept name 'NormalToggle' for your code
    public static GUIStyle NormalToggle
    {
        get
        {
            if (_normalToggle == null)
            {
                _normalToggle = new GUIStyle(GUI.skin.button) 
                {
                    fixedWidth = 46,
                    fixedHeight = 22,
                    margin = new RectOffset(5, 5, 5, 5),
                    border = new RectOffset(0, 0, 0, 0)
                };
                _normalToggle.normal.background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.2f));
                _normalToggle.onNormal.background = MakeTex(2, 2, NeonBlue);
            }
            return _normalToggle;
        }
    }

    public static GUIStyle TabButton
    {
        get
        {
            if (_tabButton == null)
            {
                _tabButton = new GUIStyle(GUI.skin.button)
                {
                    fontSize = 14,
                    alignment = TextAnchor.MiddleLeft,
                    fixedHeight = 35,
                    padding = new RectOffset(15, 0, 0, 0),
                    normal = { background = MakeTex(2, 2, Color.clear), textColor = TextDim },
                    onNormal = { background = MakeTex(2, 2, new Color(0, 0.5f, 1f, 0.1f)), textColor = Color.white },
                    hover = { textColor = Color.white },
                    border = new RectOffset(0, 0, 0, 0)
                };
            }
            return _tabButton;
        }
    }

    public static GUIStyle TabTitle
    {
        get
        {
            if (_tabTitle == null)
            {
                _tabTitle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 22,
                    fontStyle = FontStyle.Normal,
                    normal = { textColor = Color.white }
                };
            }
            return _tabTitle;
        }
    }

    public static GUIStyle TabSubtitle
    {
        get
        {
            if (_tabSubtitle == null)
            {
                _tabSubtitle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 14,
                    normal = { textColor = TextDim }
                };
            }
            return _tabSubtitle;
        }
    }
}