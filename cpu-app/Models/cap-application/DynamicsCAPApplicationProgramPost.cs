using System;
using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Carries per-programme CAP application details sent to the Dynamics <c>vsd_program</c> entity.
    /// </summary>
    public class DynamicsCAPApplicationProgramPost
    {
        // ── Dynamics YesNo option-set bounds (evaluation efforts field) ───────────
        private const int YesNoMin = 100000000; // No / False
        private const int YesNoMax = 100000001; // Yes / True

        // ── Dynamics entity type tag ──────────────────────────────────────────────
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_program"; } }

        /// <summary>GUID of the Dynamics <c>vsd_program</c> record. Always required.</summary>
        [Required(ErrorMessage = "vsd_programid (programme GUID) is required.")]
        public string vsd_programid { get; set; }

        /// <summary>
        /// Funding amount requested by the applicant. CAPProgram.REQUIRED_FIELDS enforces this
        /// on the FE; must be &gt;= 0. Mapped to <c>Vsd_Cpu_FundingAmountRequested</c> (Money).
        /// </summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_cpu_fundingamountrequested must be between 0 and 10,000,000.")]
        public decimal vsd_cpu_fundingamountrequested { get; set; }

        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_cpu_estimatedsubtotalcomponentvalue must be between 0 and 10,000,000.")]
        public decimal vsd_cpu_estimatedsubtotalcomponentvalue { get; set; }

        /// <summary>
        /// Comma-separated multi-select option-set values representing chosen programme model types.
        /// Optional – may be null when the user has not yet selected any model.
        /// </summary>
        public string vsd_cpu_programmodeltypes { get; set; }

        /// <summary>Free-text description for 'Other' programme models; optional.</summary>
        public string vsd_otherprogrammodels { get; set; }

        /// <summary>
        /// Whether the programme measures evaluation efforts: 100000000 = No, 100000001 = Yes.
        /// Mapped directly to <c>Vsd_YesNo</c> enum; invalid values would throw a cast exception.
        /// </summary>
        [Range(YesNoMin, YesNoMax,
            ErrorMessage = "vsd_cpu_programevaluationefforts must be 100000000 (No) or 100000001 (Yes).")]
        public int vsd_cpu_programevaluationefforts { get; set; }

        /// <summary>Narrative description of evaluation efforts; optional.</summary>
        public string vsd_cpu_programevaluationdescription { get; set; }

        /// <summary>Additional operations comments; optional.</summary>
        public string vsd_cpu_capprogramoperationscomments { get; set; }

        // ── Navigation binding property ───────────────────────────────────────────

        private string _vsd_ContactLookupfortunecookiebind;
        /// <summary>
        /// Optional GUID of the primary programme contact. When set, the mapper extracts the GUID
        /// and sets the <c>vsd_contactlookup</c> entity reference. Null is valid when no contact
        /// has been assigned.
        /// </summary>
        public string vsd_ContactLookupfortunecookiebind
        {
            get
            {
                if (!String.IsNullOrEmpty(_vsd_ContactLookupfortunecookiebind))
                    return "/contacts(" + _vsd_ContactLookupfortunecookiebind + ")";
                return _vsd_ContactLookupfortunecookiebind;
            }
            set { _vsd_ContactLookupfortunecookiebind = value; }
        }
    }
}