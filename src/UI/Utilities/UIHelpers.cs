using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    /// <summary>
    /// Sets the background color. If no hue is provided, it defaults to MenuUI.hue.
    /// </summary>
    public static void ApplyUIColor(float currentHue = -1f)
    {
        // If no value was passed, use the global hue from MenuUI
        float hueToUse = (currentHue < 0) ? MenuUI.hue : currentHue;

        if (CheatToggles.rgbMode)
        {
            GUI.backgroundColor = Color.HSVToRGB(hueToUse, 0.85f, 1f);
        }
        else
        {
            GUI.backgroundColor = GUIStylePreset.AccentBlue;
        }
    }
}