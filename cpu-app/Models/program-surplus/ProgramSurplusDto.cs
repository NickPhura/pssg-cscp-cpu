using System;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>Returned by GET /api/ProgramSurplus/{businessBceid}/{userBceid}/{surplusId}</summary>
    public class ProgramSurplusDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public string Businessbceid { get; set; }
        public string Userbceid { get; set; }
        public ContractDto Contract { get; set; }
        public ProgramDto Program { get; set; }
        public OrganizationDto Organization { get; set; }
        public SurplusPlanDto SurplusPlan { get; set; }
        public SurplusLineItemDto[] SurplusPlanLineItems { get; set; }
        public EligibleExpenseItemDto[] EligibleExpenseItemCollection { get; set; }
    }

    public class SurplusPlanDto
    {
        public string Vsd_SurplusPlanReportId { get; set; }
        public decimal? Vsd_SurplusAmount { get; set; }
        public bool? Vsd_SurplusRemittance { get; set; }
        public DateTime? Vsd_DateSubmitted { get; set; }
        public string Vsd_ProgramIdValue { get; set; }
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }

    public class SurplusLineItemDto
    {
        public string Vsd_SurplusLineItemId { get; set; }
        public string Vsd_Name { get; set; }
        public string Vsd_JustificationDetails { get; set; }
        public decimal? Vsd_ActualExpenditures { get; set; }
        public decimal? Vsd_ActualExpenditures2 { get; set; }
        public decimal? Vsd_ActualExpenditures3 { get; set; }
        public decimal? Vsd_ActualExpenditures4 { get; set; }
        public decimal? Vsd_AllocatedAmount { get; set; }
        public decimal? Vsd_ProposedExpenditures { get; set; }
        public DateTime? Vsd_DateSubmitted { get; set; }
        public string Vsd_EligibleExpenseItemIdValue { get; set; }
        public string Vsd_SurplusPlanIdValue { get; set; }
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }

    /// <summary>Returned by POST /api/ProgramSurplus</summary>
    public class SetSurplusResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
    }
}
