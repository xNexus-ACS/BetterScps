using System.Linq;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;

namespace Better049;

public static class Extensions
{
    public static Player GetNearest(this Player player, Vector3 position)
    {
        var nearest = Player.List
            .Where(x => x.Role.Team is Team.FoundationForces or Team.ChaosInsurgency or Team.Scientists or Team.ClassD)
            .OrderBy(x => Vector3.Distance(position, x.Position))
            .FirstOrDefault();
        
        return nearest;
    }
}