using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Carries per-programme metadata (programme reference + optional signing-officer
    /// details populated only at final submission).
    /// </summary>
    public class DynamicsBudgetProposalProgramPost
    {
        // ── Dynamics entity type tag ────────────────────────────────────────────
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_program"; } }

        /// <summary>GUID of the Dynamics <c>vsd_program</c> record. Always required.</summary>
        [Required(ErrorMessage = "vsd_programid (programme GUID) is required.")]
        public string vsd_programid { get; set; }

        /// <summary>Base-64 signature image; populated only when the user submits (not on save).</summary>
        public string vsd_signingofficersignature { get; set; }

        /// <summary>Full name of the signing officer; populated only on final submission.</summary>
        public string vsd_signingofficerfullname { get; set; }

        /// <summary>Title/role of the signing officer; populated only on final submission.</summary>
        public string vsd_signingofficertitle { get; set; }
    }
}
