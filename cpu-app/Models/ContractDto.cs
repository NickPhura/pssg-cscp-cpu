using System;

namespace Gov.Cscp.Victims.Public.Models;

public class ContractDto
{
    public string? Vsd_ContractId { get; set; }
    public string? Vsd_Name { get; set; }
    public DateTime? Vsd_FiscalStartDate { get; set; }
    public int? StatusCode { get; set; }
    public string? FortuneCookieType { get; set; }
    public string? FortuneCookieEtag { get; set; }
}
