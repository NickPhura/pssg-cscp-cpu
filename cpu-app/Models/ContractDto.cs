#nullable enable
using System;
using System.Collections.Generic;

namespace Gov.Cscp.Victims.Public.Models;

public class ContractDto
{
    public string? Vsd_ContractId { get; set; }
    public string? Vsd_Name { get; set; }
    public DateTime? Vsd_FiscalStartDate { get; set; }
    public int? StatusCode { get; set; }
    // Lookup ID values
    public string? Vsd_ContactLookup1IdValue { get; set; }
    public string? Vsd_ContactLookup2IdValue { get; set; }
    // Administrative fields
    public int? Vsd_Cpu_InsuranceOptions { get; set; }
    public int? Vsd_Cpu_MemberOfCssea { get; set; }
    public int[]? Vsd_Cpu_HumanResourcePolices { get; set; }
    public string? Vsd_Cpu_SpecificUnion { get; set; }
    public int? Vsd_Cpu_SubcontractedProgramStaff { get; set; }
    public int? Vsd_Cpu_UnionizedStaff { get; set; }
    // CAP-specific fields
    public int? Vsd_CollaborationWithKeyStakeholders { get; set; }
    public int? Vsd_ComplaintAndFeedbackProcessForParticipant { get; set; }
    public bool? Vsd_CriminalRecordChecks { get; set; }
    public int? Vsd_LetterOfReferenceFromReferralSources { get; set; }
    public int? Vsd_EstablishedConfidentialityGuidelines { get; set; }
    public string? FortuneCookieType { get; set; }
    public string? FortuneCookieEtag { get; set; }
}
