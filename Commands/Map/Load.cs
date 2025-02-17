using CommandSystem;
using ProjectMER.Features;

namespace ProjectMER.Commands.Map;

public class Load : ICommand
{
	public string Command => "load";

	public string[] Aliases => ["l"];

	public string Description => "Loads a map";

	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
	{
        string description = "You need to provide a map name!";
        bool success = false;

        if (arguments.Count > 0)
        {
            if (MapUtils.LoadMap(arguments.At(0)))
            {
                description = $"Map <color=white>{arguments.At(0)}</color> loaded successfully!";
                success = true;
            }
            else
            {
                description = $"Map <color=white>{arguments.At(0)}</color> could not be loaded. Please check your server console for details.";
                success = false;
            }
        }

        response = CommandFormatting.GenerateColoredResponse(true, success, "Map Management", description);
		return success;
	}
}
