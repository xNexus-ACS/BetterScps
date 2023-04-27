using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;

namespace Better106;

public class EventHandlers
{
    private readonly MainClass Plugin;
    public EventHandlers(MainClass plugin) => Plugin = plugin;

    public void OnRoundStarted()
    {
        Timing.RunCoroutine(RegenVigor(), "Better106_RegenVigor");
    }

    public void OnRoundEnded(EndingRoundEventArgs ev)
    {
        if (ev.IsRoundEnded)
            Timing.KillCoroutines("Better106_RegenVigor");
    }

    private IEnumerator<float> RegenVigor()
    {
        while (true)
        {
            foreach (var player in Player.List.Where(x => x.Role.Type is RoleTypeId.Scp106))
            {
                player.Role.Is(out Scp106Role scp106Role);

                if (scp106Role.Vigor < 100 && !scp106Role.IsSubmerged &&
                    !scp106Role.MovementDetected)
                    scp106Role.Vigor += Plugin.Config.VigorRegenAmount;
            }

            yield return Timing.WaitForSeconds(Plugin.Config.VigorRegenRate);
        }
    }
}