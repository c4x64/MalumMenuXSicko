using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    // ApplyUIColor is now integrated into MenuUI.OnGUI for better stability,
    // but we'll keep the logic here for future tab support.
    public static void ApplyUIColor(float currentHue)
    {
        if (CheatToggles.rgbMode)
        {
            GUI.backgroundColor = Color.HSVToRGB(currentHue, 0.85f, 1f);
        }
        else
        {
            GUI.backgroundColor = GUIStylePreset.AccentBlue;
        }
    }

    // ApplyBlurEffect was removed because GUI.DrawTexture(Screen) 
    // triggers 'Method unstripping failed' in this IL2CPP build.
}