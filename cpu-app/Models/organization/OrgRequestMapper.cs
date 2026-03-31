using Database.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class OrgRequestMapper
    {
        /// <summary>
        /// Maps an <see cref="OrganizationPost"/> DTO to a <see cref="Vsd_SetCpuOrgContractsRequest"/>.
        /// Only non-null collections are added to the request so callers do not need to
        /// populate every field — e.g. the profile update only supplies Organisation and
        /// the staff update only supplies StaffCollection.
        /// </summary>
        public static Vsd_SetCpuOrgContractsRequest ToRequest(OrganizationPost model)
        {
            var request = new Vsd_SetCpuOrgContractsRequest
            {
                BusinessBcEId = model.BusinessBCeID,
                UserBcEId = model.UserBCeID
            };

            if (model.Organization != null)
                request.Organization = MapOrganization(model.Organization);

            if (model.StaffCollection != null && model.StaffCollection.Length > 0)
                request.StaffCollection = new EntityCollection(
                    model.StaffCollection.Select(MapContact).ToList());

            return request;
        }

        // ------------------------------------------------------------------ //
        // Private mappers
        // ------------------------------------------------------------------ //

        private static Entity MapOrganization(DynamicsOrganizationPost org)
        {
            var entity = new Account();

            if (Guid.TryParse(org.accountid, out var accountId))
                entity.Id = accountId;

            if (org.name != null) entity.Name = org.name;
            if (org.telephone1 != null) entity.Telephone1 = org.telephone1;
            if (org.emailaddress1 != null) entity.EmailAddress1 = org.emailaddress1;
            if (org.fax != null) entity.Fax = org.fax;
            if (org.address1_city != null) entity.Address1_City = org.address1_city;
            if (org.address1_line1 != null) entity.Address1_Line1 = org.address1_line1;
            if (org.address1_line2 != null) entity.Address1_Line2 = org.address1_line2;
            if (org.address1_postalcode != null) entity.Address1_PostalCode = org.address1_postalcode;
            if (org.address1_stateorprovince != null) entity.Address1_StateOrProvince = org.address1_stateorprovince;
            if (org.address2_city != null) entity.Address2_City = org.address2_city;
            if (org.address2_line1 != null) entity.Address2_Line1 = org.address2_line1;
            if (org.address2_line2 != null) entity.Address2_Line2 = org.address2_line2;
            if (org.address2_postalcode != null) entity.Address2_PostalCode = org.address2_postalcode;
            if (org.address2_stateorprovince != null) entity.Address2_StateOrProvince = org.address2_stateorprovince;

            // The getter wraps the stored raw GUID in the OData bind format "/contacts(guid)".
            // SetEntityReference extracts the GUID from that string via regex.
            SetEntityReference(entity, "vsd_executivecontactid", "contact", org.vsd_ExecutiveContactIdfortunecookiebind);
            SetEntityReference(entity, "vsd_boardcontactid", "contact", org.vsd_BoardContactIdfortunecookiebind);

            return entity;
        }

        private static Entity MapContact(DynamicsOrganizationContactPost c)
        {
            var entity = new Contact();

            if (Guid.TryParse(c.contactid, out var contactId))
                entity.Id = contactId;

            if (c.firstname != null) entity.FirstName = c.firstname;
            if (c.lastname != null) entity.LastName = c.lastname;
            if (c.middlename != null) entity.MiddleName = c.middlename;
            if (c.jobtitle != null) entity.JobTitle = c.jobtitle;
            if (c.emailaddress1 != null) entity.EmailAddress1 = c.emailaddress1;
            if (c.mobilephone != null) entity.MobilePhone = c.mobilephone;
            if (c.fax != null) entity.Fax = c.fax;
            if (c.telephone2 != null) entity.Telephone2 = c.telephone2;
            if (c.address1_line1 != null) entity.Address1_Line1 = c.address1_line1;
            if (c.address1_line2 != null) entity.Address1_Line2 = c.address1_line2;
            if (c.address1_city != null) entity.Address1_City = c.address1_city;
            if (c.address1_postalcode != null) entity.Address1_PostalCode = c.address1_postalcode;
            if (c.address1_stateorprovince != null) entity.Address1_StateOrProvince = c.address1_stateorprovince;
            if (c.vsd_bceid != null) entity.Vsd_BcEId = c.vsd_bceid;
            if (c.vsd_mainphoneextension != null) entity.Vsd_MainPhoneExtension = c.vsd_mainphoneextension;
            if (c.vsd_homephoneextension != null) entity.Vsd_HomePhoneExtension = c.vsd_homephoneextension;

            if (c._parentcustomerid_value != null && Guid.TryParse(c._parentcustomerid_value, out var accountId))
                entity.ParentCustomerId = new EntityReference("account", accountId);

            if (c.vsd_employmentstatus.HasValue)
                entity.Vsd_EmploymentStatus = (Contact_Vsd_EmploymentStatus)c.vsd_employmentstatus.Value;

            if (c.statecode.HasValue)
                entity.StateCode = (Contact_StateCode)c.statecode.Value;

            return entity;
        }

        // ------------------------------------------------------------------ //
        // Helpers (replicated from ProgramApplicationRequestMapper pattern)
        // ------------------------------------------------------------------ //

        private static void SetString(Entity entity, string attributeName, string value)
        {
            if (value != null)
                entity[attributeName] = value;
        }

        /// <summary>
        /// Extracts a GUID from an OData bind string such as "/contacts(guid)" and
        /// sets it on the entity as an <see cref="EntityReference"/>.
        /// Also handles a bare GUID string without the bind wrapper.
        /// </summary>
        private static void SetEntityReference(Entity entity, string attributeName, string logicalName, string bindValue)
        {
            if (string.IsNullOrWhiteSpace(bindValue)) return;

            Guid guid;

            // Try extracting from OData bind format: /contacts(some-guid)
            var match = Regex.Match(bindValue, @"\(([0-9a-fA-F\-]{36})\)");
            if (match.Success && Guid.TryParse(match.Groups[1].Value, out guid))
            {
                entity[attributeName] = new EntityReference(logicalName, guid);
                return;
            }

            // Fall back to treating the value as a bare GUID
            if (Guid.TryParse(bindValue, out guid))
                entity[attributeName] = new EntityReference(logicalName, guid);
        }
    }
}
