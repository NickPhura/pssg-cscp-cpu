using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
	public class OrganizationPost : IValidatableObject
	{
		// this is the model that Dynamics expects back to update the organization level information
		[Required]
		public string BusinessBCeID { get; set; }
		[Required]
		public string UserBCeID { get; set; }
		// Either Organization or StaffCollection must be provided — validated below.
		public DynamicsOrganizationPost Organization { get; set; }
		public DynamicsOrganizationContactPost[] StaffCollection { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Organization == null && (StaffCollection == null || StaffCollection.Length == 0))
			{
				yield return new ValidationResult(
					"At least one of Organization or StaffCollection must be provided.",
					new[] { nameof(Organization), nameof(StaffCollection) }
				);
			}
		}
	}
}
