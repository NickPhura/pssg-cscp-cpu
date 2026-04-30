using Database.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class EntityToDtoMapper
    {
        public static OrganizationDto ToOrganizationDto(Entity entity)
        {
            if (entity == null) return null;

            return new OrganizationDto
            {
                AccountId = entity.GetAttributeValue<Guid>("accountid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("accountid").ToString()
                    : entity.Id.ToString(),
                Name = entity.GetAttributeValue<string>("name"),
                Telephone1 = entity.GetAttributeValue<string>("telephone1"),
                EmailAddress1 = entity.GetAttributeValue<string>("emailaddress1"),
                Fax = entity.GetAttributeValue<string>("fax"),
                Address1City = entity.GetAttributeValue<string>("address1_city"),
                Address1Line1 = entity.GetAttributeValue<string>("address1_line1"),
                Address1Line2 = entity.GetAttributeValue<string>("address1_line2"),
                Address1PostalCode = entity.GetAttributeValue<string>("address1_postalcode"),
                Address1StateOrProvince = entity.GetAttributeValue<string>("address1_stateorprovince"),
                Address1Country = entity.GetAttributeValue<string>("address1_country"),
                Address2City = entity.GetAttributeValue<string>("address2_city"),
                Address2Line1 = entity.GetAttributeValue<string>("address2_line1"),
                Address2Line2 = entity.GetAttributeValue<string>("address2_line2"),
                Address2PostalCode = entity.GetAttributeValue<string>("address2_postalcode"),
                Address2StateOrProvince = entity.GetAttributeValue<string>("address2_stateorprovince"),
                Address2Country = entity.GetAttributeValue<string>("address2_country"),
                ExecutiveContactIdValue = GetEntityReferenceValue(entity, "vsd_executivecontactid"),
                BoardContactIdValue = GetEntityReferenceValue(entity, "vsd_boardcontactid"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
            };
        }

        public static ContactDto ToContactDto(Entity entity)
        {
            if (entity == null) return null;

            return new ContactDto
            {
                ContactId = entity.GetAttributeValue<Guid>("contactid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("contactid").ToString()
                    : entity.Id.ToString(),
                FirstName = entity.GetAttributeValue<string>("firstname"),
                LastName = entity.GetAttributeValue<string>("lastname"),
                MiddleName = entity.GetAttributeValue<string>("middlename"),
                EmailAddress1 = entity.GetAttributeValue<string>("emailaddress1"),
                MobilePhone = entity.GetAttributeValue<string>("mobilephone"),
                Fax = entity.GetAttributeValue<string>("fax"),
                JobTitle = entity.GetAttributeValue<string>("jobtitle"),
                Address1City = entity.GetAttributeValue<string>("address1_city"),
                Address1Line1 = entity.GetAttributeValue<string>("address1_line1"),
                Address1Line2 = entity.GetAttributeValue<string>("address1_line2"),
                Address1PostalCode = entity.GetAttributeValue<string>("address1_postalcode"),
                Address1StateOrProvince = entity.GetAttributeValue<string>("address1_stateorprovince"),
                Vsd_BceId = entity.GetAttributeValue<string>("vsd_bceid"),
                Vsd_MainPhoneExtension = entity.GetAttributeValue<string>("vsd_mainphoneextension"),
                Telephone2 = entity.GetAttributeValue<string>("telephone2"),
                Vsd_HomePhoneExtension = entity.GetAttributeValue<string>("vsd_homephoneextension"),
                Vsd_EmploymentStatus = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_employmentstatus")?.Value,
                StateCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
            };
        }

        public static PortalRoleDto ToPortalRoleDto(Entity entity)
        {
            if (entity == null) return null;

            return new PortalRoleDto
            {
                Vsd_PortalRoleId = entity.GetAttributeValue<Guid>("vsd_portalroleid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_portalroleid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
            };
        }

        public static TaskDto ToTaskDto(Entity entity)
        {
            if (entity == null) return null;

            return new TaskDto
            {
                ActivityId = entity.GetAttributeValue<Guid>("activityid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("activityid").ToString()
                    : entity.Id.ToString(),
                RegardingObjectIdValue = GetEntityReferenceValue(entity, "regardingobjectid"),
                StatusCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode")?.Value,
                StateCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode")?.Value,
                Vsd_TaskTypeIdValue = GetEntityReferenceValue(entity, "vsd_tasktypeid"),
                Subject = entity.GetAttributeValue<string>("subject"),
                Description = entity.GetAttributeValue<string>("description"),
                ScheduledEnd = entity.GetAttributeValue<DateTime?>("scheduledend"),
                ModifiedOn = entity.GetAttributeValue<DateTime?>("modifiedon"),
                Vsd_ProgramIdValue = GetEntityReferenceValue(entity, "vsd_programid"),
                Vsd_ScheduleGIdValue = GetEntityReferenceValue(entity, "vsd_schedulegid"),
                Vsd_SurplusPlanIdValue = GetEntityReferenceValue(entity, "vsd_surplusplanid"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
            };
        }

        public static ProgramDto ToProgramDto(Entity entity)
        {
            if (entity == null) return null;

            return new ProgramDto
            {
                Vsd_ProgramId = entity.GetAttributeValue<Guid>("vsd_programid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_ContractIdValue = GetEntityReferenceValue(entity, "vsd_contractid"),
                Vsd_ContactLookupValue = GetEntityReferenceValue(entity, "vsd_contactlookup"),
                Vsd_ContactLookup2Value = GetEntityReferenceValue(entity, "vsd_contactlookup2"),
                Vsd_ContactLookup3Value = GetEntityReferenceValue(entity, "vsd_contactlookup3"),
                Vsd_AddressLine1 = entity.GetAttributeValue<string>("vsd_addressline1"),
                Vsd_AddressLine2 = entity.GetAttributeValue<string>("vsd_addressline2"),
                Vsd_City = entity.GetAttributeValue<string>("vsd_city"),
                Vsd_PostalCodeZip = entity.GetAttributeValue<string>("vsd_postalcodezip"),
                Vsd_ProvinceState = entity.GetAttributeValue<string>("vsd_provincestate"),
                Vsd_Country = entity.GetAttributeValue<string>("vsd_country"),
                Vsd_MailingAddressLine1 = entity.GetAttributeValue<string>("vsd_mailingaddressline1"),
                Vsd_MailingAddressLine2 = entity.GetAttributeValue<string>("vsd_mailingaddressline2"),
                Vsd_MailingCity = entity.GetAttributeValue<string>("vsd_mailingcity"),
                Vsd_MailingPostalCodeZip = entity.GetAttributeValue<string>("vsd_mailingpostalcodezip"),
                Vsd_MailingProvinceState = entity.GetAttributeValue<string>("vsd_mailingprovincestate"),
                Vsd_MailingCountry = entity.GetAttributeValue<string>("vsd_mailingcountry"),
                Vsd_EmailAddress = entity.GetAttributeValue<string>("vsd_emailaddress"),
                Vsd_Fax = entity.GetAttributeValue<string>("vsd_fax"),
                Vsd_PhoneNumber = entity.GetAttributeValue<string>("vsd_phonenumber"),
                Vsd_GovernmentFunderAgency = entity.GetAttributeValue<string>("vsd_governmentfunderagency"),
                Vsd_CostShare = entity.GetAttributeValue<bool?>("vsd_costshare"),
                Vsd_AddressTransitionOrSafeHome = entity.GetAttributeValue<bool?>("vsd_addresstransitionorsafehome"),
                Vsd_Cpu_ProgramStaffSubcontracted = entity.GetAttributeValue<bool?>("vsd_cpu_programstaffsubcontracted"),
                Vsd_Cpu_Per = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_per")?.Value,
                Vsd_Cpu_NumberOfHours = (int?)entity.GetAttributeValue<decimal?>("vsd_cpu_numberofhours"),
                Vsd_TotalScheduledHours = (int?)entity.GetAttributeValue<decimal?>("vsd_totalscheduledhours"),
                Vsd_TotalOnCallStandbyHours = (int?)entity.GetAttributeValue<decimal?>("vsd_totaloncallstandbyhours"),
                Vsd_Cpu_EstimatedSubtotalComponentValue = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_estimatedsubtotalcomponentvalue")?.Value,
                Vsd_ProgramTypeValue = GetEntityReferenceValue(entity, "vsd_programtype"),
                Vsd_Cpu_RegionDistrictValue = GetEntityReferenceValue(entity, "vsd_cpu_regiondistrict"),
                Vsd_Cpu_Program_Location = entity.GetAttributeValue<string>("vsd_cpu_program_location"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion,
                Vsd_Cpu_CapProgramOperationsComments = entity.GetAttributeValue<string>("vsd_cpu_capprogramoperationscomments"),
                vsd_Cpu_FoundingAmountRequested = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_fundingamountrequested")?.Value,
                Vsd_Cpu_ProgramModelTypes = entity.Contains("vsd_cpu_programmodeltypes") && entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection>("vsd_cpu_programmodeltypes") != null
                    ? string.Join(",", entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection>("vsd_cpu_programmodeltypes").Select(v => v.Value))
                    : null,
                Vsd_Cpu_ProgramEvaluationEfforts = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_programevaluationefforts")?.Value,
                Vsd_Cpu_ProgramEvaluationEffortsDescription = entity.GetAttributeValue<string>("vsd_cpu_programevaluationeffortsdescription")

            };
        }

        public static SystemUserDto ToSystemUserDto(Entity entity)
        {
            if (entity == null) return null;

            return new SystemUserDto
            {
                FirstName = entity.GetAttributeValue<string>("firstname"),
                LastName = entity.GetAttributeValue<string>("lastname"),
                InternalEmailAddress = entity.GetAttributeValue<string>("internalemailaddress"),
                Address1_Telephone1 = entity.GetAttributeValue<string>("address1_telephone1"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
            };
        }

        public static ContractDto ToContractDto(Entity entity)
        {
            if (entity == null) return null;
            return new ContractDto
            {
                Vsd_ContractId = entity.GetAttributeValue<Guid>("vsd_contractid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_contractid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_FiscalStartDate = entity.GetAttributeValue<DateTime?>("vsd_fiscalstartdate"),
                StatusCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode")?.Value,
                Vsd_ContactLookup1IdValue = GetEntityReferenceValue(entity, "vsd_contactlookup1"),
                Vsd_ContactLookup2IdValue = GetEntityReferenceValue(entity, "vsd_contactlookup2"),
                Vsd_Cpu_InsuranceOptions = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_insuranceoptions")?.Value,
                Vsd_Cpu_MemberOfCssea = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_memberofcssea")?.Value,
                Vsd_Cpu_HumanResourcePolices = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection>("vsd_cpu_humanresourcepolices")?.Select(v => v.Value).ToArray(),
                Vsd_Cpu_SpecificUnion = entity.GetAttributeValue<string>("vsd_cpu_specificunion"),
                Vsd_Cpu_SubcontractedProgramStaff = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_subcontractedprogramstaff")?.Value,
                Vsd_Cpu_UnionizedStaff = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_unionizedstaff")?.Value,
                // CAP-specific fields
                Vsd_CollaborationWithKeyStakeholders = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_collaborationwithkeystakeholders")?.Value,
                Vsd_ComplaintAndFeedbackProcessForParticipant = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_complaintandfeedbackprocessforparticipant")?.Value,
                Vsd_CriminalRecordChecks = entity.GetAttributeValue<bool?>("vsd_criminalrecordchecks"),
                Vsd_LetterOfReferenceFromReferralSources = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_letterofreferencefromreferralsources")?.Value,
                Vsd_EstablishedConfidentialityGuidelines = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_establishedconfidentialityguidelines")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
            };
        }

        public static InvoiceDto ToInvoiceDto(Entity entity)
        {
            if (entity == null) return null;

            return new InvoiceDto
            {
                Vsd_InvoiceId = entity.GetAttributeValue<Guid>("vsd_invoiceid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_invoiceid").ToString()
                    : entity.Id.ToString(),
                Vsd_ProgramIdValue = GetEntityReferenceValue(entity, "vsd_programid"),
                Vsd_Cpu_ScheduledPaymentDate = entity.GetAttributeValue<DateTime?>("vsd_cpu_scheduledpaymentdate"),
                StatusCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
            };
        }

        public static ProgramExpenseDto ToProgramExpenseDto(Entity entity)
        {
            if (entity == null) return null;

            return new ProgramExpenseDto
            {
                Vsd_ProgramExpenseId = entity.GetAttributeValue<Guid>("vsd_programexpenseid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programexpenseid").ToString()
                    : entity.Id.ToString(),
                TransactionCurrencyIdValue = GetEntityReferenceValue(entity, "transactioncurrencyid"),
                Vsd_EligibleExpenseItemIdValue = GetEntityReferenceValue(entity, "vsd_eligibleexpenseitemid"),
                Vsd_ProgramIdValue = GetEntityReferenceValue(entity, "vsd_programid"),
                Vsd_Cpu_TitlePosition = entity.GetAttributeValue<string>("vsd_cpu_titleposition"),
                Vsd_Cpu_Benefits = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_benefits")?.Value,
                Vsd_Cpu_FundedFromVscp = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_fundedfromvscp")?.Value,
                Vsd_Cpu_ProgramExpenseType = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_programexpensetype")?.Value,
                Vsd_Cpu_Salary = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_salary")?.Value,
                Vsd_InputAmount = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_inputamount")?.Value,
                Vsd_TotalCost = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_totalcost")?.Value,
                Vsd_Cpu_OtherExpense = entity.GetAttributeValue<string>("vsd_cpu_otherexpense"),
                StateCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ProgramRevenueSourceDto ToProgramRevenueSourceDto(Entity entity)
        {
            if (entity == null) return null;

            return new ProgramRevenueSourceDto
            {
                Vsd_ProgramRevenueSourceId = entity.GetAttributeValue<Guid>("vsd_programrevenuesourceid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programrevenuesourceid").ToString()
                    : entity.Id.ToString(),
                TransactionCurrencyIdValue = GetEntityReferenceValue(entity, "transactioncurrencyid"),
                Vsd_ProgramIdValue = GetEntityReferenceValue(entity, "vsd_programid"),
                Vsd_CashContribution = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cashcontribution")?.Value,
                Vsd_Cpu_OtherRevenueSource = entity.GetAttributeValue<string>("vsd_cpu_otherrevenuesource"),
                Vsd_Cpu_RevenueSourceType = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_revenuesourcetype")?.Value,
                Vsd_InKindContribution = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_inkindcontribution")?.Value,
                StateCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static EligibleExpenseItemDto ToEligibleExpenseItemDto(Entity entity)
        {
            if (entity == null) return null;

            return new EligibleExpenseItemDto
            {
                Vsd_EligibleExpenseItemId = entity.GetAttributeValue<Guid>("vsd_eligibleexpenseitemid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_eligibleexpenseitemid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_ProgramExpenseType = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_programexpensetype")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ProgramTypeDto ToProgramTypeDto(Entity entity)
        {
            if (entity == null) return null;

            return new ProgramTypeDto
            {
                Vsd_ProgramTypeId = entity.GetAttributeValue<Guid>("vsd_programtypeid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programtypeid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_ProgramCategory = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_programcategory")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ProgramBudgetDto ToProgramBudgetDto(Entity entity)
        {
            if (entity == null) return null;

            return new ProgramBudgetDto
            {
                Vsd_ProgramId = entity.GetAttributeValue<Guid>("vsd_programid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programid").ToString()
                    : entity.Id.ToString(),
                TransactionCurrencyIdValue = GetEntityReferenceValue(entity, "transactioncurrencyid"),
                Vsd_ContactLookupValue = GetEntityReferenceValue(entity, "vsd_contactlookup"),
                Vsd_ContractIdValue = GetEntityReferenceValue(entity, "vsd_contractid"),
                Vsd_ProgramTypeValue = GetEntityReferenceValue(entity, "vsd_programtype"),
                Vsd_ServiceProviderIdValue = GetEntityReferenceValue(entity, "vsd_serviceproviderid"),
                StatusCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode")?.Value,
                Vsd_Cpu_PercentOfTotalAdminCostsFromVscp = entity.GetAttributeValue<decimal?>("vsd_cpu_percentoftotaladmincostsfromvscp"),
                Vsd_Cpu_PercentOfTotalProgramDeliveryFromVscp = entity.GetAttributeValue<decimal?>("vsd_cpu_percentoftotalprogramdeliveryfromvscp"),
                Vsd_Cpu_PercentOfTotalSalaryBenefitsVscp = entity.GetAttributeValue<decimal?>("vsd_cpu_percentoftotalsalarybenefitsvscp"),
                Vsd_Cpu_TotalAdministrationCosts = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totaladministrationcosts")?.Value,
                Vsd_Cpu_TotalAdministrationCostsFromVscp = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totaladministrationcostsfromvscp")?.Value,
                Vsd_Cpu_TotalCashContributions = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totalcashcontributions")?.Value,
                Vsd_Cpu_TotalInKindContributions = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totalinkindcontributions")?.Value,
                Vsd_Cpu_TotalProgramDeliveryCosts = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totalprogramdeliverycosts")?.Value,
                Vsd_Cpu_TotalProgramDeliveryFromVscp = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totalprogramdeliveryfromvscp")?.Value,
                Vsd_Cpu_TotalRevenueAmounts = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totalrevenueamounts")?.Value,
                Vsd_Cpu_TotalSalariesAndBenefits = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totalsalariesandbenefits")?.Value,
                Vsd_Cpu_TotalSalariesAndBenefitsFromVscp = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_cpu_totalsalariesandbenefitsfromvscp")?.Value,
                Vsd_EmailAddress = entity.GetAttributeValue<string>("vsd_emailaddress"),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_SigningOfficerSignature = entity.GetAttributeValue<string>("vsd_signingofficersignature"),
                Vsd_SigningOfficerFullName = entity.GetAttributeValue<string>("vsd_signingofficerfullname"),
                Vsd_SigningOfficerTitle = entity.GetAttributeValue<string>("vsd_signingofficertitle"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ContractBudgetDto ToContractBudgetDto(Entity entity)
        {
            if (entity == null) return null;

            return new ContractBudgetDto
            {
                Vsd_ContractId = entity.GetAttributeValue<Guid>("vsd_contractid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_contractid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_FiscalStartDate = entity.GetAttributeValue<DateTime?>("vsd_fiscalstartdate"),
                StatusCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode")?.Value,
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ScheduleDto ToScheduleDto(Entity entity)
        {
            if (entity == null) return null;

            return new ScheduleDto
            {
                Vsd_ScheduleId = entity.GetAttributeValue<Guid>("vsd_scheduleid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_scheduleid").ToString()
                    : entity.Id.ToString(),
                Vsd_Days = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection> ("vsd_days")?.Select(v=> v.Value).ToArray(),
                Vsd_ScheduledStartTime = entity.GetAttributeValue<string>("vsd_scheduledstarttime"),
                Vsd_ScheduledEndTime = entity.GetAttributeValue<string>("vsd_scheduledendtime"),
                Vsd_Cpu_ScheduleType = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_scheduletype")?.Value,
                StateCode = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode")?.Value,
                Vsd_ProgramIdValue = GetEntityReferenceValue(entity, "vsd_programid"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ProgramContactDto ToProgramContactDto(Entity entity)
        {
            if (entity == null) return null;

            return new ProgramContactDto
            {
                ContactId = GetEntityReferenceValue(entity, "contactid"),
                Vsd_ProgramId = GetEntityReferenceValue(entity, "vsd_programid"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static RegionDistrictDto ToRegionDistrictDto(Entity entity)
        {
            if (entity == null) return null;

            return new RegionDistrictDto
            {
                Vsd_RegionDistrictId = entity.GetAttributeValue<Guid>("vsd_regiondistrictid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_regiondistrictid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ServiceAreaDto ToServiceAreaDto(Entity entity)
        {
            if (entity == null) return null;

            return new ServiceAreaDto
            {
                Vsd_ServiceAreaId = entity.GetAttributeValue<Guid>("vsd_serviceareaid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_serviceareaid").ToString()
                    : entity.Id.ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_ProgramId = GetEntityReferenceValue(entity, "vsd_programid"),
                Vsd_RegionDistrictId = GetEntityReferenceValue(entity, "vsd_regiondistrictid"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ScheduleGDto ToScheduleGDto(Entity entity)
        {
            if (entity == null) return null;

            return new ScheduleGDto
            {
                Vsd_ScheduleGId = entity.GetAttributeValue<Guid>("vsd_schedulegid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_schedulegid").ToString()
                    : entity.Id.ToString(),
                Vsd_Cpu_ReportingPeriod = entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_cpu_reportingperiod")?.Value,
                Vsd_SalaryAndBenefitsBudgeted = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_salaryandbenefitsbudgeted")?.Value,
                Vsd_QuarterlyBudgetedSalariesBenefits = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlybudgetedsalariesbenefits")?.Value,
                Vsd_ProgramDeliveryBudgeted = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_programdeliverybudgeted")?.Value,
                Vsd_QuarterlyBudgetedProgramDelivery = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlybudgetedprogramdelivery")?.Value,
                Vsd_ProgramAdministrationBudgeted = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_programadministrationbudgeted")?.Value,
                Vsd_QuarterlyBudgetedProgramAdministration = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlybudgetedprogramadministration")?.Value,
                Vsd_ActualHoursThisQuarter = entity.GetAttributeValue<decimal?>("vsd_actualhoursthisquarter"),
                Vsd_ContractedServiceHrsThisQuarter = entity.GetAttributeValue<decimal?>("vsd_contractedservicehrsthisquarter"),
                Vsd_ProgramAdministrationCurrentQuarter = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_programadministrationcurrentquarter")?.Value,
                Vsd_QuarterlyVarianceProgramAdministration = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlyvarianceprogramadministration")?.Value,
                Vsd_YearToDateProgramAdministration = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_yeartodateprogramadministration")?.Value,
                Vsd_YearToDateVarianceProgramAdministration = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_yeartodatevarianceprogramadministration")?.Value,
                Vsd_ProgramDeliveryCurrentQuarter = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_programdeliverycurrentquarter")?.Value,
                Vsd_QuarterlyVarianceProgramDelivery = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlyvarianceprogramdelivery")?.Value,
                Vsd_YearToDateProgramDelivery = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_yeartodateprogramdelivery")?.Value,
                Vsd_YearToDateVarianceProgramDelivery = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_yeartodatevarianceprogramdelivery")?.Value,
                Vsd_SalariesBenefitsCurrentQuarter = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_salariesbenefitscurrentquarter")?.Value,
                Vsd_QuarterlyVarianceSalariesBenefits = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlyvariancesalariesbenefits")?.Value,
                Vsd_YearToDateSalariesAndBenefits = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_yeartodatesalariesandbenefits")?.Value,
                Vsd_YearToDateVarianceSalariesBenefits = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_yeartodatevariancesalariesbenefits")?.Value,
                Vsd_ProgramAdministrationExplanation = entity.GetAttributeValue<string>("vsd_programadministrationexplanation"),
                Vsd_ProgramDeliveryExplanations = entity.GetAttributeValue<string>("vsd_programdeliveryexplanations"),
                Vsd_SalariesAndBenefitsExplanation = entity.GetAttributeValue<string>("vsd_salariesandbenefitsexplanation"),
                Vsd_ReportReviewed = entity.GetAttributeValue<bool?>("vsd_reportreviewed"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static ScheduleGLineItemDto ToScheduleGLineItemDto(Entity entity)
        {
            if (entity == null) return null;

            return new ScheduleGLineItemDto 
            {
                Vsd_ScheduleGLineItemId = entity.GetAttributeValue<Guid>("vsd_scheduleglineitemid") != Guid.Empty
                     ? entity.GetAttributeValue<Guid>("vsd_scheduleglineitemid").ToString()
                     : entity.Id.ToString(),
                Vsd_ExpenseLineItemValue = GetEntityReferenceValue(entity, "vsd_expenselineitem"),
                Vsd_ScheduleGIdValue = GetEntityReferenceValue(entity, "vsd_schedulegid"),
                Vsd_AnnualBudgetedAmount = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_annualbudgetedamount")?.Value,
                Vsd_QuarterlyBudgetedAmount = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlybudgetedamount")?.Value,
                Vsd_ActualExpensesCurrentQuarter = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_actualexpensescurrentquarter")?.Value,
                Vsd_QuarterlyVariance = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_quarterlyvariance")?.Value,
                Vsd_ActualExpendituresYearToDate = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_actualexpendituresyeartodate")?.Value,
                Vsd_YearToDateVariance = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_yeartodatevariance")?.Value,
                Vsd_ExplanationForVariance = entity.GetAttributeValue<string>("vsd_explanationforvariance"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        private static string GetEntityReferenceValue(Entity entity, string attributeName)
        {
            if (entity.Contains(attributeName) && entity[attributeName] is EntityReference entityRef)
            {
                return entityRef.Id.ToString();
            }

            // Also check formatted values for _value pattern
            var lookupKey = $"_{attributeName}_value";
            if (entity.Contains(lookupKey))
            {
                var value = entity[lookupKey];
                return value?.ToString();
            }

            return null;
        }

        // ── Status Report mappers ─────────────────────────────────────────────────

        public static StatusReportContractDto ToStatusReportContractDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportContractDto
            {
                vsd_contractid = (entity.GetAttributeValue<Guid>("vsd_contractid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_contractid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                _vsd_customer_value = GetEntityReferenceValue(entity, "vsd_customer")
            };
        }

        public static StatusReportProgramDto ToStatusReportProgramDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportProgramDto
            {
                vsd_programid = (entity.GetAttributeValue<Guid>("vsd_programid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                _vsd_contractid_value = GetEntityReferenceValue(entity, "vsd_contractid"),
                _vsd_programtype_value = GetEntityReferenceValue(entity, "vsd_programtype"),
                _vsd_serviceproviderid_value = GetEntityReferenceValue(entity, "vsd_serviceproviderid"),
                vsd_cpu_numberofhours = (int?)entity.GetAttributeValue<decimal?>("vsd_cpu_numberofhours")
            };
        }

        public static StatusReportOrganizationDto ToStatusReportOrganizationDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportOrganizationDto
            {
                accountid = (entity.GetAttributeValue<Guid>("accountid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("accountid")
                    : entity.Id).ToString(),
                name = entity.GetAttributeValue<string>("name")
            };
        }

        public static StatusReportContactDto ToStatusReportContactDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportContactDto
            {
                contactid = (entity.GetAttributeValue<Guid>("contactid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("contactid")
                    : entity.Id).ToString(),
                firstname = entity.GetAttributeValue<string>("firstname"),
                lastname = entity.GetAttributeValue<string>("lastname")
            };
        }

        public static StatusReportCategoryDto ToStatusReportCategoryDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportCategoryDto
            {
                vsd_monthlystatisticscategoryid = (entity.GetAttributeValue<Guid>("vsd_monthlystatisticscategoryid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_monthlystatisticscategoryid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                vsd_categoryorder = entity.GetAttributeValue<int?>("vsd_categoryorder")
            };
        }

        public static StatusReportQuestionItemDto ToStatusReportQuestionItemDto(Entity entity)
        {
            if (entity == null) return null;
            var categoryId = GetEntityReferenceValue(entity, "vsd_categoryid");
            return new StatusReportQuestionItemDto
            {
                vsd_cpustatisticsmasterdataid = (entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                vsd_questionorder = entity.GetAttributeValue<int?>("vsd_questionorder"),
                vsd_questiontype = entity.GetAttributeValue<OptionSetValue>("vsd_questiontype")?.Value,
                vsd_tooltip = entity.GetAttributeValue<string>("vsd_tooltip"),
                _vsd_categoryid_value = categoryId,
                categoryID = categoryId,
                _vsd_cpuprogramtype_value = GetEntityReferenceValue(entity, "vsd_cpuprogramtype")
            };
        }

        public static StatusReportChildQuestionDto ToStatusReportChildQuestionDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportChildQuestionDto
            {
                vsd_cpustatisticsmasterdataid = (entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                vsd_questionorder = entity.GetAttributeValue<int?>("vsd_questionorder"),
                vsd_questiontype = entity.GetAttributeValue<OptionSetValue>("vsd_questiontype")?.Value,
                vsd_tooltip = entity.GetAttributeValue<string>("vsd_tooltip"),
                _vsd_categoryid_value = GetEntityReferenceValue(entity, "vsd_categoryid"),
                _vsd_parentid_value = GetEntityReferenceValue(entity, "vsd_parentid")
            };
        }

        public static StatusReportMcQuestionDto ToStatusReportMcQuestionDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportMcQuestionDto
            {
                vsd_cpustatisticsmasterdataanswerid = (entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataanswerid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataanswerid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                _vsd_questionid_value = GetEntityReferenceValue(entity, "vsd_questionid")
            };
        }

        public static StatusReportChildMcQuestionDto ToStatusReportChildMcQuestionDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportChildMcQuestionDto
            {
                vsd_cpustatisticsmasterdataanswerid = (entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataanswerid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_cpustatisticsmasterdataanswerid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                _vsd_questionid_value = GetEntityReferenceValue(entity, "vsd_questionid"),
                _vsd_parentid_value = GetEntityReferenceValue(entity, "vsd_parentid")
            };
        }

        public static StatusReportAnswerItemDto ToStatusReportAnswerItemDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportAnswerItemDto
            {
                vsd_datacollectionlineitemid = (entity.GetAttributeValue<Guid>("vsd_datacollectionlineitemid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_datacollectionlineitemid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                _vsd_datacollectionid_value = GetEntityReferenceValue(entity, "vsd_datacollectionid"),
                vsd_questioncategory = entity.GetAttributeValue<string>("vsd_questioncategory"),
                vsd_questionorder = entity.GetAttributeValue<int?>("vsd_questionorder"),
                vsd_questiontype1 = entity.GetAttributeValue<OptionSetValue>("vsd_questiontype1")?.Value,
                vsd_tooltip = entity.GetAttributeValue<string>("vsd_tooltip"),
                vsd_yesno = entity.GetAttributeValue<OptionSetValue>("vsd_yesno")?.Value,
                vsd_textanswer = entity.GetAttributeValue<string>("vsd_textanswer"),
                vsd_number = entity.GetAttributeValue<decimal?>("vsd_number"),
                createdon = entity.GetAttributeValue<DateTime?>("createdon"),
                _vsd_categoryid_value = GetEntityReferenceValue(entity, "vsd_categoryid")
            };
        }

        public static StatusReportProgramTypeDto ToStatusReportProgramTypeDto(Entity entity)
        {
            if (entity == null) return null;
            return new StatusReportProgramTypeDto
            {
                vsd_programtypeid = (entity.GetAttributeValue<Guid>("vsd_programtypeid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programtypeid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name")
            };
        }

        public static DataCollectionItemDto ToDataCollectionItemDto(Entity entity)
        {
            if (entity == null) return null;
            return new DataCollectionItemDto
            {
                vsd_datacollectionid = (entity.GetAttributeValue<Guid>("vsd_datacollectionid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_datacollectionid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name"),
                vsd_reportingperiod = entity.GetAttributeValue<OptionSetValue>("vsd_reportingperiod")?.Value,
                statuscode = entity.GetAttributeValue<OptionSetValue>("statuscode")?.Value,
                vsd_submissiondate = entity.GetAttributeValue<DateTime?>("vsd_submissiondate")?.ToString("o"),
                _vsd_contact_value = GetEntityReferenceValue(entity, "vsd_contact"),
                createdon = entity.GetAttributeValue<DateTime?>("createdon"),
                _vsd_program_value = GetEntityReferenceValue(entity, "vsd_program")
            };
        }

        public static ContactMinimalDto ToContactMinimalDto(Entity entity)
        {
            if (entity == null) return null;
            return new ContactMinimalDto
            {
                contactid = (entity.GetAttributeValue<Guid>("contactid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("contactid")
                    : entity.Id).ToString(),
                fullname = entity.GetAttributeValue<string>("fullname")
            };
        }

        public static ProgramMinimalDto ToProgramMinimalDto(Entity entity)
        {
            if (entity == null) return null;
            return new ProgramMinimalDto
            {
                vsd_programid = (entity.GetAttributeValue<Guid>("vsd_programid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_programid")
                    : entity.Id).ToString(),
                vsd_name = entity.GetAttributeValue<string>("vsd_name")
            };
        }

        public static SurplusPlanDto ToSurplusPlanDto(Entity entity)
        {
            if (entity == null) return null;

            return new SurplusPlanDto
            {
                Vsd_SurplusPlanReportId = (entity.GetAttributeValue<Guid>("vsd_surplusplanreportid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_surplusplanreportid")
                    : entity.Id).ToString(),
                Vsd_SurplusAmount = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_surplusamount")?.Value,
                Vsd_SurplusRemittance = entity.GetAttributeValue<bool?>("vsd_surplusremittance"),
                Vsd_DateSubmitted = entity.GetAttributeValue<DateTime?>("vsd_datesubmitted"),
                Vsd_ProgramIdValue = GetEntityReferenceValue(entity, "vsd_programid"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        public static SurplusLineItemDto ToSurplusLineItemDto(Entity entity)
        {
            if (entity == null) return null;

            return new SurplusLineItemDto
            {
                Vsd_SurplusLineItemId = (entity.GetAttributeValue<Guid>("vsd_surpluslineitemid") != Guid.Empty
                    ? entity.GetAttributeValue<Guid>("vsd_surpluslineitemid")
                    : entity.Id).ToString(),
                Vsd_Name = entity.GetAttributeValue<string>("vsd_name"),
                Vsd_JustificationDetails = entity.GetAttributeValue<string>("vsd_justificationdetails"),
                Vsd_ActualExpenditures = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_actualexpenditures")?.Value,
                Vsd_ActualExpenditures2 = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_actualexpenditures2")?.Value,
                Vsd_ActualExpenditures3 = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_actualexpenditures3")?.Value,
                Vsd_ActualExpenditures4 = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_actualexpenditures4")?.Value,
                Vsd_AllocatedAmount = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_allocatedamount")?.Value,
                Vsd_ProposedExpenditures = entity.GetAttributeValue<Microsoft.Xrm.Sdk.Money>("vsd_proposedexpenditures")?.Value,
                Vsd_DateSubmitted = entity.GetAttributeValue<DateTime?>("vsd_datesubmitted"),
                Vsd_EligibleExpenseItemIdValue = GetEntityReferenceValue(entity, "vsd_eligibleexpenseitemid"),
                Vsd_SurplusPlanIdValue = GetEntityReferenceValue(entity, "vsd_surplusplanid"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ?? entity.RowVersion
            };
        }

        /// <summary>Maps an activitymimeattachment entity to a DocumentItemDto.</summary>
        public static DocumentItemDto ToDocumentItemDto(Entity entity)
        {
            if (entity == null) return null;

            // The attribute may come back as a string or as a Guid depending on the message.
            string idStr = null;
            if (entity.Contains("activitymimeattachmentid"))
            {
                var raw = entity["activitymimeattachmentid"];
                idStr = raw switch
                {
                    Guid g => g != Guid.Empty ? g.ToString() : null,
                    string s => !string.IsNullOrWhiteSpace(s) ? s : null,
                    _ => raw?.ToString()
                };
            }
            if (idStr == null && entity.Id != Guid.Empty)
                idStr = entity.Id.ToString();

            return new DocumentItemDto
            {
                activitymimeattachmentid = idStr,
                filename = entity.GetAttributeValue<string>("filename"),
                subject = entity.GetAttributeValue<string>("subject"),
                subjectOther = entity.GetAttributeValue<string>("vsd_subjectother"),
                body = entity.GetAttributeValue<string>("body"),
                overwritetime = entity.GetAttributeValue<DateTime?>("overwritetime")?.ToString("o")
            };
        }
    }
}
