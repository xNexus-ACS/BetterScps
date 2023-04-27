using System;
using Exiled.API.Features;

namespace Better106;

public class MainClass : Plugin<Config>
{
    public override string Author => "xNexusACS";
    public override string Name => "Better106";
    public override string Prefix => "Better106";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredExiledVersion => new(6, 0, 0);
        
    public EventHandlers EventHandlers;
    public static MainClass Instance;
        
    public override void OnEnabled()
    {
        Instance = this;
        EventHandlers = new EventHandlers(this);

        Exiled.Events.Handlers.Server.RoundStarted += EventHandlers.OnRoundStarted;
        Exiled.Events.Handlers.Server.EndingRound += EventHandlers.OnRoundEnded;
            
        base.OnEnabled();
    }
        
    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Server.RoundStarted -= EventHandlers.OnRoundStarted;
        Exiled.Events.Handlers.Server.EndingRound -= EventHandlers.OnRoundEnded;
            
        EventHandlers = null;
        Instance = null;
        base.OnDisabled();
    }
}