using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UITools {

    private static readonly Texture2D backgroundTexture = Texture2D.whiteTexture;
    private static readonly GUIStyle textureStyle = new GUIStyle { normal = new GUIStyleState { background = backgroundTexture } };

    public static void DrawRect(Rect position, Color color, GUIContent content = null)
    {
        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUI.Box(position, content ?? GUIContent.none, textureStyle);
        GUI.backgroundColor = backgroundColor;
    }

    public static void LayoutBox(Color color, GUIContent content = null)
    {
        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUILayout.Box(content ?? GUIContent.none, textureStyle);
        GUI.backgroundColor = backgroundColor;
    }

    public static Color ColorFromRGB(int r, int g, int b, int a = 255)
    {
        return new Color((float)r / 255, (float)g / 255, (float)b / 255, (float)a / 255);
    }

    public static bool WidthIsScalingReference()
    {
        if (Screen.width / 16 > Screen.height / 9)
        {
            return false;
        }

        return true;
    }

    public static float GetUIScalingFactor()
    {
        if (WidthIsScalingReference())
        {
            
            return (float)Screen.width / 640;
            
        }
        
        return (float)Screen.height / 360;
    }
}
