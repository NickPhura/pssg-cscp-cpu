#nullable enable
using System;

namespace Gov.Cscp.Victims.Public.Models;

public class SystemUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? InternalEmailAddress { get; set; }
    public string? Address1_Telephone1 { get; set; }
    public string? FortuneCookieType { get; set; }
    public string? FortuneCookieEtag { get; set; }
}
