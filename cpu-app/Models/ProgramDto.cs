#nullable enable
using System;

namespace Gov.Cscp.Victims.Public.Models;

public class ProgramDto
{
    public string? Vsd_ProgramId { get; set; }
    public string? Vsd_Name { get; set; }
    public string? Vsd_ContractIdValue { get; set; }
    // Contact lookup values
    public string? Vsd_ContactLookupValue { get; set; }
    public string? Vsd_ContactLookup2Value { get; set; }
    public string? Vsd_ContactLookup3Value { get; set; }
    // Main address
    public string? Vsd_AddressLine1 { get; set; }
    public string? Vsd_AddressLine2 { get; set; }
    public string? Vsd_City { get; set; }
    public string? Vsd_PostalCodeZip { get; set; }
    public string? Vsd_ProvinceState { get; set; }
    public string? Vsd_Country { get; set; }
    // Mailing address
    public string? Vsd_MailingAddressLine1 { get; set; }
    public string? Vsd_MailingAddressLine2 { get; set; }
    public string? Vsd_MailingCity { get; set; }
    public string? Vsd_MailingPostalCodeZip { get; set; }
    public string? Vsd_MailingProvinceState { get; set; }
    public string? Vsd_MailingCountry { get; set; }
    // Contact info
    public string? Vsd_EmailAddress { get; set; }
    public string? Vsd_Fax { get; set; }
    public string? Vsd_PhoneNumber { get; set; }
    public string? Vsd_GovernmentFunderAgency { get; set; }
    // Flags and option sets
    public bool? Vsd_CostShare { get; set; }
    public bool? Vsd_AddressTransitionOrSafeHome { get; set; }
    public bool? Vsd_Cpu_ProgramStaffSubcontracted { get; set; }
    public int? Vsd_Cpu_Per { get; set; }
    public int? Vsd_Cpu_NumberOfHours { get; set; }
    public int? Vsd_TotalScheduledHours { get; set; }
    public int? Vsd_TotalOnCallStandbyHours { get; set; }
    public string? Vsd_Cpu_CapProgramOperationsComments { get; set; }
    public decimal? vsd_Cpu_FoundingAmountRequested { get; set; }
    public decimal? Vsd_Cpu_EstimatedSubtotalComponentValue { get; set; }
    // Lookup IDs for type, region, location
    public string? Vsd_ProgramTypeValue { get; set; }
    public string? Vsd_Cpu_RegionDistrictValue { get; set; }
    public string? Vsd_Cpu_Program_Location { get; set; }
    public string? FortuneCookieType { get; set; }
    public string? FortuneCookieEtag { get; set; }
    public string? Vsd_Cpu_ProgramModelTypes { get; set; }
    public int? Vsd_Cpu_ProgramEvaluationEfforts { get; set; }
    public string? Vsd_Cpu_ProgramEvaluationEffortsDescription { get; set; }
}
