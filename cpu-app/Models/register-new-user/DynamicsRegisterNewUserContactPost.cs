using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsRegisterNewUserContactPost
    {
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.contact"; } }
        [Required]
        public string address1_city { get; set; }
        public string address1_composite { get; set; }
        [Required]
        public string address1_line1 { get; set; }
        public string address1_line2 { get; set; }
        [Required]
        public string address1_postalcode { get; set; }
        public string address1_stateorprovince { get; set; }
        [Required]
        public string emailaddress1 { get; set; }
        public string fax { get; set; }
        [Required]
        public string firstname { get; set; }
        public string fullname { get; set; }
        // Role is always expected: 100000005 = Staff, 100000007 = Contractor
        [Range(100000000, int.MaxValue)]
        public int vsd_contactrole { get; set; }
        [Required]
        public string jobtitle { get; set; }
        [Required]
        public string lastname { get; set; }
        public string middlename { get; set; }
        [Required]
        public string mobilephone { get; set; }
        public string vsd_mainphoneextension { get; set; }
        public string telephone2 { get; set; }
        public string vsd_homephoneextension { get; set; }
        //public int? vsd_employmentstatus { get; set; }
    }
}
