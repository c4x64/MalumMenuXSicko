using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    // --- Palette ---
    public static readonly Color BgBase = new(0.078f, 0.078f, 0.078f, 1f); // #141414
    public static readonly Color BgSidebar = new(0.090f, 0.090f, 0.090f, 1f); // #171717
    public static readonly Color SeparatorColor = new(0.10f, 0.45f, 1.00f, 0.4f);
    public static readonly Color TextPrimary = new(0.95f, 0.97f, 1.00f, 1f);
    public static readonly Color TextSecondary = new(0.55f, 0.65f, 0.80f, 1f);

    // --- Backing fields for lazy initialization ---
    private static GUIStyle _windowStyle;
    private static GUIStyle _tabButton;
    private static GUIStyle _tabTitle;
    private static GUIStyle _separator;

    public static Texture2D MakeTex(int w, int h, Color col)
    {
        var pix = new Color[w * h];
        for (int i = 0; i < pix.Length; i++) pix[i] = col;
        var t = new Texture2D(w, h);
        t.SetPixels(pix);
        t.Apply();
        return t;
    }

    public static GUIStyle WindowStyle
    {
        get
        {
            if (_windowStyle == null)
            {
                _windowStyle = new GUIStyle(GUI.skin.window)
                {
                    normal = { background = MakeTex(2, 2, BgBase), textColor = TextPrimary },
                    onNormal = { background = MakeTex(2, 2, BgBase), textColor = TextPrimary },
                    fontSize = 13,
                    fontStyle = FontStyle.Bold
                };
                _windowStyle.padding = new RectOffset { left = 0, right = 0, top = 24, bottom = 8 };
            }
            return _windowStyle;
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
                    normal = { background = MakeTex(2, 2, Color.clear), textColor = TextSecondary },
                    hover = { background = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.12f)), textColor = TextPrimary },
                    active = { background = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.22f)), textColor = Color.white },
                    fontSize = 13,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleLeft,
                };
                _tabButton.padding = new RectOffset { left = 14, right = 6, top = 7, bottom = 7 };
            }
            return _tabButton;
        }
    }

    public static GUIStyle TabButtonActive
    {
        get
        {
            var s = new GUIStyle(TabButton)
            {
                normal = { background = MakeTex(2, 2, new Color(0.10f, 0.40f, 0.90f, 0.20f)), textColor = Color.white }
            };
            return s;
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
                    normal = { textColor = TextPrimary },
                    fontSize = 18,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleLeft,
                    padding = new RectOffset()
                };
            }
            return _tabTitle;
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
                    normal = { background = MakeTex(2, 2, SeparatorColor) },
                    margin = new RectOffset { left = 0, right = 0, top = 4, bottom = 4 }
                };
            }
            return _separator;
        }
    }

    public static void ApplyToSkin()
    {
        // You can expand this later to style other default elements
        // For now, we are applying styles directly so this can be empty.
    }
}