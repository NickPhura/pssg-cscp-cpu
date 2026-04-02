using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Carries the top-level Schedule G (expense report) fields sent to the
    /// Dynamics <c>vsd_scheduleg</c> entity.
    /// </summary>
    public class DynamicsScheduleGCollectionPost
    {
        // ── Dynamics entity type tag ──────────────────────────────────────────────
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_scheduleg"; } }

        /// <summary>GUID of the existing <c>vsd_scheduleg</c> record to update. Always required.</summary>
        [Required(ErrorMessage = "vsd_schedulegid (Schedule G GUID) is required.")]
        public string vsd_schedulegid { get; set; }

        // ── Contract service hours ─────────────────────────────────────────

        /// <summary>Actual service hours delivered this quarter; entered by the user.</summary>
        [Range(typeof(decimal), "0", "100000",
            ErrorMessage = "vsd_actualhoursthisquarter must be between 0 and 100,000.")]
        public decimal vsd_actualhoursthisquarter { get; set; }

        /// <summary>Contracted service hours for the quarter; read-only computed by Dynamics.</summary>
        public decimal vsd_contractedservicehrsthisquarter { get; set; }

        // ── Program administration ─────────────────────────────────────────

        /// <summary>Actual administration expenditure this quarter.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_programadministrationcurrentquarter must be between 0 and 10,000,000.")]
        public decimal vsd_programadministrationcurrentquarter { get; set; }

        /// <summary>Quarterly variance (budget − actual); can be negative on overspend. Computed by the FE.</summary>
        public decimal vsd_quarterlyvarianceprogramadministration { get; set; }

        /// <summary>Year-to-date administration total (actual + prior year-to-date).</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_yeartodateprogramadministration must be between 0 and 10,000,000.")]
        public decimal vsd_yeartodateprogramadministration { get; set; }

        /// <summary>Annual variance (annual budget − year-to-date); can be negative. Computed by the FE.</summary>
        public decimal vsd_yeartodatevarianceprogramadministration { get; set; }

        /// <summary>Free-text explanation for administration variance; optional.</summary>
        public string vsd_programadministrationexplanation { get; set; }

        // ── Program delivery ───────────────────────────────────────────────

        /// <summary>Actual program delivery expenditure this quarter.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_programdeliverycurrentquarter must be between 0 and 10,000,000.")]
        public decimal vsd_programdeliverycurrentquarter { get; set; }

        /// <summary>Quarterly variance (budget − actual); can be negative. Computed by the FE.</summary>
        public decimal vsd_quarterlyvarianceprogramdelivery { get; set; }

        /// <summary>Year-to-date program delivery total.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_yeartodateprogramdelivery must be between 0 and 10,000,000.")]
        public decimal vsd_yeartodateprogramdelivery { get; set; }

        /// <summary>Annual variance; can be negative. Computed by the FE.</summary>
        public decimal vsd_yeartodatevarianceprogramdelivery { get; set; }

        /// <summary>Free-text explanation for program delivery variance; optional.</summary>
        public string vsd_programdeliveryexplanations { get; set; }

        // ── Salaries and benefits ───────────────────────────────────────────

        /// <summary>Actual salaries and benefits expenditure this quarter.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_salariesbenefitscurrentquarter must be between 0 and 10,000,000.")]
        public decimal vsd_salariesbenefitscurrentquarter { get; set; }

        /// <summary>Quarterly variance; can be negative. Computed by the FE.</summary>
        public decimal vsd_quarterlyvariancesalariesbenefits { get; set; }

        /// <summary>Year-to-date salaries and benefits total.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_yeartodatesalariesandbenefits must be between 0 and 10,000,000.")]
        public decimal vsd_yeartodatesalariesandbenefits { get; set; }

        /// <summary>Annual variance; can be negative. Computed by the FE.</summary>
        public decimal vsd_yeartodatevariancesalariesbenefits { get; set; }

        /// <summary>Free-text explanation for salaries and benefits variance; optional.</summary>
        public string vsd_salariesandbenefitsexplanation { get; set; }

        // ── Authorization ─────────────────────────────────────────────────

        /// <summary>
        /// Whether the executive has reviewed and authorised the report.
        /// Set to <c>true</c> only on final submission; <c>false</c> on intermediate saves.
        /// </summary>
        public bool vsd_reportreviewed { get; set; }
    }
}
