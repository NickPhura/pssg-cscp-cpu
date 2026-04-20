namespace Gov.Cscp.Victims.Public.Models
{
    public class ScheduleDto
    {
        public string Vsd_ScheduleId { get; set; }
        public int[] Vsd_Days { get; set; }
        public string Vsd_ScheduledStartTime { get; set; }
        public string Vsd_ScheduledEndTime { get; set; }
        public int? Vsd_Cpu_ScheduleType { get; set; }
        public int? StateCode { get; set; }
        public string Vsd_ProgramIdValue { get; set; }
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
