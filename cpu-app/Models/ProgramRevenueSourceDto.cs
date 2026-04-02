using System;

namespace Gov.Cscp.Victims.Public.Models
{
    public class ProgramRevenueSourceDto
    {
        public string TransactionCurrencyIdValue { get; set; }
        public string Vsd_ProgramIdValue { get; set; }
        public decimal? Vsd_CashContribution { get; set; }
        public string Vsd_Cpu_OtherRevenueSource { get; set; }
        public int? Vsd_Cpu_RevenueSourceType { get; set; }
        public decimal? Vsd_InKindContribution { get; set; }
        public string Vsd_ProgramRevenueSourceId { get; set; }
        public int? StateCode { get; set; }
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
