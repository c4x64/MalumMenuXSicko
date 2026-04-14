using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    // ── Palette ───────────────────────────────────────────────────
    public static readonly Color BgBase         = new(0.078f, 0.078f, 0.078f, 1f); // #141414
    public static readonly Color BgSidebar      = new(0.090f, 0.090f, 0.090f, 1f); // #171717
    public static readonly Color AccentBlue     = new(0.10f,  0.45f,  1.00f,  1f);
    public static readonly Color AccentBlueHover= new(0.20f,  0.55f,  1.00f,  1f);
    public static readonly Color BgButton       = new(0.12f,  0.12f,  0.12f,  1f);
    public static readonly Color BgButtonHover  = new(0.10f,  0.35f,  0.80f,  1f);
    public static readonly Color BgToggleOn     = new(0.07f,  0.25f,  0.60f,  1f);
    public static readonly Color BgToggleOff    = new(0.20f,  0.20f,  0.20f,  1f);
    public static readonly Color SeparatorColor = new(0.10f,  0.45f,  1.00f,  0.4f);
    public static readonly Color TrackColor     = new(0.10f,  0.10f,  0.10f,  1f);
    public static readonly Color TextPrimary    = new(0.95f,  0.97f,  1.00f,  1f);
    public static readonly Color TextSecondary  = new(0.55f,  0.65f,  0.80f,  1f);
    public static readonly Color SuccessGreen   = new(0.20f,  0.80f,  0.30f,  1f);
    public static readonly Color WarningOrange  = new(1.00f,  0.64f,  0.00f,  1f);
    public static readonly Color ErrorRed       = new(0.90f,  0.30f,  0.30f,  1f);

    // ── Backing fields ────────────────────────────────────────────
    private static GUIStyle _separator;
    private static GUIStyle _normalButton;
    private static GUIStyle _normalToggle;
    private static GUIStyle _iphoneToggle;
    private static GUIStyle _tabButton;
    private static GUIStyle _tabTitle;
    private static GUIStyle _tabSubtitle;
    private static GUIStyle _windowStyle;
    private static GUIStyle _sliderThumb;
    private static GUIStyle _sliderTrack;
    private static GUIStyle _scrollbarThumb;
    private static GUIStyle _scrollbarTrack;
    private static GUIStyle _textField;
    private static GUIStyle _successButton;
    private static GUIStyle _warningButton;
    private static GUIStyle _errorButton;

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
                _windowStyle.padding = new RectOffset();
                _windowStyle.padding.left = 8;
                _windowStyle.padding.right = 8;
                _windowStyle.padding.top = 24;
                _windowStyle.padding.bottom = 8;
                _windowStyle.border = new RectOffset();
                _windowStyle.border.left = 4;
                _windowStyle.border.right = 4;
                _windowStyle.border.top = 4;
                _windowStyle.border.bottom = 4;
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
                _separator.margin = new RectOffset();
                _separator.margin.left = 0;
                _separator.margin.right = 0;
                _separator.margin.top = 4;
                _separator.margin.bottom = 4;
                _separator.padding = new RectOffset();
                _separator.border = new RectOffset();
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
                _normalButton.padding = new RectOffset();
                _normalButton.padding.left = 8;
                _normalButton.padding.right = 8;
                _normalButton.padding.top = 5;
                _normalButton.padding.bottom = 5;
                _normalButton.margin = new RectOffset();
                _normalButton.margin.left = 2;
                _normalButton.margin.right = 2;
                _normalButton.margin.top = 2;
                _normalButton.margin.bottom = 2;
                _normalButton.border = new RectOffset();
                _normalButton.border.left = 2;
                _normalButton.border.right = 2;
                _normalButton.border.top = 2;
                _normalButton.border.bottom = 2;
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
                _normalToggle.active.background   = MakeTex(2, 2, BgButtonHover);
                _normalToggle.normal.textColor    = TextSecondary;
                _normalToggle.hover.textColor     = TextPrimary;
                _normalToggle.active.textColor    = TextPrimary;
                _normalToggle.onNormal.background  = MakeTex(2, 2, BgToggleOn);
                _normalToggle.onHover.background   = MakeTex(2, 2, new Color(0.12f, 0.42f, 0.95f, 1f));
                _normalToggle.onActive.background  = MakeTex(2, 2, AccentBlue);
                _normalToggle.onNormal.textColor   = AccentBlue;
                _normalToggle.onHover.textColor    = Color.white;
                _normalToggle.onActive.textColor   = Color.white;
                _normalToggle.fontSize  = 13;
                _normalToggle.padding = new RectOffset();
                _normalToggle.padding.left = 22;
                _normalToggle.padding.right = 4;
                _normalToggle.padding.top = 5;
                _normalToggle.padding.bottom = 5;
                _normalToggle.margin = new RectOffset();
                _normalToggle.margin.left = 2;
                _normalToggle.margin.right = 2;
                _normalToggle.margin.top = 3;
                _normalToggle.margin.bottom = 3;
                _normalToggle.border = new RectOffset();
                _normalToggle.border.left = 2;
                _normalToggle.border.right = 2;
                _normalToggle.border.top = 2;
                _normalToggle.border.bottom = 2;
                _normalToggle.overflow = new RectOffset();
            }
            return _normalToggle;
        }
    }

    // ── iPhone-Style Toggle ───────────────────────────────────────
    public static GUIStyle IPhoneToggle
    {
        get
        {
            if (_iphoneToggle == null)
            {
                _iphoneToggle = new GUIStyle(GUI.skin.toggle);
                _iphoneToggle.normal.background   = MakeTex(2, 2, BgToggleOff);
                _iphoneToggle.hover.background    = MakeTex(2, 2, new Color(0.25f, 0.25f, 0.25f, 1f));
                _iphoneToggle.active.background   = MakeTex(2, 2, BgToggleOff);
                _iphoneToggle.normal.textColor    = TextSecondary;
                _iphoneToggle.hover.textColor     = TextSecondary;
                _iphoneToggle.active.textColor    = TextSecondary;
                _iphoneToggle.onNormal.background  = MakeTex(2, 2, SuccessGreen);
                _iphoneToggle.onHover.background   = MakeTex(2, 2, new Color(0.30f, 0.90f, 0.40f, 1f));
                _iphoneToggle.onActive.background  = MakeTex(2, 2, SuccessGreen);
                _iphoneToggle.onNormal.textColor   = Color.white;
                _iphoneToggle.onHover.textColor    = Color.white;
                _iphoneToggle.onActive.textColor   = Color.white;
                _iphoneToggle.fontSize  = 12;
                _iphoneToggle.fontStyle = FontStyle.Normal;
                _iphoneToggle.alignment = TextAnchor.MiddleCenter;
                _iphoneToggle.padding = new RectOffset();
                _iphoneToggle.padding.left = 12;
                _iphoneToggle.padding.right = 12;
                _iphoneToggle.padding.top = 6;
                _iphoneToggle.padding.bottom = 6;
                _iphoneToggle.margin = new RectOffset();
                _iphoneToggle.margin.left = 2;
                _iphoneToggle.margin.right = 2;
                _iphoneToggle.margin.top = 3;
                _iphoneToggle.margin.bottom = 3;
                _iphoneToggle.border = new RectOffset();
                _iphoneToggle.border.left = 12;
                _iphoneToggle.border.right = 12;
                _iphoneToggle.border.top = 3;
                _iphoneToggle.border.bottom = 3;
                _iphoneToggle.overflow = new RectOffset();
            }
            return _iphoneToggle;
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
                _sliderTrack.border = new RectOffset();
                _sliderTrack.border.left = 2;
                _sliderTrack.border.right = 2;
                _sliderTrack.border.top = 2;
                _sliderTrack.border.bottom = 2;
                _sliderTrack.fixedHeight = 6f;
                _sliderTrack.margin = new RectOffset();
                _sliderTrack.margin.left = 2;
                _sliderTrack.margin.right = 2;
                _sliderTrack.margin.top = 8;
                _sliderTrack.margin.bottom = 8;
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
                _sliderThumb.border = new RectOffset();
                _sliderThumb.border.left = 2;
                _sliderThumb.border.right = 2;
                _sliderThumb.border.top = 2;
                _sliderThumb.border.bottom = 2;
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
                _scrollbarTrack.border = new RectOffset();
                _scrollbarTrack.border.left = 2;
                _scrollbarTrack.border.right = 2;
                _scrollbarTrack.border.top = 2;
                _scrollbarTrack.border.bottom = 2;
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
                _scrollbarThumb.border = new RectOffset();
                _scrollbarThumb.border.left = 2;
                _scrollbarThumb.border.right = 2;
                _scrollbarThumb.border.top = 2;
                _scrollbarThumb.border.bottom = 2;
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
                _textField.padding = new RectOffset();
                _textField.padding.left = 8;
                _textField.padding.right = 8;
                _textField.padding.top = 5;
                _textField.padding.bottom = 5;
                _textField.margin = new RectOffset();
                _textField.margin.left = 2;
                _textField.margin.right = 2;
                _textField.margin.top = 2;
                _textField.margin.bottom = 2;
                _textField.border = new RectOffset();
                _textField.border.left = 2;
                _textField.border.right = 2;
                _textField.border.top = 2;
                _textField.border.bottom = 2;
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
                _tabButton.normal.background  = MakeTex(2, 2, Color.clear);
                _tabButton.hover.background   = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.12f));
                _tabButton.active.background  = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.22f));
                _tabButton.normal.textColor   = TextSecondary;
                _tabButton.hover.textColor    = TextPrimary;
                _tabButton.active.textColor   = Color.white;
                _tabButton.fontSize  = 13;
                _tabButton.fontStyle = FontStyle.Bold;
                _tabButton.alignment = TextAnchor.MiddleLeft;
                _tabButton.padding = new RectOffset();
                _tabButton.padding.left = 14;
                _tabButton.padding.right = 6;
                _tabButton.padding.top = 7;
                _tabButton.padding.bottom = 7;
                _tabButton.margin = new RectOffset();
                _tabButton.margin.left = 0;
                _tabButton.margin.right = 0;
                _tabButton.margin.top = 1;
                _tabButton.margin.bottom = 1;
                _tabButton.border = new RectOffset();
            }
            return _tabButton;
        }
    }

    // ── Active tab button (selected) ──────────────────────────────
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
                _tabTitle.padding = new RectOffset();
                _tabTitle.padding.left = 0;
                _tabTitle.padding.right = 0;
                _tabTitle.padding.top = 2;
                _tabTitle.padding.bottom = 6;
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
                _tabSubtitle.padding = new RectOffset();
                _tabSubtitle.padding.left = 0;
                _tabSubtitle.padding.right = 0;
                _tabSubtitle.padding.top = 0;
                _tabSubtitle.padding.bottom = 4;
            }
            return _tabSubtitle;
        }
    }

    // ── Success Button (Green) ────────────────────────────────────
    public static GUIStyle SuccessButton
    {
        get
        {
            if (_successButton == null)
            {
                _successButton = new GUIStyle(NormalButton);
                _successButton.normal.background   = MakeTex(2, 2, SuccessGreen);
                _successButton.hover.background    = MakeTex(2, 2, new Color(0.30f, 0.90f, 0.40f, 1f));
                _successButton.active.background   = MakeTex(2, 2, new Color(0.10f, 0.60f, 0.20f, 1f));
                _successButton.normal.textColor    = Color.white;
                _successButton.hover.textColor     = Color.white;
                _successButton.active.textColor    = Color.white;
            }
            return _successButton;
        }
    }

    // ── Warning Button (Orange) ───────────────────────────────────
    public static GUIStyle WarningButton
    {
        get
        {
            if (_warningButton == null)
            {
                _warningButton = new GUIStyle(NormalButton);
                _warningButton.normal.background   = MakeTex(2, 2, WarningOrange);
                _warningButton.hover.background    = MakeTex(2, 2, new Color(1.00f, 0.74f, 0.10f, 1f));
                _warningButton.active.background   = MakeTex(2, 2, new Color(0.80f, 0.54f, 0.00f, 1f));
                _warningButton.normal.textColor    = Color.white;
                _warningButton.hover.textColor     = Color.white;
                _warningButton.active.textColor    = Color.white;
            }
            return _warningButton;
        }
    }

    // ── Error Button (Red) ────────────────────────────────────────
    public static GUIStyle ErrorButton
    {
        get
        {
            if (_errorButton == null)
            {
                _errorButton = new GUIStyle(NormalButton);
                _errorButton.normal.background   = MakeTex(2, 2, ErrorRed);
                _errorButton.hover.background    = MakeTex(2, 2, new Color(1.00f, 0.40f, 0.40f, 1f));
                _errorButton.active.background   = MakeTex(2, 2, new Color(0.70f, 0.10f, 0.10f, 1f));
                _errorButton.normal.textColor    = Color.white;
                _errorButton.hover.textColor     = Color.white;
                _errorButton.active.textColor    = Color.white;
            }
            return _errorButton;
        }
    }

    // ── Push all styles into GUI.skin ──────────────────────────────
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

    // ── Reset cached styles ────────────────────────────────────────
    public static void Reset()
    {
        _separator      = null;
        _normalButton   = null;
        _normalToggle   = null;
        _iphoneToggle   = null;
        _tabButton      = null;
        _tabTitle       = null;
        _tabSubtitle    = null;
        _windowStyle    = null;
        _sliderThumb    = null;
        _sliderTrack    = null;
        _scrollbarThumb = null;
        _scrollbarTrack = null;
        _textField      = null;
        _successButton  = null;
        _warningButton  = null;
        _errorButton    = null;
    }
}