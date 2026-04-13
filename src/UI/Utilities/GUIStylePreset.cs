using UnityEngine;

namespace MalumMenu;

public static class GUIStylePreset
{
    // ── Theme Colors ──────────────────────────────────────────────
    public static readonly Color BgBase         = new Color(0.04f, 0.04f, 0.06f, 0.92f);
    public static readonly Color BgSidebar      = new Color(0.06f, 0.06f, 0.09f, 0.95f);
    public static readonly Color AccentBlue     = new Color(0.10f, 0.45f, 1.00f, 1.00f);
    public static readonly Color BgButton       = new Color(0.10f, 0.10f, 0.15f, 0.90f);
    public static readonly Color SeparatorColor = new Color(0.10f, 0.45f, 1.00f, 0.35f);
    public static readonly Color TextPrimary    = new Color(0.95f, 0.97f, 1.00f, 1.00f);
    public static readonly Color TextSecondary  = new Color(0.55f, 0.65f, 0.80f, 1.00f);

    // ── Backing Fields ────────────────────────────────────────────
    private static GUIStyle _separator;
    private static GUIStyle _normalButton;
    private static GUIStyle _normalToggle;
    private static GUIStyle _tabButton;
    private static GUIStyle _tabTitle;
    private static GUIStyle _tabSubtitle;
    private static GUIStyle _windowStyle;

    // ── Helpers ───────────────────────────────────────────────────
    private static Texture2D MakeTex(int w, int h, Color col)
    {
        var pix = new Color[w * h];
        for (var i = 0; i < pix.Length; i++) pix[i] = col;
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
                _separator.margin  = new RectOffset { top = 4, bottom = 4 };
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
                _normalButton.normal.background  = MakeTex(2, 2, BgButton);
                _normalButton.hover.background   = MakeTex(2, 2, new Color(0.13f, 0.50f, 1.00f, 0.45f));
                _normalButton.active.background  = MakeTex(2, 2, AccentBlue);
                _normalButton.normal.textColor   = TextPrimary;
                _normalButton.hover.textColor    = Color.white;
                _normalButton.active.textColor   = Color.white;
                _normalButton.fontSize  = 13;
                _normalButton.fontStyle = FontStyle.Normal;
                _normalButton.padding   = new RectOffset(6, 6, 4, 4);
                _normalButton.margin    = new RectOffset(2, 2, 2, 2);
                _normalButton.border    = new RectOffset(3, 3, 3, 3);
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
                _normalToggle.normal.textColor   = TextSecondary;
                _normalToggle.hover.textColor    = TextPrimary;
                _normalToggle.active.textColor   = Color.white;
                _normalToggle.onNormal.textColor = AccentBlue;
                _normalToggle.fontSize = 13;
                _normalToggle.margin   = new RectOffset(2, 2, 3, 3);
            }
            return _normalToggle;
        }
    }

    // ── TabButton ─────────────────────────────────────────────────
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
                _tabButton.padding   = new RectOffset(10, 6, 7, 7);
                _tabButton.margin    = new RectOffset(0, 0, 1, 1);
                _tabButton.border    = new RectOffset(0, 0, 0, 0);
            }
            return _tabButton;
        }
    }

    // Active (selected) tab — slightly brighter blue tinted bg
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

    // ── TabTitle ──────────────────────────────────────────────────
    public static GUIStyle TabTitle
    {
        get
        {
            if (_tabTitle == null)
            {
                _tabTitle = new GUIStyle(GUI.skin.label);
                _tabTitle.normal.textColor = TextPrimary;
                _tabTitle.fontSize   = 18;
                _tabTitle.fontStyle  = FontStyle.Bold;
                _tabTitle.alignment  = TextAnchor.MiddleLeft;
                _tabTitle.padding    = new RectOffset(0, 0, 2, 6);
            }
            return _tabTitle;
        }
    }

    // ── TabSubtitle ───────────────────────────────────────────────
    public static GUIStyle TabSubtitle
    {
        get
        {
            if (_tabSubtitle == null)
            {
                _tabSubtitle = new GUIStyle(GUI.skin.label);
                _tabSubtitle.normal.textColor = TextSecondary;
                _tabSubtitle.fontSize   = 12;
                _tabSubtitle.fontStyle  = FontStyle.Bold;
                _tabSubtitle.alignment  = TextAnchor.MiddleLeft;
                _tabSubtitle.padding    = new RectOffset(0, 0, 0, 4);
            }
            return _tabSubtitle;
        }
    }

    // ── Reset ─────────────────────────────────────────────────────
    public static void Reset()
    {
        _separator    = null;
        _normalButton = null;
        _normalToggle = null;
        _tabButton    = null;
        _tabTitle     = null;
        _tabSubtitle  = null;
        _windowStyle  = null;
    }
}
