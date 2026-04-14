using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    // ── Palette ───────────────────────────────────────────────────
    // #141414 / #171717 — solid, no transparency
    public static readonly Color BgBase         = new Color(0.078f, 0.078f, 0.078f, 1f); // #141414
    public static readonly Color BgSidebar      = new Color(0.090f, 0.090f, 0.090f, 1f); // #171717
    public static readonly Color AccentBlue     = new Color(0.10f,  0.45f,  1.00f,  1f);
    public static readonly Color AccentBlueHover= new Color(0.20f,  0.55f,  1.00f,  1f);
    // Buttons: slightly lighter than the base
    public static readonly Color BgButton       = new Color(0.12f,  0.12f,  0.12f,  1f);
    public static readonly Color BgButtonHover  = new Color(0.10f,  0.35f,  0.80f,  1f);
    // Toggle ON background
    public static readonly Color BgToggleOn     = new Color(0.07f,  0.25f,  0.60f,  1f);
    // Separator
    public static readonly Color SeparatorColor = new Color(0.10f,  0.45f,  1.00f,  0.4f);
    // Slider
    public static readonly Color TrackColor     = new Color(0.10f,  0.10f,  0.10f,  1f);
    // Text
    public static readonly Color TextPrimary    = new Color(0.95f,  0.97f,  1.00f,  1f);
    public static readonly Color TextSecondary  = new Color(0.55f,  0.65f,  0.80f,  1f);

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
                _windowStyle.padding   = new RectOffset(8, 8, 24, 8);
                _windowStyle.border    = new RectOffset(4, 4, 4, 4);
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
                _separator.margin  = new RectOffset(0, 0, 4, 4);
                _separator.padding = new RectOffset(0, 0, 0, 0);
                _separator.border  = new RectOffset(0, 0, 0, 0);
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
                _normalButton.fontStyle  = FontStyle.Normal;
                _normalButton.alignment  = TextAnchor.MiddleCenter;
                _normalButton.padding    = new RectOffset(8, 8, 5, 5);
                _normalButton.margin     = new RectOffset(2, 2, 2, 2);
                _normalButton.border     = new RectOffset(2, 2, 2, 2);
            }
            return _normalButton;
        }
    }

    // ── NormalToggle ──────────────────────────────────────────────
    // OFF: dark bg, muted text
    // ON:  blue-tinted bg, blue label text + blue checkbox indicator
    public static GUIStyle NormalToggle
    {
        get
        {
            if (_normalToggle == null)
            {
                _normalToggle = new GUIStyle(GUI.skin.toggle);
                // OFF
                _normalToggle.normal.background   = MakeTex(2, 2, BgButton);
                _normalToggle.hover.background    = MakeTex(2, 2, new Color(0.15f, 0.40f, 0.85f, 1f));
                _normalToggle.active.background   = MakeTex(2, 2, BgButtonHover);
                _normalToggle.normal.textColor    = TextSecondary;
                _normalToggle.hover.textColor     = TextPrimary;
                _normalToggle.active.textColor    = TextPrimary;
                // ON — clearly blue-accented
                _normalToggle.onNormal.background  = MakeTex(2, 2, BgToggleOn);
                _normalToggle.onHover.background   = MakeTex(2, 2, new Color(0.12f, 0.42f, 0.95f, 1f));
                _normalToggle.onActive.background  = MakeTex(2, 2, AccentBlue);
                _normalToggle.onNormal.textColor   = AccentBlue;
                _normalToggle.onHover.textColor    = Color.white;
                _normalToggle.onActive.textColor   = Color.white;
                _normalToggle.fontSize  = 13;
                _normalToggle.padding   = new RectOffset(22, 4, 5, 5);
                _normalToggle.margin    = new RectOffset(2, 2, 3, 3);
                _normalToggle.border    = new RectOffset(2, 2, 2, 2);
                _normalToggle.overflow  = new RectOffset(0, 0, 0, 0);
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
                _sliderTrack.border = new RectOffset(2, 2, 2, 2);
                _sliderTrack.fixedHeight = 6f;
                _sliderTrack.margin = new RectOffset(2, 2, 8, 8);
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
                _sliderThumb.hover.background   = MakeTex(2, 2, AccentBlueHover);
                _sliderThumb.active.background  = MakeTex(2, 2, Color.white);
                _sliderThumb.fixedWidth  = 14f;
                _sliderThumb.fixedHeight = 14f;
                _sliderThumb.border = new RectOffset(2, 2, 2, 2);
            }
            return _sliderThumb;
        }
    }

    // ── Scrollbar track ───────────────────────────────────────────
    public static GUIStyle ScrollbarTrack
    {
        get
        {
            if (_scrollbarTrack == null)
            {
                _scrollbarTrack = new GUIStyle(GUI.skin.verticalScrollbar);
                _scrollbarTrack.normal.background = MakeTex(2, 2, BgButton);
                _scrollbarTrack.fixedWidth = 6f;
                _scrollbarTrack.border = new RectOffset(2, 2, 2, 2);
            }
            return _scrollbarTrack;
        }
    }

    // ── Scrollbar thumb ───────────────────────────────────────────
    public static GUIStyle ScrollbarThumb
    {
        get
        {
            if (_scrollbarThumb == null)
            {
                _scrollbarThumb = new GUIStyle(GUI.skin.verticalScrollbarThumb);
                _scrollbarThumb.normal.background  = MakeTex(2, 2, new Color(0.15f, 0.45f, 0.90f, 1f));
                _scrollbarThumb.hover.background   = MakeTex(2, 2, AccentBlueHover);
                _scrollbarThumb.active.background  = MakeTex(2, 2, AccentBlue);
                _scrollbarThumb.fixedWidth = 6f;
                _scrollbarThumb.border = new RectOffset(2, 2, 2, 2);
            }
            return _scrollbarThumb;
        }
    }

    // ── Text field ────────────────────────────────────────────────
    public static GUIStyle TextField
    {
        get
        {
            if (_textField == null)
            {
                _textField = new GUIStyle(GUI.skin.textField);
                _textField.normal.background   = MakeTex(2, 2, new Color(0.10f, 0.10f, 0.10f, 1f));
                _textField.focused.background  = MakeTex(2, 2, new Color(0.08f, 0.18f, 0.38f, 1f));
                _textField.normal.textColor    = TextPrimary;
                _textField.focused.textColor   = Color.white;
                _textField.fontSize  = 13;
                _textField.padding   = new RectOffset(8, 8, 5, 5);
                _textField.margin    = new RectOffset(2, 2, 2, 2);
                _textField.border    = new RectOffset(2, 2, 2, 2);
            }
            return _textField;
        }
    }

    // ── Sidebar tab button (inactive) ─────────────────────────────
    public static GUIStyle TabButton
    {
        get
        {
            if (_tabButton == null)
            {
                _tabButton = new GUIStyle(GUI.skin.button);
                _tabButton.normal.background  = MakeTex(2, 2, new Color(0f, 0f, 0f, 0f));
                _tabButton.hover.background   = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.12f));
                _tabButton.active.background  = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.22f));
                _tabButton.normal.textColor   = TextSecondary;
                _tabButton.hover.textColor    = TextPrimary;
                _tabButton.active.textColor   = Color.white;
                _tabButton.fontSize  = 13;
                _tabButton.fontStyle = FontStyle.Bold;
                _tabButton.alignment = TextAnchor.MiddleLeft;
                _tabButton.padding   = new RectOffset(14, 6, 7, 7);
                _tabButton.margin    = new RectOffset(0, 0, 1, 1);
                _tabButton.border    = new RectOffset(0, 0, 0, 0);
            }
            return _tabButton;
        }
    }

    // ── Active tab button (selected) — brighter bg + white text ──
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

    // ── Section title ─────────────────────────────────────────────
    public static GUIStyle TabTitle
    {
        get
        {
            if (_tabTitle == null)
            {
                _tabTitle = new GUIStyle(GUI.skin.label);
                _tabTitle.normal.textColor = TextPrimary;
                _tabTitle.fontSize  = 18;
                _tabTitle.fontStyle = FontStyle.Bold;
                _tabTitle.alignment = TextAnchor.MiddleLeft;
                _tabTitle.padding   = new RectOffset(0, 0, 2, 6);
            }
            return _tabTitle;
        }
    }

    // ── Subtitle ──────────────────────────────────────────────────
    public static GUIStyle TabSubtitle
    {
        get
        {
            if (_tabSubtitle == null)
            {
                _tabSubtitle = new GUIStyle(GUI.skin.label);
                _tabSubtitle.normal.textColor = TextSecondary;
                _tabSubtitle.fontSize  = 12;
                _tabSubtitle.fontStyle = FontStyle.Bold;
                _tabSubtitle.alignment = TextAnchor.MiddleLeft;
                _tabSubtitle.padding   = new RectOffset(0, 0, 0, 4);
            }
            return _tabSubtitle;
        }
    }

    // ── Push all styles into GUI.skin (called every OnGUI frame) ──
    public static void ApplyToSkin()
    {
        GUI.skin.button                 = NormalButton;
        GUI.skin.toggle                 = NormalToggle;
        GUI.skin.horizontalSlider       = SliderTrack;
        GUI.skin.horizontalSliderThumb  = SliderThumb;
        GUI.skin.verticalScrollbar      = ScrollbarTrack;
        GUI.skin.verticalScrollbarThumb = ScrollbarThumb;
        GUI.skin.textField              = TextField;
        GUI.skin.textArea               = TextField;

        GUI.skin.label.normal.textColor = TextPrimary;
        GUI.skin.label.fontSize         = 13;
        GUI.skin.toggle.fontSize        = 13;
        GUI.skin.button.fontSize        = 13;
    }

    // ── Reset cached styles ───────────────────────────────────────
    public static void Reset()
    {
        _separator      = null;
        _normalButton   = null;
        _normalToggle   = null;
        _tabButton      = null;
        _tabTitle       = null;
        _tabSubtitle    = null;
        _windowStyle    = null;
        _sliderThumb    = null;
        _sliderTrack    = null;
        _scrollbarThumb = null;
        _scrollbarTrack = null;
        _textField      = null;
    }
}