using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    /// <summary>
    /// Sets the global GUI.backgroundColor for the menu.
    /// No textures are drawn here to avoid IL2CPP unstripping errors.
    /// </summary>
    public static void ApplyUIColor()
    {
        if (CheatToggles.rgbMode)
        {
            // Syncs with the hue variable in MenuUI for RGB effect
            GUI.backgroundColor = Color.HSVToRGB(MenuUI.hue, 0.85f, 1f);
        }
        else
        {
            // Default to your sleek Neon Blue
            GUI.backgroundColor = GUIStylePreset.AccentBlue;
        }
    }
}