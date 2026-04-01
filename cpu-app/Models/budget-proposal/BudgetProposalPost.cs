using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    public class BudgetProposalPost
    {
        /// <summary>Organisation BCeID that owns the contract; sourced from session state.</summary>
        [Required(ErrorMessage = "BusinessBCeID is required.")]
        public string BusinessBCeID { get; set; }

        /// <summary>User BCeID of the person submitting the proposal; sourced from session state.</summary>
        [Required(ErrorMessage = "UserBCeID is required.")]
        public string UserBCeID { get; set; }

        /// <summary>All expense line items (salaries, program delivery, administration) across every programme.</summary>
        [Required(ErrorMessage = "ProgramExpenseCollection is required.")]
        public DynamicsBudgetProposalExpensePost[] ProgramExpenseCollection { get; set; }

        /// <summary>All revenue-source entries across every programme.</summary>
        [Required(ErrorMessage = "ProgramRevenueSourceCollection is required.")]
        public DynamicsBudgetProposalRevenueSourcePost[] ProgramRevenueSourceCollection { get; set; }

        /// <summary>One entry per programme; carries optional signing-officer information populated on final submission.</summary>
        [Required(ErrorMessage = "ProgramCollection must contain at least one programme entry.")]
        [MinLength(1, ErrorMessage = "ProgramCollection must contain at least one programme entry.")]
        public DynamicsBudgetProposalProgramPost[] ProgramCollection { get; set; }
    }
}
