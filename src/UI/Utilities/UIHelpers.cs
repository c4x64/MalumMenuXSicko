using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    // Solid 1x1 texture used for blur-style dark overlay background
    private static Texture2D _blurOverlay;

    private static Texture2D BlurOverlay
    {
        get
        {
            if (_blurOverlay == null)
            {
                _blurOverlay = new Texture2D(1, 1);
                // Near-black with very slight blue tint — mimics frosted glass / backdrop blur
                _blurOverlay.SetPixel(0, 0, new Color(0.04f, 0.04f, 0.07f, 0.88f));
                _blurOverlay.Apply();
            }
            return _blurOverlay;
        }
    }

    /// <summary>
    /// Call once per frame before drawing the window.
    /// Sets GUI.backgroundColor to the blue accent (or RGB-cycled colour),
    /// and draws a full-screen dark blur-style overlay behind the menu.
    /// </summary>
    public static void ApplyUIColor()
    {
        // ── Full-screen "blur" backing overlay ────────────────────
        // Unity IMGUI has no real backdrop blur, but a semi-transparent
        // dark panel behind the window achieves the frosted-glass feel.
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlurOverlay);

        // ── Accent colour for window chrome / buttons ─────────────
        if (CheatToggles.rgbMode)
        {
            // RGB mode: cycle through hues
            GUI.backgroundColor = Color.HSVToRGB(MenuUI.hue, 0.85f, 1f);
        }
        else
        {
            var configHtmlColor = MalumMenu.menuHtmlColor.Value;

            // If no custom colour set, use the default blue accent
            if (string.IsNullOrEmpty(configHtmlColor))
            {
                GUI.backgroundColor = GUIStylePreset.AccentBlue;
                return;
            }

            if (!ColorUtility.TryParseHtmlString(configHtmlColor, out var uiColor))
            {
                if (!configHtmlColor.StartsWith("#"))
                {
                    if (ColorUtility.TryParseHtmlString("#" + configHtmlColor, out uiColor))
                    {
                        GUI.backgroundColor = uiColor;
                    }
                }
            }
            else
            {
                GUI.backgroundColor = uiColor;
            }
        }
    }
}
