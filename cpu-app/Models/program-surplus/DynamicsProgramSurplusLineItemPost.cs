using System;
using System.ComponentModel.DataAnnotations;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsProgramSurplusLineItemPost
    {
        public string vsd_justificationdetails { get; set; }
        [Range(typeof(decimal), "0", "10000000")]
        public decimal? vsd_actualexpenditures { get; set; }
        [Range(typeof(decimal), "0", "10000000")]
        public decimal? vsd_actualexpenditures2 { get; set; }
        [Range(typeof(decimal), "0", "10000000")]
        public decimal? vsd_actualexpenditures3 { get; set; }
        [Range(typeof(decimal), "0", "10000000")]
        public decimal? vsd_actualexpenditures4 { get; set; }
        [Range(typeof(decimal), "0", "10000000")]
        public decimal? vsd_allocatedamount { get; set; }
        [Range(typeof(decimal), "0", "10000000")]
        public decimal? vsd_proposedexpenditures { get; set; }
        [Required]
        public string vsd_surpluslineitemid { get; set; }
        public DateTime? vsd_datesubmitted { get; set; }
        // public string vsd_surplusplanid { get; set; }
        public string fortunecookietype { get { return "#Microsoft.Dynamics.CRM.vsd_surpluslineitem"; } }
    }
}
