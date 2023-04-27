using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace Better049.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class Eco : ICommand
{
    public string Command => "echolocation";
    public string[] Aliases => new[] {"eco"};
    public string Description => "Uses the echolocation ability of SCP-049.";

    private bool isOnCooldown = false;
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);

        if (player.Role.Type is not RoleTypeId.Scp049)
        {
            response = "You're not Scp049";
            return false;
        }

        if (isOnCooldown)
        {
            response = "You're on cooldown.";
            return false;
        }
        
        var target = Player.List
            .First(x => x.Role.Team is Team.FoundationForces or Team.ChaosInsurgency or Team.Scientists or Team.ClassD)
            .GetNearest(player.Position);
        
        TriggerEcholocation(player, target);
        
        response = "Echolocation triggered.";
        return true;
    }

    private void TriggerEcholocation(Player player, Player sensedTarget)
    {
        if (Vector3.Distance(player.Position, sensedTarget.Position) > 20f)
            return;
        
        player.Role.Is(out Scp049Role scp049Role);
        player.ShowHint("You sense a nearby human.", 5f);
        scp049Role.Sense(sensedTarget);
        isOnCooldown = true;

        Timing.CallDelayed(10f, () => scp049Role.LoseSenseTarget());
        Timing.CallDelayed(MainClass.Instance.Config.EcholocationCooldown, () => isOnCooldown = false);
    }
}