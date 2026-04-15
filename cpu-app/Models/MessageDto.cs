#nullable enable
namespace Gov.Cscp.Victims.Public.Models;

public class MessageDto
{
    public string? RegardingObjectId { get; set; }
    public string? Vsd_Program { get; set; }
    public string? Vsd_Cpu_RegionDistrict { get; set; }
    public string? Description { get; set; }

    // Shared properties for ETag/Concurrency
    public string? FortuneCookieType { get; set; }
    public string? FortuneCookieEtag { get; set; }
}
