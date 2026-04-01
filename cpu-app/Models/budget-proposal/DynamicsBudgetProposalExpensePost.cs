using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Represents a single expense line item (Salary &amp; Benefits, Program Delivery or Administration)
    /// sent to the Dynamics <c>vsd_programexpense</c> entity.
    /// </summary>
    public class DynamicsBudgetProposalExpensePost : IValidatableObject
    {
        // ── expense-type constants ──────────────────────────────────────────────
        private const int ExpenseTypeSalariesAndBenefits = 100000000;
        private const int ExpenseTypeProgramDelivery = 100000001;
        private const int ExpenseTypeAdministration = 100000002;

        // ── Dynamics entity type tag ────────────────────────────────────────────
        public string fortunecookietype { get { return "#Microsoft.Dynamics.CRM.vsd_programexpense"; } }

        // ── optional GUID of the existing expense record (null = create new) ───
        public string vsd_programexpenseid { get; set; }

        /// <summary>
        /// Expense category: 100000000 = Salaries &amp; Benefits,
        ///                   100000001 = Program Delivery,
        ///                   100000002 = Administration.
        /// </summary>
        [Range(ExpenseTypeSalariesAndBenefits, ExpenseTypeAdministration,
            ErrorMessage = "vsd_cpu_programexpensetype must be 100000000 (Salaries), 100000001 (Program Delivery) or 100000002 (Administration).")]
        public int vsd_cpu_programexpensetype { get; set; }

        /// <summary>Free-text description; required when this is an 'other' expense item (no eligible-expense-item binding).</summary>
        public string vsd_cpu_otherexpense { get; set; }

        /// <summary>Position / title of the staff member; required when expense type is Salaries &amp; Benefits.</summary>
        public string vsd_cpu_titleposition { get; set; }

        /// <summary>Computed total cost (Dynamics-side, read-only on post).</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_totalcost must be between 0 and 10,000,000.")]
        public decimal vsd_totalcost { get; set; }

        /// <summary>Input amount entered by the user (non-salary expenses).</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_inputamount must be between 0 and 10,000,000.")]
        public decimal vsd_inputamount { get; set; }

        /// <summary>Salary component (Salaries &amp; Benefits expenses only).</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_cpu_salary must be between 0 and 10,000,000.")]
        public decimal vsd_cpu_salary { get; set; }

        /// <summary>Amount of the expense funded from VSCP.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_cpu_fundedfromvscp must be between 0 and 10,000,000.")]
        public decimal vsd_cpu_fundedfromvscp { get; set; }

        /// <summary>Benefits component (Salaries &amp; Benefits expenses only).</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_cpu_benefits must be between 0 and 10,000,000.")]
        public decimal vsd_cpu_benefits { get; set; }

        /// <summary>Record state: 0 = active, 1 = inactive (soft-deleted).</summary>
        [Range(0, 1, ErrorMessage = "statecode must be 0 (active) or 1 (inactive).")]
        public int? statecode { get; set; }

        // ── Navigation binding properties ───────────────────────────────────────

        private string _vsd_EligibleExpenseItemIdfortunecookiebind;
        /// <summary>
        /// Optional binding to <c>vsd_eligibleexpenseitem</c>.
        /// Null causes Dynamics to create a new 'other' expense item server-side.
        /// </summary>
        public string vsd_EligibleExpenseItemIdfortunecookiebind
        {
            get
            {
                if (_vsd_EligibleExpenseItemIdfortunecookiebind != null)
                    return "/vsd_eligibleexpenseitems(" + _vsd_EligibleExpenseItemIdfortunecookiebind + ")";
                return null;
            }
            set { _vsd_EligibleExpenseItemIdfortunecookiebind = value; }
        }

        private string _vsd_ProgramIdfortunecookiebind;
        /// <summary>GUID of the programme this expense belongs to. Always required.</summary>
        [Required(ErrorMessage = "vsd_ProgramIdfortunecookiebind (programme GUID) is required.")]
        public string vsd_ProgramIdfortunecookiebind
        {
            get
            {
                if (_vsd_ProgramIdfortunecookiebind != null)
                    return "/vsd_programs(" + _vsd_ProgramIdfortunecookiebind + ")";
                return null;
            }
            set { _vsd_ProgramIdfortunecookiebind = value; }
        }

        // ── Cross-property (conditional) validation ─────────────────────────────
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Salaries & Benefits entries must identify the position/title.
            if (vsd_cpu_programexpensetype == ExpenseTypeSalariesAndBenefits
                && string.IsNullOrWhiteSpace(vsd_cpu_titleposition))
            {
                yield return new ValidationResult(
                    "vsd_cpu_titleposition is required for Salaries & Benefits expense entries.",
                    new[] { nameof(vsd_cpu_titleposition) });
            }

            // 'Other' expense items (no eligible-expense-item binding) must include a description.
            if (_vsd_EligibleExpenseItemIdfortunecookiebind == null
                && vsd_cpu_programexpensetype != ExpenseTypeSalariesAndBenefits
                && string.IsNullOrWhiteSpace(vsd_cpu_otherexpense))
            {
                yield return new ValidationResult(
                    "vsd_cpu_otherexpense is required when no eligible expense item is bound.",
                    new[] { nameof(vsd_cpu_otherexpense) });
            }
        }
    }
}