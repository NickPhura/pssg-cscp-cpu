using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    public class CAPApplicationPost
    {
        /// <summary>Organisation BCeID that owns the contract; sourced from session state.</summary>
        [Required(ErrorMessage = "BusinessBCeID is required.")]
        public string BusinessBCeID { get; set; }

        /// <summary>User BCeID of the person submitting the application; sourced from session state.</summary>
        [Required(ErrorMessage = "UserBCeID is required.")]
        public string UserBCeID { get; set; }

        /// <summary>Exactly one contract entry carrying the contract GUID, insurance and compliance flags.</summary>
        [Required(ErrorMessage = "ContractCollection is required.")]
        [MinLength(1, ErrorMessage = "ContractCollection must contain at least one contract entry.")]
        public DynamicsCAPApplicationContractPost[] ContractCollection { get; set; }

        /// <summary>Organisation address and identity information; always populated from applicant information.</summary>
        [Required(ErrorMessage = "Organization is required.")]
        public DynamicsCAPApplicationOrganizationPost Organization { get; set; }

        /// <summary>Staff contacts to associate with programmes; omitted when no staff are added.</summary>
        public DynamicsCAPApplicationProgramContactPost[] AddProgramContactCollection { get; set; }

        /// <summary>Staff contacts to disassociate from programmes; omitted when no staff are removed.</summary>
        public DynamicsCAPApplicationProgramContactPost[] RemoveProgramContactCollection { get; set; }

        /// <summary>One entry per CAP programme attached to the contract.</summary>
        [Required(ErrorMessage = "ProgramCollection is required.")]
        [MinLength(1, ErrorMessage = "ProgramCollection must contain at least one programme entry.")]
        public DynamicsCAPApplicationProgramPost[] ProgramCollection { get; set; }
    }
}
