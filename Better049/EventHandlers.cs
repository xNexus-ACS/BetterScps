using CustomPlayerEffects;
using Exiled.Events.EventArgs.Scp049;

namespace Better049;

public class EventHandlers
{
    private readonly MainClass Plugin;
    public EventHandlers(MainClass plugin) => Plugin = plugin;

    public void OnRevived(FinishingRecallEventArgs ev)
    {
        if (Plugin.Config.EnableBuffing)
        {
            ev.Player.HumeShieldStat.CurValue += 100;
            ev.Player.EnableEffect<Scp207>(7);
        }
    }
}