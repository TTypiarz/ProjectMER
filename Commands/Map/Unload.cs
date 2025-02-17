using CommandSystem;
using ProjectMER.Features;

namespace ProjectMER.Commands.Map;

public class Unload : ICommand
{
	public string Command => "unload";

	public string[] Aliases => ["unl"];

	public string Description => "Unloads a map";

	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
	{
		string description = "Unloaded all maps!";
		bool success = true;

		if (arguments.Count > 0)
		{
            MapUtils.UnloadMap(arguments.At(0));
            description = $"Unloaded map <color=white>{arguments.At(0)}</color>!";
		}
		else
		{
			foreach (string mapName in MapUtils.LoadedMaps.Keys.ToList())
			{
				MapUtils.UnloadMap(mapName);
			}
		}

		response = CommandFormatting.GenerateColoredResponse(true, success, "Map Management", description);
		return success;
	}
}
