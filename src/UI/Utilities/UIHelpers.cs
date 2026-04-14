using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    public static void ApplyUIColor()
    {
        if (CheatToggles.rgbMode)
            GUI.backgroundColor = Color.HSVToRGB(MenuUI.hue, 0.85f, 1f);
        else
            GUI.backgroundColor = GUIStylePreset.AccentBlue;
    }
}
