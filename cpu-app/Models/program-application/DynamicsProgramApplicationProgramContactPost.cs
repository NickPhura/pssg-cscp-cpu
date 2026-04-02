using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsProgramApplicationProgramContactPost
    {
        [Required]
        public string contactid { get; set; }
        [Required]
        public string vsd_programid { get; set; }
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_contact_vsd_program"; } }
    }
}
