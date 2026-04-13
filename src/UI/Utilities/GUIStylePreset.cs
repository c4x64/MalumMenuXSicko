using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    private static Color GlassBlack = new Color(0.02f, 0.02f, 0.02f, 0.94f); 
    private static Color NeonBlue = new Color(0.0f, 0.5f, 1.0f, 1.0f);       
    private static Color TextDim = new Color(0.5f, 0.5f, 0.5f);

    private static GUIStyle _separator;
    private static GUIStyle _normalButton; 
    private static GUIStyle _normalToggle; 
    private static GUIStyle _tabButton;
    private static GUIStyle _tabTitle;
    private static GUIStyle _tabSubtitle;

    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i) pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    public static GUIStyle WindowStyle
    {
        get
        {
            GUIStyle style = new GUIStyle(GUI.skin.window);
            style.normal.background = MakeTex(2, 2, GlassBlack);
            style.onNormal.background = MakeTex(2, 2, GlassBlack);
            style.border = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 };
            style.padding = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 };
            return style;
        }
    }

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
                    margin = new RectOffset { left = 5, right = 5, top = 5, bottom = 5 },
                    border = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 }
                };
                _normalToggle.normal.background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.2f));
                _normalToggle.onNormal.background = MakeTex(2, 2, NeonBlue);
            }
            return _normalToggle;
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
                    border = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 },
                    normal = { background = MakeTex(2, 2, new Color(0.1f, 0.1f, 0.1f)), textColor = Color.white },
                    hover = { background = MakeTex(2, 2, new Color(0.15f, 0.15f, 0.15f)), textColor = NeonBlue },
                    active = { background = MakeTex(2, 2, NeonBlue), textColor = Color.white }
                };
            }
            return _normalButton;
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
                    padding = new RectOffset { left = 15, right = 0, top = 0, bottom = 0 },
                    normal = { background = MakeTex(2, 2, Color.clear), textColor = TextDim },
                    onNormal = { background = MakeTex(2, 2, new Color(0, 0.5f, 1f, 0.1f)), textColor = Color.white },
                    hover = { textColor = Color.white },
                    border = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 }
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
                    normal = { textColor = Color.white },
                    padding = new RectOffset { left = 10, right = 0, top = 10, bottom = 5 }
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
                    normal = { textColor = TextDim },
                    padding = new RectOffset { left = 10, right = 0, top = 0, bottom = 0 }
                };
            }
            return _tabSubtitle;
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
                    margin = new RectOffset { left = 0, right = 0, top = 4, bottom = 4 },
                    padding = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 },
                    border = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 },
                    fixedHeight = 1
                };
            }
            return _separator;
        }
    }
}