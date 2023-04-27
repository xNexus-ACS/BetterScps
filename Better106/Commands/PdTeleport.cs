using System;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace Better106.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class PdTeleport : ICommand
{
    public string Command => "pocketdimensionteleport";
    public string[] Aliases => new[] {"pdt"};
    public string Description => "Teleports you as Scp106 to the Pocket Dimension / Back to the facility.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);

        if (player.Role.Type is not RoleTypeId.Scp106)
        {
            response = "You're not Scp106";
            return false;
        }

        if (Warhead.IsDetonated)
        {
            response = "The warhead has been detonated, you can't use this command.";
            return false;
        }
        
        DoTeleport(player);
        response = "Teleported.";
        return true;
    }

    private void DoTeleport(Player player)
    {
        if (player.CurrentRoom.Type != RoomType.Pocket)
        {
            player.Position = Room.Get(RoomType.Pocket).Position + Vector3.up;
        }
        else
        {
            player.Position = Room.Get(MainClass.Instance.Config.RoomToGoBack).Position + Vector3.up;
        }
    }
}