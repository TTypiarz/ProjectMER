using CommandSystem;
using ProjectMER.Features;

namespace ProjectMER.Commands.Map;

public class Save : ICommand
{
	public string Command => "save";

	public string[] Aliases => ["s"];

	public string Description => "Saves a map";

	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
	{
        string description = "You need to provide a map name!";
        bool success = false;

        if (arguments.Count != 0)
		{
            MapUtils.SaveMap(arguments.At(0));
            description = $"Saved current map as <color=white>{arguments.At(0)}</color>!";
			success = true;
		}

		response = CommandFormatting.GenerateColoredResponse(true, success, "Map Management", description);
		return success;
	}
}
