using System;
using Exiled.API.Features;
using HarmonyLib;

namespace Better268;

public class MainClass : Plugin<Config>
{
    public override string Author => "xNexusACS";
    public override string Name => "Better268";
    public override string Prefix => "Better268";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredExiledVersion => new(6, 0, 0);

    public static MainClass Singleton;

    public Harmony Harmony { get; private set; }

    public override void OnEnabled()
    {
        Singleton = this;
        Harmony = new Harmony("xnexusacs.better268");
        Harmony.PatchAll();

        base.OnEnabled();
    }
    
    public override void OnDisabled()
    {
        Harmony.UnpatchAll(Harmony.Id);
        Harmony = null;
        Singleton = null;
        base.OnDisabled();
    }
}