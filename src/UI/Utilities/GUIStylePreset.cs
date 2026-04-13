using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    // ══════════════════════════════════════════════════════════════
    //  THEME PALETTE
    // ══════════════════════════════════════════════════════════════
    public static readonly Color BgBase          = new Color(0.04f, 0.04f, 0.06f, 0.92f);
    public static readonly Color BgSidebar       = new Color(0.06f, 0.06f, 0.09f, 0.95f);
    public static readonly Color AccentBlue      = new Color(0.10f, 0.45f, 1.00f, 1.00f);
    public static readonly Color AccentBlueHover = new Color(0.20f, 0.55f, 1.00f, 0.85f);
    public static readonly Color BgButton        = new Color(0.09f, 0.09f, 0.13f, 0.90f);
    public static readonly Color BgButtonHover   = new Color(0.12f, 0.38f, 0.80f, 0.55f);
    public static readonly Color BgToggleOn      = new Color(0.08f, 0.30f, 0.70f, 0.45f);
    public static readonly Color SeparatorColor  = new Color(0.10f, 0.45f, 1.00f, 0.35f);
    public static readonly Color TrackColor      = new Color(0.08f, 0.08f, 0.12f, 0.95f);
    public static readonly Color ThumbColor      = new Color(0.10f, 0.45f, 1.00f, 0.90f);
    public static readonly Color ScrollbarBg     = new Color(0.06f, 0.06f, 0.09f, 0.80f);
    public static readonly Color TextPrimary     = new Color(0.95f, 0.97f, 1.00f, 1.00f);
    public static readonly Color TextSecondary   = new Color(0.55f, 0.65f, 0.80f, 1.00f);

    // ══════════════════════════════════════════════════════════════
    //  BACKING FIELDS
    // ══════════════════════════════════════════════════════════════
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

    // ══════════════════════════════════════════════════════════════
    //  HELPER
    // ══════════════════════════════════════════════════════════════
    public static Texture2D MakeTex(int w, int h, Color col)
    {
        var pix = new Color[w * h];
        for (var i = 0; i < pix.Length; i++) pix[i] = col;
        // IL2CPP requires explicit size without constructor params for best compatibility
        var t = new Texture2D(w, h);
        t.SetPixels(pix);
        t.Apply();
        return t;
    }

    public static Texture2D MakeRoundedTex(Color col)  => MakeTex(4, 4, col);

    // ══════════════════════════════════════════════════════════════
    //  STYLES (Fixed for IL2CPP)
    // ══════════════════════════════════════════════════════════════

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

    public static GUIStyle Separator
    {
        get
        {
            if (_separator == null)
            {
                _separator = new GUIStyle(GUI.skin.box);
                _separator.normal.background = MakeTex(2, 2, SeparatorColor);
                _separator.margin  = new RectOffset { left = 0, right = 0, top = 4, bottom = 4 };
                _separator.padding = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 };
                _separator.border  = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 };
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
                _normalButton = new GUIStyle(GUI.skin.button);
                _normalButton.normal.background   = MakeRoundedTex(BgButton);
                _normalButton.hover.background    = MakeRoundedTex(BgButtonHover);
                _normalButton.active.background   = MakeRoundedTex(AccentBlue);
                _normalButton.focused.background  = MakeRoundedTex(BgButtonHover);
                _normalButton.normal.textColor    = TextPrimary;
                _normalButton.hover.textColor     = Color.white;
                _normalButton.active.textColor    = Color.white;
                _normalButton.focused.textColor   = Color.white;
                _normalButton.fontSize   = 13;
                _normalButton.fontStyle  = FontStyle.Normal;
                _normalButton.alignment  = TextAnchor.MiddleCenter;
                _normalButton.padding    = new RectOffset { left = 8, right = 8, top = 5, bottom = 5 };
                _normalButton.margin     = new RectOffset { left = 2, right = 2, top = 2, bottom = 2 };
                _normalButton.border     = new RectOffset { left = 3, right = 3, top = 3, bottom = 3 };
            }
            return _normalButton;
        }
    }

    public static GUIStyle NormalToggle
    {
        get
        {
            if (_normalToggle == null)
            {
                _normalToggle = new GUIStyle(GUI.skin.toggle);
                _normalToggle.normal.background   = MakeTex(2, 2, BgButton);
                _normalToggle.hover.background    = MakeTex(2, 2, BgButtonHover);
                _normalToggle.active.background   = MakeTex(2, 2, BgButtonHover);
                _normalToggle.normal.textColor    = TextSecondary;
                _normalToggle.hover.textColor     = TextPrimary;
                _normalToggle.active.textColor    = TextPrimary;
                _normalToggle.onNormal.background  = MakeTex(2, 2, BgToggleOn);
                _normalToggle.onHover.background   = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.55f));
                _normalToggle.onActive.background  = MakeTex(2, 2, AccentBlue);
                _normalToggle.onNormal.textColor   = AccentBlue;
                _normalToggle.onHover.textColor    = Color.white;
                _normalToggle.onActive.textColor   = Color.white;
                _normalToggle.fontSize  = 13;
                _normalToggle.padding   = new RectOffset { left = 22, right = 4, top = 5, bottom = 5 };
                _normalToggle.margin    = new RectOffset { left = 2, right = 2, top = 3, bottom = 3 };
                _normalToggle.border    = new RectOffset { left = 3, right = 3, top = 3, bottom = 3 };
                _normalToggle.overflow  = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 };
            }
            return _normalToggle;
        }
    }

    public static GUIStyle SliderTrack
    {
        get
        {
            if (_sliderTrack == null)
            {
                _sliderTrack = new GUIStyle(GUI.skin.horizontalSlider);
                _sliderTrack.normal.background = MakeTex(2, 4, TrackColor);
                _sliderTrack.border = new RectOffset { left = 3, right = 3, top = 3, bottom = 3 };
                _sliderTrack.fixedHeight = 6f;
                _sliderTrack.margin = new RectOffset { left = 2, right = 2, top = 8, bottom = 8 };
            }
            return _sliderTrack;
        }
    }

    public static GUIStyle SliderThumb
    {
        get
        {
            if (_sliderThumb == null)
            {
                _sliderThumb = new GUIStyle(GUI.skin.horizontalSliderThumb);
                _sliderThumb.normal.background  = MakeTex(2, 2, ThumbColor);
                _sliderThumb.hover.background   = MakeTex(2, 2, AccentBlueHover);
                _sliderThumb.active.background  = MakeTex(2, 2, Color.white);
                _sliderThumb.fixedWidth  = 14f;
                _sliderThumb.fixedHeight = 14f;
                _sliderThumb.border = new RectOffset { left = 3, right = 3, top = 3, bottom = 3 };
            }
            return _sliderThumb;
        }
    }

    public static GUIStyle ScrollbarTrack
    {
        get
        {
            if (_scrollbarTrack == null)
            {
                _scrollbarTrack = new GUIStyle(GUI.skin.verticalScrollbar);
                _scrollbarTrack.normal.background = MakeTex(2, 2, ScrollbarBg);
                _scrollbarTrack.fixedWidth = 6f;
                _scrollbarTrack.border = new RectOffset { left = 2, right = 2, top = 2, bottom = 2 };
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
                _scrollbarThumb.normal.background  = MakeTex(2, 2, new Color(0.15f, 0.45f, 0.90f, 0.60f));
                _scrollbarThumb.hover.background   = MakeTex(2, 2, AccentBlueHover);
                _scrollbarThumb.active.background  = MakeTex(2, 2, AccentBlue);
                _scrollbarThumb.fixedWidth = 6f;
                _scrollbarThumb.border = new RectOffset { left = 2, right = 2, top = 2, bottom = 2 };
            }
            return _scrollbarThumb;
        }
    }

    public static GUIStyle TextField
    {
        get
        {
            if (_textField == null)
            {
                _textField = new GUIStyle(GUI.skin.textField);
                _textField.normal.background   = MakeTex(2, 2, new Color(0.06f, 0.06f, 0.10f, 0.95f));
                _textField.focused.background  = MakeTex(2, 2, new Color(0.08f, 0.10f, 0.18f, 0.95f));
                _textField.normal.textColor    = TextPrimary;
                _textField.focused.textColor   = Color.white;
                _textField.fontSize  = 13;
                _textField.padding   = new RectOffset { left = 8, right = 8, top = 5, bottom = 5 };
                _textField.margin    = new RectOffset { left = 2, right = 2, top = 2, bottom = 2 };
                _textField.border    = new RectOffset { left = 3, right = 3, top = 3, bottom = 3 };
            }
            return _textField;
        }
    }

    public static GUIStyle TabButton
    {
        get
        {
            if (_tabButton == null)
            {
                _tabButton = new GUIStyle(GUI.skin.button);
                _tabButton.normal.background  = MakeTex(2, 2, new Color(0f, 0f, 0f, 0f));
                _tabButton.hover.background   = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.15f));
                _tabButton.active.background  = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.25f));
                _tabButton.normal.textColor   = TextSecondary;
                _tabButton.hover.textColor    = TextPrimary;
                _tabButton.active.textColor   = Color.white;
                _tabButton.fontSize  = 13;
                _tabButton.fontStyle = FontStyle.Bold;
                _tabButton.alignment = TextAnchor.MiddleLeft;
                _tabButton.padding   = new RectOffset { left = 10, right = 6, top = 7, bottom = 7 };
                _tabButton.margin    = new RectOffset { left = 0, right = 0, top = 1, bottom = 1 };
                _tabButton.border    = new RectOffset { left = 0, right = 0, top = 0, bottom = 0 };
            }
            return _tabButton;
        }
    }

    public static GUIStyle TabButtonActive
    {
        get
        {
            var s = new GUIStyle(TabButton);
            s.normal.background = MakeTex(2, 2, new Color(0.10f, 0.45f, 1.00f, 0.22f));
            s.normal.textColor  = Color.white;
            return s;
        }
    }

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
                _tabTitle.padding   = new RectOffset { left = 0, right = 0, top = 2, bottom = 6 };
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
                _tabSubtitle = new GUIStyle(GUI.skin.label);
                _tabSubtitle.normal.textColor = TextSecondary;
                _tabSubtitle.fontSize  = 12;
                _tabSubtitle.fontStyle = FontStyle.Bold;
                _tabSubtitle.alignment = TextAnchor.MiddleLeft;
                _tabSubtitle.padding   = new RectOffset { left = 0, right = 0, top = 0, bottom = 4 };
            }
            return _tabSubtitle;
        }
    }

    public static void ApplyToSkin()
    {
        GUI.skin.button                  = NormalButton;
        GUI.skin.toggle                  = NormalToggle;
        GUI.skin.horizontalSlider        = SliderTrack;
        GUI.skin.horizontalSliderThumb   = SliderThumb;
        GUI.skin.verticalScrollbar       = ScrollbarTrack;
        GUI.skin.verticalScrollbarThumb  = ScrollbarThumb;
        GUI.skin.textField               = TextField;
        GUI.skin.textArea                = TextField;

        GUI.skin.label.normal.textColor  = TextPrimary;
        GUI.skin.label.fontSize          = 13;
        GUI.skin.toggle.fontSize         = 13;
        GUI.skin.button.fontSize         = 13;
    }

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