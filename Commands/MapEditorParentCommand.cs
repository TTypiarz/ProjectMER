
using CommandSystem;
using ProjectMER.Commands.Map;
using ProjectMER.Commands.Modifying;
using ProjectMER.Commands.Modifying.Position;
using ProjectMER.Commands.Modifying.Rotation;
using ProjectMER.Commands.ToolGunLike;

namespace ProjectMER.Commands;

/// <summary>
/// The base parent command.
/// </summary>
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class MapEditorParentCommand : ParentCommand
{
    public MapEditorParentCommand() => LoadGeneratedCommands();

    public override string Command => "mapeditor";

    public override string[] Aliases { get; } = ["mer", "mp"];

    public override string Description => "The MapEditorReborn parent command";

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new Save());
        RegisterCommand(new Load());
        RegisterCommand(new Unload());
        RegisterCommand(new ToggleToolGun());
        RegisterCommand(new Utility.List());

        RegisterCommand(new Position());
        RegisterCommand(new Rotation());
        // RegisterCommand(new Scale());
        RegisterCommand(new Modify());

        RegisterCommand(new Select());
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        string description = string.Empty;

        foreach (ICommand command in AllCommands)
        {
            description += $"\t<b>• {arguments.Array[0]} <color=yellow>{command.Command}</color> <color=#808080>/ {string.Join(" / ", command.Aliases)}</color></b>\n\t\t<color=#D3D3D3><i>{command.Description}</i></color>\n";
        }

        response = CommandFormatting.GenerateResponse(true, "Commands", description);
        return true;
    }
}
