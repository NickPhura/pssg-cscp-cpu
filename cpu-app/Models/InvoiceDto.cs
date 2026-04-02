using System;

namespace Gov.Cscp.Victims.Public.Models;

public class InvoiceDto
{
    public string? Vsd_InvoiceId { get; set; }
    public string? Vsd_ProgramIdValue { get; set; }
    public DateTime? Vsd_Cpu_ScheduledPaymentDate { get; set; }
    public int? StatusCode { get; set; }

    // Shared properties for ETag/Concurrency
    public string? FortuneCookieType { get; set; }
    public string? FortuneCookieEtag { get; set; }
}
