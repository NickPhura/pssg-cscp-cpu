using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    public class ProgramSurplusPost
    {
        [Required]
        public string BusinessBCeID { get; set; }
        [Required]
        public string UserBCeID { get; set; }
        public DynamicsProgramSurplusLineItemPost[] SurplusPlanLineItemCollection { get; set; }
        [Required]
        [MinLength(1)]
        public DynamicsProgramSurplus[] SurplusPlanCollection { get; set; }
    }
}
