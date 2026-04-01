using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Represents a single revenue-source entry sent to the Dynamics
    /// <c>vsd_programrevenuesource</c> entity.
    /// </summary>
    public class DynamicsBudgetProposalRevenueSourcePost : IValidatableObject
    {
        // ── revenue-source type constants ───────────────────────────────────────
        private const int RevenueTypeVscp = 100000000; // Ministry of PSSG – VSCP
        private const int RevenueTypeMunicipalGovernment = 100000001;
        private const int RevenueTypeRegionalDistrict = 100000002;
        private const int RevenueTypeApplicantOrg = 100000003;
        private const int RevenueTypeOther = 100000004; // requires vsd_cpu_otherrevenuesource

        // ── Dynamics entity type tag ────────────────────────────────────────────
        public string fortunecookietype { get { return "#Microsoft.Dynamics.CRM.vsd_programrevenuesource"; } }

        /// <summary>Optional GUID of an existing revenue-source record (null = create new).</summary>
        public string vsd_programrevenuesourceid { get; set; }

        /// <summary>Free-text label for 'Other' revenue sources; required when type is 100000004.</summary>
        public string vsd_cpu_otherrevenuesource { get; set; }

        /// <summary>
        /// Revenue-source category: 100000000 = VSCP, 100000001 = Municipal Govt,
        /// 100000002 = Regional District, 100000003 = Applicant Org, 100000004 = Other.
        /// </summary>
        [Range(RevenueTypeVscp, RevenueTypeOther,
            ErrorMessage = "vsd_cpu_revenuesourcetype must be between 100000000 (VSCP) and 100000004 (Other).")]
        public int vsd_cpu_revenuesourcetype { get; set; }

        /// <summary>Cash contribution amount; must be ≥ 0 when provided.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_cashcontribution must be between 0 and 10,000,000.")]
        public decimal? vsd_cashcontribution { get; set; }

        /// <summary>In-kind contribution amount; must be ≥ 0 when provided.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_inkindcontribution must be between 0 and 10,000,000.")]
        public decimal? vsd_inkindcontribution { get; set; }

        /// <summary>Record state: 0 = active, 1 = inactive (soft-deleted).</summary>
        [Range(0, 1, ErrorMessage = "statecode must be 0 (active) or 1 (inactive).")]
        public int? statecode { get; set; }

        // ── Navigation binding property ─────────────────────────────────────────

        private string _vsd_ProgramIdfortunecookiebind;
        /// <summary>GUID of the programme this revenue source belongs to. Always required.</summary>
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
            // 'Other' revenue sources must supply a descriptive label.
            if (vsd_cpu_revenuesourcetype == RevenueTypeOther
                && string.IsNullOrWhiteSpace(vsd_cpu_otherrevenuesource))
            {
                yield return new ValidationResult(
                    "vsd_cpu_otherrevenuesource is required when the revenue source type is 'Other' (100000004).",
                    new[] { nameof(vsd_cpu_otherrevenuesource) });
            }
        }
    }
}