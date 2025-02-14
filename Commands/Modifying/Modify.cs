﻿using System.ComponentModel;
using System.Reflection;
using System.Text;
using CommandSystem;
using LabApi.Features.Wrappers;
using NorthwoodLib.Pools;
using ProjectMER.Features;
using ProjectMER.Features.Extensions;
using ProjectMER.Features.Objects;

namespace ProjectMER.Commands.Modifying;

/// <summary>
/// Command used for modifying the objects.
/// </summary>
public class Modify : ICommand
{
	/// <inheritdoc/>
	public string Command => "modify";

	/// <inheritdoc/>
	public string[] Aliases { get; } = ["mod"];

	/// <inheritdoc/>
	public string Description => "Allows modifying properties of the selected object.";

	/// <inheritdoc/>
	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
	{
		Player? player = Player.Get(sender);
		if (player is null)
		{
			response = "This command can't be run from the server console.";
			return false;
		}

		if (!ToolGun.TryGetSelectedMapObject(player, out MapEditorObject mapEditorObject))
		{
			response = "You haven't selected any object!";
			return false;
		}

		object instance = mapEditorObject.GetType().GetField("Base").GetValue(mapEditorObject);
		List<PropertyInfo> properties = instance.GetType().GetProperties().ToList();

		if (arguments.Count == 0)
		{
			StringBuilder sb = StringBuilderPool.Shared.Rent();
			sb.AppendLine();
			sb.Append("Object properties:");
			sb.AppendLine();
			sb.AppendLine();
			foreach (string property in properties.GetColoredProperties(instance))
			{
				sb.Append(property);
				sb.AppendLine();
			}

			response = StringBuilderPool.Shared.ToStringReturn(sb);
			return true;
		}

		PropertyInfo foundProperty = properties.FirstOrDefault(x => x.Name.ToLower().Contains(arguments.At(0).ToLower()));

		if (foundProperty == null)
		{
			response = $"There isn't any object property that contains \"{arguments.At(0)}\" in it's name!";
			return false;
		}

		try
		{
			if (foundProperty.PropertyType != typeof(string))
			{
				object value = TypeDescriptor.GetConverter(foundProperty.PropertyType).ConvertFromInvariantString(arguments.At(1));
				foundProperty.SetValue(instance, value);
			}
			else
			{
				string spacedString = arguments.At(1);
				for (int i = 1; i < arguments.Count - 1; i++)
				{
					spacedString += $" {arguments.At(1 + i)}";
				}

				foundProperty.SetValue(instance, TypeDescriptor.GetConverter(foundProperty.PropertyType).ConvertFromInvariantString(spacedString));
			}
		}
		catch (Exception)
		{
			response = $"\"{arguments.At(1)}\" is not a valid argument! The value should be a {foundProperty.PropertyType} type.";
			return false;
		}

		mapEditorObject.UpdateObjectAndCopies();

		response = "You've successfully modified the object!";
		return true;
	}
}