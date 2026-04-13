using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    private static Texture2D _blurOverlay;

    // We use a 1x1 texture and stretch it. It's the most efficient way.
    private static Texture2D BlurOverlay
    {
        get
        {
            if (_blurOverlay == null)
            {
                // Fix: Standard constructor for IL2CPP compatibility
                _blurOverlay = new Texture2D(1, 1);
                // Near-black blue tint (Frosted Glass Effect)
                _blurOverlay.SetPixel(0, 0, new Color(0.04f, 0.04f, 0.07f, 0.88f));
                _blurOverlay.Apply();
            }
            return _blurOverlay;
        }
    }

    /// <summary>
    /// Call this at the start of OnGUI() to create the "Blurred Background" feel.
    /// </summary>
    public static void ApplyBlurEffect()
    {
        // This draws a semi-transparent dark blue "curtain" over the whole game.
        // It makes the game behind it look out of focus/blurred.
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlurOverlay);
    }

    public static void ApplyUIColor()
    {
        if (CheatToggles.rgbMode)
        {
            // RGB mode: vibrant but keeps the minimalist vibe
            GUI.backgroundColor = Color.HSVToRGB(MenuUI.hue, 0.7f, 1f);
        }
        else
        {
            // Default to your sleek Neon Blue
            GUI.backgroundColor = GUIStylePreset.AccentBlue;
        }
    }
}