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
            var entity = new Entity("account");

            if (Guid.TryParse(org.accountid, out var accountId))
                entity.Id = accountId;

            SetString(entity, "name", org.name);
            SetString(entity, "telephone1", org.telephone1);
            SetString(entity, "emailaddress1", org.emailaddress1);
            SetString(entity, "fax", org.fax);
            SetString(entity, "address1_city", org.address1_city);
            SetString(entity, "address1_line1", org.address1_line1);
            SetString(entity, "address1_line2", org.address1_line2);
            SetString(entity, "address1_postalcode", org.address1_postalcode);
            SetString(entity, "address1_stateorprovince", org.address1_stateorprovince);
            SetString(entity, "address2_city", org.address2_city);
            SetString(entity, "address2_line1", org.address2_line1);
            SetString(entity, "address2_line2", org.address2_line2);
            SetString(entity, "address2_postalcode", org.address2_postalcode);
            SetString(entity, "address2_stateorprovince", org.address2_stateorprovince);

            // The getter wraps the stored raw GUID in the OData bind format "/contacts(guid)".
            // SetEntityReference extracts the GUID from that string via regex.
            SetEntityReference(entity, "vsd_executivecontactid", "contact", org.vsd_ExecutiveContactIdfortunecookiebind);
            SetEntityReference(entity, "vsd_boardcontactid", "contact", org.vsd_BoardContactIdfortunecookiebind);

            return entity;
        }

        private static Entity MapContact(DynamicsOrganizationContactPost c)
        {
            var entity = new Entity("contact");

            if (Guid.TryParse(c.contactid, out var contactId))
                entity.Id = contactId;

            SetString(entity, "firstname", c.firstname);
            SetString(entity, "lastname", c.lastname);
            SetString(entity, "middlename", c.middlename);
            SetString(entity, "jobtitle", c.jobtitle);
            SetString(entity, "emailaddress1", c.emailaddress1);
            SetString(entity, "mobilephone", c.mobilephone);
            SetString(entity, "fax", c.fax);
            SetString(entity, "telephone2", c.telephone2);
            SetString(entity, "address1_line1", c.address1_line1);
            SetString(entity, "address1_line2", c.address1_line2);
            SetString(entity, "address1_city", c.address1_city);
            SetString(entity, "address1_postalcode", c.address1_postalcode);
            SetString(entity, "address1_stateorprovince", c.address1_stateorprovince);
            SetString(entity, "vsd_bceid", c.vsd_bceid);
            SetString(entity, "vsd_mainphoneextension", c.vsd_mainphoneextension);
            SetString(entity, "vsd_homephoneextension", c.vsd_homephoneextension);

            if (c._parentcustomerid_value != null && Guid.TryParse(c._parentcustomerid_value, out var accountId))
                entity["parentcustomerid"] = new EntityReference("account", accountId);

            if (c.vsd_employmentstatus.HasValue)
                entity["vsd_employmentstatus"] = new OptionSetValue(c.vsd_employmentstatus.Value);

            if (c.statecode.HasValue)
                entity["statecode"] = new OptionSetValue(c.statecode.Value);

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
