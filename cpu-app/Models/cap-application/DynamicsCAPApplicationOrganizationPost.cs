using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
	/// <summary>
	/// Carries organisation identity and mailing address fields sent to the Dynamics <c>account</c> entity.
	/// </summary>
	public class DynamicsCAPApplicationOrganizationPost
	{
		// ── Dynamics entity type tag ──────────────────────────────────────────────
		public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.account"; } }

		/// <summary>Internal owner reference; set by Dynamics, not required from the portal.</summary>
		public string _ownerid_value { get; set; }

		/// <summary>GUID of the Dynamics <c>account</c> record to update. Always required.</summary>
		[Required(ErrorMessage = "accountid (organisation GUID) is required.")]
		public string accountid { get; set; }

		/// <summary>Organisation display name.</summary>
		public string name { get; set; }

		/// <summary>Mailing address – street line 1. Validated as required on the FE (ContactInformation).</summary>
		[Required(ErrorMessage = "address1_line1 is required.")]
		public string address1_line1 { get; set; }

		/// <summary>Mailing address – street line 2 (optional).</summary>
		public string address1_line2 { get; set; }

		/// <summary>Mailing address – city. Validated as required on the FE.</summary>
		[Required(ErrorMessage = "address1_city is required.")]
		public string address1_city { get; set; }

		/// <summary>Mailing address – postal code. Validated as required on the FE.</summary>
		[Required(ErrorMessage = "address1_postalcode is required.")]
		public string address1_postalcode { get; set; }

		/// <summary>Mailing address – province / state.</summary>
		public string address1_stateorprovince { get; set; }

		/// <summary>Mailing address – country.</summary>
		public string address1_country { get; set; }

		/// <summary>Full composite address string; computed by Dynamics, not submitted by the portal.</summary>
		public string address1_composite { get; set; }
	}
}
