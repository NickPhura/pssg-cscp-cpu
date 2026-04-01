using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Represents one programme expense line item sent to the
    /// Dynamics <c>vsd_scheduleglineitem</c> entity.
    /// </summary>
    public class DynamicsScheduleGLineItemCollectionPost
    {
        // ── Dynamics entity type tag ──────────────────────────────────────────────
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_scheduleglineitem"; } }

        /// <summary>GUID of the existing <c>vsd_scheduleglineitem</c> record. Always required.</summary>
        [Required(ErrorMessage = "vsd_scheduleglineitemid (line item GUID) is required.")]
        public string vsd_scheduleglineitemid { get; set; }

        /// <summary>Actual expenditure entered by the user for the current quarter; must be ≥ 0.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_actualexpensescurrentquarter must be between 0 and 10,000,000.")]
        public decimal vsd_actualexpensescurrentquarter { get; set; }

        /// <summary>
        /// Quarterly variance (quarterly budget − actual); computed by the FE as
        /// <c>quarterlyBudget - actual</c>. Can be negative on overspend.
        /// </summary>
        public decimal vsd_quarterlyvariance { get; set; }

        /// <summary>Year-to-date actual expenditure (actual + prior year-to-date); must be ≥ 0.</summary>
        [Range(typeof(decimal), "0", "10000000",
            ErrorMessage = "vsd_actualexpendituresyeartodate must be between 0 and 10,000,000.")]
        public decimal vsd_actualexpendituresyeartodate { get; set; }

        /// <summary>
        /// Annual variance (annual budget − year-to-date); computed by the FE.
        /// Can be negative on overspend.
        /// </summary>
        public decimal vsd_yeartodatevariance { get; set; }

        /// <summary>Free-text explanation for variance; optional.</summary>
        public string vsd_explanationforvariance { get; set; }
    }
}
