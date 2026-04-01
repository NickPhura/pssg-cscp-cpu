using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsProgramApplicationSchedulePost
    {
        public string fortunecookietype { get { return "Microsoft.Dynamics.CRM.vsd_schedule"; } }
        public string vsd_days { get; set; }
        public string vsd_scheduledendtime { get; set; }
        public string vsd_scheduledstarttime { get; set; }
        public string vsd_scheduleid { get; set; }
        public int vsd_cpu_scheduletype { get; set; }
        [Range(0, 1)]
        public int? statecode { get; set; }
        private string _vsd_ProgramIdfortunecookiebind;
        [Required]
        public string vsd_ProgramIdfortunecookiebind
        {
            get
            {
                if (_vsd_ProgramIdfortunecookiebind != null)
                {
                    return "/vsd_programs(" + _vsd_ProgramIdfortunecookiebind + ")";
                }
                else
                {
                    return null;
                }
            }
            set { _vsd_ProgramIdfortunecookiebind = value; }
        }
    }
}