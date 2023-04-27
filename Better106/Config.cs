using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace Better106;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
        
    public float VigorRegenAmount { get; set; } = 0.02f;
    public float VigorRegenRate { get; set; } = 0.50f;
    public RoomType RoomToGoBack { get; set; } = RoomType.HczHid;
}