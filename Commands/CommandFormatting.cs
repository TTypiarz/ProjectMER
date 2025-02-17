using UnityEngine;

namespace ProjectMER.Commands;

public static class CommandFormatting
{
    public static readonly Color LightRed = new(1, 0.3f, 0.3f);
    public static readonly Color LightGreen = new(0.6f, 1, 0.6f);
    public static readonly Color LightGray = new(0.7f, 0.7f, 0.7f);

    /// <summary>
    /// Returns the formatted header string with the given category.
    /// </summary>
    public static string GetHeader(string category)
    {
        return $"<color=white><b><smallcaps>MapEditorReborn</smallcaps> " +
               $"<size=75%><color=green>v{ProjectMER.Singleton.Version}</color></size> / {category}</b></color>";
    }

    /// <summary>
    /// Modify the font of a given text.
    /// </summary>
    private static string ModifyFont(string text)
    {
        return $"<font=\"LiberationSans SDF\">{text}</font>";
    }

    /// <summary>
    /// Generates a formatted command response with an optional header.
    /// </summary>
    public static string GenerateResponse(bool withHeader, string category, string message)
    {
        string header = withHeader ? $"{GetHeader(category)}\n" : string.Empty;
        return "\n" + ModifyFont(header + message);
    }

    /// <summary>
    /// Generates a formatted command response with colored text indicator.
    /// </summary>
    public static string GenerateColoredResponse(bool withHeader, bool success, string category, string message)
    {
        string header = withHeader ? $"{GetHeader(category)}\n" : string.Empty;
        Color color = success ? LightGreen : LightRed;
        string coloredMessage = $"\t<i><color={color.ToHex()}>{message}</color></i>";
        return "\n" + ModifyFont(header + coloredMessage);
    }
}
