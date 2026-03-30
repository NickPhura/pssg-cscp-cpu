namespace Gov.Cscp.Victims.Public.Models
{
    public class ExpenseReportDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public string Businessbceid { get; set; }
        public string Userbceid { get; set; }

        public ContractDto Contract { get; set; }
        public OrganizationDto Organization { get; set; }
        public PortalRoleDto[] PortalRoles { get; set; }
        public ProgramDto Program { get; set; }
        public ScheduleGDto ScheduleG { get; set; }
        public ScheduleGLineItemDto[] ScheduleGLineItems { get; set; }
    }
}
