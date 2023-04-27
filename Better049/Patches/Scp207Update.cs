using CustomPlayerEffects;
using Exiled.API.Features;
using HarmonyLib;
using Mirror;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using UnityEngine;

namespace Better049.Patches;

[HarmonyPatch(typeof(Scp207), nameof(Scp207.OnEffectUpdate))]
internal static class Scp207Update
{
    [HarmonyPrefix]
    private static void OnUpdate(Scp207 __instance)
    {
        var owner = Player.Get(__instance.Hub);
        
        if (!NetworkServer.active || Vitality.CheckPlayer(owner.ReferenceHub) || owner.Role.Team is Team.SCPs)
            return;
        __instance._damageCounter += Time.deltaTime;
        if (__instance._damageCounter < 1.0)
            return;
        
        --__instance._damageCounter;
        var num = owner.ReferenceHub.GetVelocity().SqrMagnitudeIgnoreY();
        owner.ReferenceHub.playerStats.DealDamage(new UniversalDamageHandler(
            (num > 2.0 ? num > 5.0 ? num > 50.0 ? 1f : 0.4f : 0.15f : 0.1f) *
            (__instance._currentDrink.DamageMultiplier * RainbowTaste.CurrentMultiplier(owner.ReferenceHub)),
            DeathTranslations.Scp207));
    }
}