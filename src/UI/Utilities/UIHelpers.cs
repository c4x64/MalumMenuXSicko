using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    private static Texture2D _blurOverlay;

    private static Texture2D BlurOverlay
    {
        get
        {
            if (_blurOverlay == null)
            {
                _blurOverlay = new Texture2D(1, 1);
                // Dark Blue "Frosted" tint
                _blurOverlay.SetPixel(0, 0, new Color(0.04f, 0.04f, 0.07f, 0.88f));
                _blurOverlay.Apply();
            }
            return _blurOverlay;
        }
    }

    public static void ApplyBlurEffect()
    {
        // Graphics.DrawTexture is often more stable in IL2CPP than GUI.DrawTexture
        if (BlurOverlay != null)
        {
            Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlurOverlay);
        }
    }

    public static void ApplyUIColor()
    {
        if (CheatToggles.rgbMode)
        {
            GUI.backgroundColor = Color.HSVToRGB(MenuUI.hue, 0.85f, 1f);
        }
        else
        {
            // Default sleek blue
            GUI.backgroundColor = GUIStylePreset.AccentBlue;
        }
    }
}