using System;

namespace Gov.Cscp.Victims.Public.Models
{
    public class ProgramBudgetDto
    {
        public string TransactionCurrencyIdValue { get; set; }
        public string Vsd_ContactLookupValue { get; set; }
        public string Vsd_ContractIdValue { get; set; }
        public string Vsd_ProgramTypeValue { get; set; }
        public string Vsd_ServiceProviderIdValue { get; set; }
        public int? StatusCode { get; set; }
        public decimal? Vsd_Cpu_PercentOfTotalAdminCostsFromVscp { get; set; }
        public decimal? Vsd_Cpu_PercentOfTotalProgramDeliveryFromVscp { get; set; }
        public decimal? Vsd_Cpu_PercentOfTotalSalaryBenefitsVscp { get; set; }
        public decimal? Vsd_Cpu_TotalAdministrationCosts { get; set; }
        public decimal? Vsd_Cpu_TotalAdministrationCostsFromVscp { get; set; }
        public decimal? Vsd_Cpu_TotalCashContributions { get; set; }
        public decimal? Vsd_Cpu_TotalInKindContributions { get; set; }
        public decimal? Vsd_Cpu_TotalProgramDeliveryCosts { get; set; }
        public decimal? Vsd_Cpu_TotalProgramDeliveryFromVscp { get; set; }
        public decimal? Vsd_Cpu_TotalRevenueAmounts { get; set; }
        public decimal? Vsd_Cpu_TotalSalariesAndBenefits { get; set; }
        public decimal? Vsd_Cpu_TotalSalariesAndBenefitsFromVscp { get; set; }
        public string Vsd_EmailAddress { get; set; }
        public string Vsd_Name { get; set; }
        public string Vsd_ProgramId { get; set; }
        public string Vsd_SigningOfficerSignature { get; set; }
        public string Vsd_SigningOfficerFullName { get; set; }
        public string Vsd_SigningOfficerTitle { get; set; }
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
