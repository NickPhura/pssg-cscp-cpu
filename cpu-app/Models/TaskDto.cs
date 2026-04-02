using System;

namespace Gov.Cscp.Victims.Public.Models;

public class TaskDto
{
    public string? ActivityId { get; set; }
    public string? RegardingObjectIdValue { get; set; }
    public int? StatusCode { get; set; }
    public int? StateCode { get; set; }
    public string? Vsd_TaskTypeIdValue { get; set; }
    public string? Subject { get; set; }
    public string? Description { get; set; }
    public DateTime? ScheduledEnd { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? Vsd_ProgramIdValue { get; set; }
    public string? Vsd_ScheduleGIdValue { get; set; }
    public string? Vsd_SurplusPlanIdValue { get; set; }
    public string? FortuneCookieType { get; set; }
    public string? FortuneCookieEtag { get; set; }
}
