using System;
using Exiled.API.Features;
using HarmonyLib;

namespace Better049;

public class MainClass : Plugin<Config>
{
    public override string Author => "xNexusACS";
    public override string Name => "Better049";
    public override string Prefix => "Better049";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredExiledVersion => new(6, 0, 0);
    
    public static MainClass Instance;
    public EventHandlers EventHandlers;
    
    public Harmony Harmony { get; private set; }

    public override void OnEnabled()
    {
        Instance = this;
        EventHandlers = new EventHandlers(this);
        Harmony = new Harmony("xnexusacs.better049");
        Harmony.PatchAll();

        Exiled.Events.Handlers.Scp049.FinishingRecall += EventHandlers.OnRevived;

        base.OnEnabled();
    }
    
    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp049.FinishingRecall -= EventHandlers.OnRevived;
        
        Harmony.UnpatchAll(Harmony.Id);
        Harmony = null;
        EventHandlers = null;
        Instance = null;
        base.OnDisabled();
    }
}