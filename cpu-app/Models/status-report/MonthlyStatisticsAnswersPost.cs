using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
	public class MonthlyStatisticsAnswers
	{
		[Required]
		public string BusinessBCeID { get; set; }
		[Required]
		public string UserBCeID { get; set; }
		// public int ReportingPeriod { get; set; }
		public int? StatusCode { get; set; }
		//public string DataCollectionid { get; set; }
		public DynamicsDataCollectionLineItemPost[] AnswerCollection { get; set; }
	}
}
