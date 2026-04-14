using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    // ── Palette ───────────────────────────────────────────────────
    public static readonly Color BgBase          = new Color(0.078f, 0.078f, 0.078f, 1f); // #141414
    public static readonly Color BgSidebar       = new Color(0.090f, 0.090f, 0.090f, 1f); // #171717
    public static readonly Color AccentBlue      = new Color(0.10f,  0.45f,  1.00f,  1f);
    public static readonly Color AccentBlueHover = new Color(0.20f,  0.55f,  1.00f,  1f);
    public static readonly Color BgButton        = new Color(0.12f,  0.12f,  0.12f,  1f);
    public static readonly Color BgButtonHover   = new Color(0.10f,  0.35f,  0.80f,  1f);
    public static readonly Color BgToggleOn      = new Color(0.07f,  0.25f,  0.60f,  1f);
    public static readonly Color SeparatorColor  = new Color(0.10f,  0.45f,  1.00f,  0.4f);
    public static readonly Color TrackColor      = new Color(0.10f,  0.10f,  0.10f,  1f);
    public static readonly Color TextPrimary     = new Color(0.95f,  0.97f,  1.00f,  1f);
    public static readonly Color TextSecondary   = new Color(0.55f,  0.65f,  0.80f,  1f);

    // ── Backing fields ────────────────────────────────────────────
    private static GUIStyle _separator;
    private static GUIStyle _normalButton;
    private static GUIStyle _normalToggle;
    private static GUIStyle _tabButton;
    private static GUIStyle _tabTitle;
    private static GUIStyle _tabSubtitle;
    private static GUIStyle _windowStyle;
    private static GUIStyle _sliderThumb;
    private static GUIStyle _sliderTrack;
    private static GUIStyle _scrollbarThumb;
    private static GUIStyle _scrollbarTrack;
    private static GUIStyle _textField;
    private static GUIStyle _boxStyle;
    private static GUIStyle _headerStyle;

    // ── Helpers ───────────────────────────────────────────────────
    public static Texture2D MakeTex(int w, int h, Color col)
    {
        var pix = new Color[w * h];
        for (int i = 0; i < pix.Length; i++) pix[i] = col;
        var t = new Texture2D(w, h);
        t.SetPixels(pix);
        t.Apply();
        return t;
    }

    // ── WindowStyle ───────────────────────────────────────────────
    public static GUIStyle WindowStyle
    {
        get
        {
            if (_windowStyle == null)
            {
                _windowStyle = new GUIStyle(GUI.skin.window);
                _windowStyle.normal.background   = MakeTex(2, 2, BgBase);
                _windowStyle.onNormal.background = MakeTex(2, 2, BgBase);
                _windowStyle.normal.textColor    = TextPrimary;
                _windowStyle.onNormal.textColor  = TextPrimary;
                _windowStyle.fontSize  = 13;
                _windowStyle.fontStyle = FontStyle.Bold;
                _windowStyle.padding   = new RectOffset { left = 8, right = 8, top = 24, bottom = 8 };
                _windowStyle.border    = new RectOffset { left = 4, right = 4, top = 4, bottom = 4 };
            }
            return _windowStyle;
        }
    }

    // ── Separator ─────────────────────────────────────────────────
    public static GUIStyle Separator
    {
        get
        {
            if (_separator == null)
            {
                _separator = new GUIStyle(GUI.skin.box);
                _separator.normal.background = MakeTex(2, 2, SeparatorColor);
                _separator.margin  = new RectOffset { left = 0, right = 0, top = 4, bottom = 4 };
                _separator.padding = new RectOffset();
                _separator.border  = new RectOffset();
            }
            return _separator;
        }
    }

    // ── NormalButton ──────────────────────────────────────────────
    public static GUIStyle NormalButton
    {
        get
        {
            if (_normalButton == null)
            {
                _normalButton = new GUIStyle(GUI.skin.button);
                _normalButton.normal.background   = MakeTex(2, 2, BgButton);
                _normalButton.hover.background    = MakeTex(2, 2, BgButtonHover);
                _normalButton.active.background   = MakeTex(2, 2, AccentBlue);
                _normalButton.focused.background  = MakeTex(2, 2, BgButtonHover);
                _normalButton.normal.textColor    = TextPrimary;
                _normalButton.hover.textColor     = Color.white;
                _normalButton.active.textColor    = Color.white;
                _normalButton.focused.textColor   = Color.white;
                _normalButton.fontSize   = 13;
                _normalButton.alignment  = TextAnchor.MiddleCenter;
                _normalButton.padding    = new RectOffset { left = 8, right = 8, top = 5, bottom = 5 };
                _normalButton.margin     = new RectOffset { left = 2, right = 2, top = 2, bottom = 2 };
                _normalButton.border     = new RectOffset { left = 2, right = 2, top = 2, bottom = 2 };
            }
            return _normalButton;
        }
    }

    // ── NormalToggle ──────────────────────────────────────────────
    public static GUIStyle NormalToggle
    {
        get
        {
            if (_normalToggle == null)
            {
                _normalToggle = new GUIStyle(GUI.skin.toggle);
                _normalToggle.normal.background   = MakeTex(2, 2, BgButton);
                _normalToggle.hover.background    = MakeTex(2, 2, new Color(0.15f, 0.40f, 0.85f, 1f));
                _normalToggle.normal.textColor    = TextSecondary;
                _normalToggle.onNormal.background  = MakeTex(2, 2, BgToggleOn);
                _normalToggle.onNormal.textColor   = AccentBlue;
                _normalToggle.fontSize  = 13;
                _normalToggle.padding   = new RectOffset { left = 22, right = 4, top = 5, bottom = 5 };
            }
            return _normalToggle;
        }
    }

    // ── Slider track ──────────────────────────────────────────────
    public static GUIStyle SliderTrack
    {
        get
        {
            if (_sliderTrack == null)
            {
                _sliderTrack = new GUIStyle(GUI.skin.horizontalSlider);
                _sliderTrack.normal.background = MakeTex(2, 4, TrackColor);
                _sliderTrack.fixedHeight = 6f;
            }
            return _sliderTrack;
        }
    }

    // ── Slider thumb ──────────────────────────────────────────────
    public static GUIStyle SliderThumb
    {
        get
        {
            if (_sliderThumb == null)
            {
                _sliderThumb = new GUIStyle(GUI.skin.horizontalSliderThumb);
                _sliderThumb.normal.background  = MakeTex(2, 2, AccentBlue);
                _sliderThumb.fixedWidth  = 14f;
                _sliderThumb.fixedHeight = 14f;
            }
            return _sliderThumb;
        }
    }

    // ── Scrollbar Styles (Essential for long menus) ────────────────
    public static GUIStyle ScrollbarTrack
    {
        get
        {
            if (_scrollbarTrack == null)
            {
                _scrollbarTrack = new GUIStyle(GUI.skin.verticalScrollbar);
                _scrollbarTrack.normal.background = MakeTex(2, 2, BgButton);
                _scrollbarTrack.fixedWidth = 6f;
            }
            return _scrollbarTrack;
        }
    }

    public static GUIStyle ScrollbarThumb
    {
        get
        {
            if (_scrollbarThumb == null)
            {
                _scrollbarThumb = new GUIStyle(GUI.skin.verticalScrollbarThumb);
                _scrollbarThumb.normal.background  = MakeTex(2, 2, new Color(0.15f, 0.45f, 0.90f, 1f));
                _scrollbarThumb.fixedWidth = 6f;
            }
            return _scrollbarThumb;
        }
    }

    // ── ADDITIONAL: Group Box Style ───────────────────────────────
    public static GUIStyle GroupBox
    {
        get
        {
            if (_boxStyle == null)
            {
                _boxStyle = new GUIStyle(GUI.skin.box);
                _boxStyle.normal.background = MakeTex(2, 2, new Color(0.11f, 0.11f, 0.11f, 1f));
                _boxStyle.padding = new RectOffset(10, 10, 10, 10);
                _boxStyle.margin = new RectOffset(0, 0, 5, 5);
                _boxStyle.border = new RectOffset(1, 1, 1, 1);
            }
            return _boxStyle;
        }
    }

    // ── Sidebar Styles ────────────────────────────────────────────
    public static GUIStyle TabButton
    {
        get
        {
            if (_tabButton == null)
            {
                _tabButton = new GUIStyle(GUI.skin.button);
                _tabButton.normal.background  = MakeTex(2, 2, new Color(0f, 0f, 0f, 0f));
                _tabButton.hover.background   = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.12f));
                _tabButton.normal.textColor   = TextSecondary;
                _tabButton.fontSize  = 13;
                _tabButton.fontStyle = FontStyle.Bold;
                _tabButton.alignment = TextAnchor.MiddleLeft;
                _tabButton.padding   = new RectOffset { left = 14, right = 6, top = 7, bottom = 7 };
            }
            return _tabButton;
        }
    }

    public static GUIStyle TabButtonActive
    {
        get
        {
            var s = new GUIStyle(TabButton);
            s.normal.background = MakeTex(2, 2, new Color(0.10f, 0.40f, 0.90f, 0.20f));
            s.normal.textColor  = Color.white;
            return s;
        }
    }
}