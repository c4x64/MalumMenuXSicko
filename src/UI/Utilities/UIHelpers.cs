using UnityEngine;

namespace MalumMenu;

public static class UIHelpers
{
    // Call this for your toggles. It uses the GUIStylePreset.NormalToggle
    public static bool DrawToggle(bool value, string label)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.ExpandWidth(true));

        // Using the style you wanted (iPhone visual, but named NormalToggle)
        if (GUILayout.Button("", GUIStylePreset.NormalToggle))
        {
            value = !value;
        }

        Rect lastRect = GUILayoutUtility.GetLastRect();
        float circleSize = 18f;
        float padding = (lastRect.height - circleSize) / 2f;
        float xPos = value ? (lastRect.xMax - circleSize - padding) : (lastRect.xMin + padding);

        // Draw the white "Thumb" circle
        Color oldColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.white;
        GUI.Box(new Rect(xPos, lastRect.y + padding, circleSize, circleSize), "", GUI.skin.button);
        GUI.backgroundColor = oldColor;

        GUILayout.EndHorizontal();
        return value;
    }

    public static void ApplyUIColor()
    {
        // Keeping your existing color logic
        if (CheatToggles.rgbMode)
        {
            GUI.backgroundColor = Color.HSVToRGB(MenuUI.hue, 1f, 1f);
        }
    }
}