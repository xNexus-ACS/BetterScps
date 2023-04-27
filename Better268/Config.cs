using Exiled.API.Interfaces;

namespace Better268;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    
    public float CustomCooldown { get; set; } = 80f;
}