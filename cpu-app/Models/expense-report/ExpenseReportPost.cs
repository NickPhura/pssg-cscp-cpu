using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
	public class ExpenseReportPost
	{
		/// <summary>Organisation BCeID that owns the contract; sourced from session state.</summary>
		[Required(ErrorMessage = "BusinessBCeID is required.")]
		public string BusinessBCeID { get; set; }

		/// <summary>User BCeID of the person submitting the report; sourced from session state.</summary>
		[Required(ErrorMessage = "UserBCeID is required.")]
		public string UserBCeID { get; set; }

		/// <summary>
		/// Always contains exactly one entry representing the Schedule G record being updated.
		/// The converter always builds a single <c>sg</c> object and wraps it in an array.
		/// </summary>
		[Required(ErrorMessage = "ScheduleGCollection is required.")]
		[MinLength(1, ErrorMessage = "ScheduleGCollection must contain at least one Schedule G entry.")]
		public DynamicsScheduleGCollectionPost[] ScheduleGCollection { get; set; }

		/// <summary>
		/// One entry per programme expense line item. Always populated by the converter
		/// (may be an empty array when the programme has no line items).
		/// </summary>
		[Required(ErrorMessage = "ScheduleGLineItemCollection is required.")]
		public DynamicsScheduleGLineItemCollectionPost[] ScheduleGLineItemCollection { get; set; }
	}
}
