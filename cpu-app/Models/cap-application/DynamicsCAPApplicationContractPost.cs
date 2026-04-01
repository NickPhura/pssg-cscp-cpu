using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Carries the contract-level fields sent to the Dynamics <c>vsd_contract</c> entity.
    /// </summary>
    public class DynamicsCAPApplicationContractPost
    {
        // ── Dynamics YesNo option-set bounds ─────────────────────────────────────
        private const int YesNoMin = 100000000; // No
        private const int YesNoMax = 100000001; // Yes

        // ── Dynamics entity type tag ──────────────────────────────────────────────
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_contract"; } }

        /// <summary>GUID of the Dynamics <c>vsd_contract</c> record. Always required.</summary>
        [Required(ErrorMessage = "vsd_contractid (contract GUID) is required.")]
        public string vsd_contractid { get; set; }

        /// <summary>Human-readable contract reference number.</summary>
        public string vsd_name { get; set; }

        /// <summary>Base-64 signature image; populated only on final submission.</summary>
        public string vsd_authorizedsigningofficersignature { get; set; }

        /// <summary>Full name of the signing officer; populated only on final submission.</summary>
        public string vsd_signingofficersname { get; set; }

        /// <summary>Title of the signing officer; populated only on final submission.</summary>
        public string vsd_signingofficertitle { get; set; }

        /// <summary>Insurance option: 100000000 = No, 100000001 = Yes.</summary>
        [Range(YesNoMin, YesNoMax,
            ErrorMessage = "vsd_cpu_insuranceoptions must be 100000000 (No) or 100000001 (Yes).")]
        public int? vsd_cpu_insuranceoptions { get; set; }

        /// <summary>Collaboration with key stakeholders: 100000000 = No, 100000001 = Yes.</summary>
        [Range(YesNoMin, YesNoMax,
            ErrorMessage = "vsd_collaborationwithkeystakeholders must be 100000000 (No) or 100000001 (Yes).")]
        public int? vsd_collaborationwithkeystakeholders { get; set; }

        /// <summary>Complaint and feedback process for participants: 100000000 = No, 100000001 = Yes.</summary>
        [Range(YesNoMin, YesNoMax,
            ErrorMessage = "vsd_complaintandfeedbackprocessforparticipant must be 100000000 (No) or 100000001 (Yes).")]
        public int? vsd_complaintandfeedbackprocessforparticipant { get; set; }

        /// <summary>Whether criminal record checks are conducted. Always populated by the form.</summary>
        public bool vsd_criminalrecordchecks { get; set; }

        /// <summary>Letter of reference from referral sources: 100000000 = No, 100000001 = Yes.</summary>
        [Range(YesNoMin, YesNoMax,
            ErrorMessage = "vsd_letterofreferencefromreferralsources must be 100000000 (No) or 100000001 (Yes).")]
        public int? vsd_letterofreferencefromreferralsources { get; set; }

        /// <summary>Established confidentiality guidelines: 100000000 = No, 100000001 = Yes.</summary>
        [Range(YesNoMin, YesNoMax,
            ErrorMessage = "vsd_establishedconfidentialityguidelines must be 100000000 (No) or 100000001 (Yes).")]
        public int? vsd_establishedconfidentialityguidelines { get; set; }
    }
}
