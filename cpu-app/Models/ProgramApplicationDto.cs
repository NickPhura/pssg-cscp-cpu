namespace Gov.Cscp.Victims.Public.Models
{
    public class ProgramApplicationDto
    {
        public ContactDto BoardContact { get; set; }
        public string Businessbceid { get; set; }
        public ContractDto Contract { get; set; }
        public ContactDto ExecutiveContact { get; set; }
        public bool IsSuccess { get; set; }
        public OrganizationDto Organization { get; set; }
        public PortalRoleDto[] PortalRoles { get; set; }
        public ProgramDto[] ProgramCollection { get; set; }
        public ProgramContactDto[] ProgramContactCollection { get; set; }
        public ProgramContactDto[] ProgramSubcontractorCollection { get; set; }
        public ProgramTypeDto[] ProgramTypeCollection { get; set; }
        public RegionDistrictDto[] RegionDistrictCollection { get; set; }
        public string Result { get; set; }
        public ScheduleDto[] ScheduleCollection { get; set; }
        public ServiceAreaDto[] ServiceAreaCollection { get; set; }
        public ContactDto[] StaffCollection { get; set; }
        public string Userbceid { get; set; }
    }
}
