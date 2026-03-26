using Microsoft.Xrm.Sdk;
using System;

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
                Vsd_City = entity.GetAttributeValue<string>("vsd_city"),
                Vsd_AddressLine1 = entity.GetAttributeValue<string>("vsd_addressline1"),
                Vsd_AddressLine2 = entity.GetAttributeValue<string>("vsd_addressline2"),
                Vsd_ProvinceState = entity.GetAttributeValue<string>("vsd_provincestate"),
                Vsd_EmailAddress = entity.GetAttributeValue<string>("vsd_emailaddress"),
                Vsd_Fax = entity.GetAttributeValue<string>("vsd_fax"),
                Vsd_MailingCity = entity.GetAttributeValue<string>("vsd_mailingcity"),
                Vsd_MailingAddressLine1 = entity.GetAttributeValue<string>("vsd_mailingaddressline1"),
                Vsd_MailingAddressLine2 = entity.GetAttributeValue<string>("vsd_mailingaddressline2"),
                Vsd_MailingProvinceState = entity.GetAttributeValue<string>("vsd_mailingprovincestate"),
                Vsd_PhoneNumber = entity.GetAttributeValue<string>("vsd_phonenumber"),
                FortuneCookieType = entity.LogicalName,
                FortuneCookieEtag = entity.GetAttributeValue<string>("versionnumber") ??
                                   entity.RowVersion
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
    }
}
