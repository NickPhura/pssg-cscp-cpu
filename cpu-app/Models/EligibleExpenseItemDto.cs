using System;

namespace Gov.Cscp.Victims.Public.Models
{
    public class EligibleExpenseItemDto
    {
        public string Vsd_EligibleExpenseItemId { get; set; }
        public string Vsd_Name { get; set; }
        public int? Vsd_ProgramExpenseType { get; set; }
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
