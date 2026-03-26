namespace Gov.Cscp.Victims.Public.Models
{
    public class CpuOrgContractsDto
    {
        public ContactDto BoardContact { get; set; }
        public string Businessbceid { get; set; }
        public ContractDto[] Contracts { get; set; }
        public ContactDto ExecutiveContact { get; set; }
        public InvoiceDto[] Invoices { get; set; }
        public bool IsSuccess { get; set; }
        public MessageDto[] Messages { get; set; }
        public SystemUserDto MinistryUser { get; set; }
        public OrganizationDto Organization { get; set; }
        public PortalRoleDto[] PortalRoles { get; set; }
        public ProgramDto[] Programs { get; set; }
        public string Result { get; set; }
        public ContactDto[] Staff { get; set; }
        public TaskDto[] Tasks { get; set; }
        public string Userbceid { get; set; }
        public string Fortunecookiecontext { get; set; }
    }
}
