using Exiled.API.Interfaces;

namespace Better049;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    
    public float EcholocationCooldown { get; set; } = 30f;
    public bool EnableBuffing { get; set; } = true;
}