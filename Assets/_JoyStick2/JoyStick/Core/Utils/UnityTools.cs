using UnityEngine;

public static class UnityTools
{
    public static bool IsActiveAndEnabled(Component component)
    {
        if (component == null)
        {
            return false;
        }
        Behaviour behaviour = component as Behaviour;
        if (behaviour != null)
        {
            return behaviour.isActiveAndEnabled;
        }
        return component.gameObject.activeInHierarchy;
    }

    public static void LogColor(Color color, object arg)
    {
        Debug.LogFormat("<color=#{0}>{1}</color>", UnityTools.Color2Hex(color), arg);
    }

    public static string Color2Hex(Color color)
    {
        return ColorUtility.ToHtmlStringRGB(color);
    }
}
