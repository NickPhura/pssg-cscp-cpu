using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class DynamicsDataCollectionLineItemPost
    {
        public string fortunecookietype
        {
            get { return "#Microsoft.Dynamics.CRM.vsd_datacollectionlineitem"; }
        }
        public string vsd_name { get; set; }
        public string _vsd_questionid_value { set; get; }
        public string vsd_questioncategory { get; set; }
        public decimal? vsd_number { get; set; }
        [Range(100000000, 100000002)]
        public int vsd_questiontype1 { get; set; }
        public int vsd_questionorder { get; set; }
        public string vsd_textanswer { get; set; }
        [Range(100000000, 100000001)]
        public int? vsd_yesno { get; set; }

        private string _vsd_QuestionIdfortunecookiebind;
        [Required]
        public string vsd_QuestionIdfortunecookiebind
        {
            get
            {
                if (_vsd_QuestionIdfortunecookiebind != null)
                {
                    return "/vsd_cpustatisticsmasterdatas(" + _vsd_QuestionIdfortunecookiebind + ")";
                }
                else
                {
                    return null;
                }
            }
            set { _vsd_QuestionIdfortunecookiebind = value; }
        }

        private string _vsd_CategoryIdfortunecookiebind;
        public string vsd_CategoryIdfortunecookiebind
        {
            get
            {
                if (_vsd_CategoryIdfortunecookiebind != null)
                {
                    return "/vsd_monthlystatisticscategories(" + _vsd_CategoryIdfortunecookiebind + ")";
                }
                else
                {
                    return null;
                }
            }
            set { _vsd_CategoryIdfortunecookiebind = value; }
        }

        public string GetAnswer()
        {
            if (vsd_yesno != null)
            {
                return vsd_yesno == 100000001 ? "true" : "false";
            }
            if (!vsd_textanswer.IsNullOrEmpty())
            {
                return vsd_textanswer;
            }
            return vsd_number.ToString();
        }

    }
}
