using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Associates (or disassociates) a contact with a programme in the
    /// <c>vsd_contact_vsd_program</c> many-to-many intersection entity.
    /// Both GUIDs are required – the mapper silently skips any entry where either
    /// cannot be parsed, so we enforce them here to catch data-quality issues early.
    /// </summary>
    public class DynamicsCAPApplicationProgramContactPost
    {
        // ── Dynamics entity type tag ──────────────────────────────────────────────
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_contact_vsd_program"; } }

        /// <summary>GUID of the contact (person) to link. Required.</summary>
        [Required(ErrorMessage = "contactid (contact GUID) is required.")]
        public string contactid { get; set; }

        /// <summary>GUID of the programme to link the contact to. Required.</summary>
        [Required(ErrorMessage = "vsd_programid (programme GUID) is required.")]
        public string vsd_programid { get; set; }
    }
}
