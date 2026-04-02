using System;

namespace Gov.Cscp.Victims.Public.Models
{
    public class ProgramExpenseDto
    {
        public string TransactionCurrencyIdValue { get; set; }
        public string Vsd_EligibleExpenseItemIdValue { get; set; }
        public string Vsd_ProgramIdValue { get; set; }
        public string Vsd_ProgramExpenseId { get; set; }
        public string Vsd_Cpu_TitlePosition { get; set; }
        public decimal? Vsd_Cpu_Benefits { get; set; }
        public decimal? Vsd_Cpu_FundedFromVscp { get; set; }
        public int? Vsd_Cpu_ProgramExpenseType { get; set; }
        public decimal? Vsd_Cpu_Salary { get; set; }
        public decimal? Vsd_InputAmount { get; set; }
        public decimal? Vsd_TotalCost { get; set; }
        public string Vsd_Cpu_OtherExpense { get; set; }
        public int? StateCode { get; set; }
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
