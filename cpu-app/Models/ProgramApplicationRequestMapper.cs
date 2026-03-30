using Database.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class ProgramApplicationRequestMapper
    {
        public static Vsd_SetCpuOrgContractsRequest ToRequest(ProgramApplicationPost model)
        {
            var request = new Vsd_SetCpuOrgContractsRequest
            {
                BusinessBcEId = model.BusinessBCeID,
                UserBcEId = model.UserBCeID
            };

            if (model.Organization != null)
                request.Organization = MapOrganization(model.Organization);

            if (model.ContactCollection != null)
                request.ContactCollection = new EntityCollection(
                    model.ContactCollection.Select(MapContact).ToList());

            if (model.ContractCollection != null)
                request.ContractCollection = new EntityCollection(
                    model.ContractCollection.Select(MapContract).ToList());

            if (model.ProgramCollection != null)
                request.ProgramCollection = new EntityCollection(
                    model.ProgramCollection.Select(MapProgram).ToList());

            if (model.ScheduleCollection != null)
                request.ScheduleCollection = new EntityCollection(
                    model.ScheduleCollection.Select(MapSchedule).ToList());

            if (model.AddProgramContactCollection != null)
                request.AddProgramContactCollection = new EntityCollection(
                    model.AddProgramContactCollection.Select(MapProgramContact).ToList());

            if (model.RemoveProgramContactCollection != null)
                request.RemoveProgramContactCollection = new EntityCollection(
                    model.RemoveProgramContactCollection.Select(MapProgramContact).ToList());

            if (model.AddProgramSubContractorCollection != null)
                request.AddProgramSubcontractorCollection = new EntityCollection(
                    model.AddProgramSubContractorCollection.Select(MapProgramContact).ToList());

            if (model.RemoveProgramSubContractorCollection != null)
                request.RemoveProgramSubcontractorCollection = new EntityCollection(
                    model.RemoveProgramSubContractorCollection.Select(MapProgramContact).ToList());

            return request;
        }

        private static Entity MapOrganization(DynamicsProgramApplicationOrganizationPost org)
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
            SetString(entity, "address1_country", org.address1_country);
            SetString(entity, "address2_city", org.address2_city);
            SetString(entity, "address2_line1", org.address2_line1);
            SetString(entity, "address2_line2", org.address2_line2);
            SetString(entity, "address2_postalcode", org.address2_postalcode);
            SetString(entity, "address2_stateorprovince", org.address2_stateorprovince);
            SetString(entity, "address2_country", org.address2_country);

            SetEntityReference(entity, "vsd_executivecontactid", "contact", org.vsd_ExecutiveContactIdfortunecookiebind);
            SetEntityReference(entity, "vsd_boardcontactid", "contact", org.vsd_BoardContactIdfortunecookiebind);

            return entity;
        }

        private static Entity MapContact(DynamicsProgramApplicationContactPost c)
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
            SetString(entity, "vsd_portalfield", c.vsd_portalfield);
            SetParentCustomerId(entity, c._parentcustomerid_value);

            if (c.vsd_employmentstatus.HasValue)
                entity["vsd_employmentstatus"] = new OptionSetValue(c.vsd_employmentstatus.Value);

            if (c.statecode.HasValue)
                entity["statecode"] = new OptionSetValue(c.statecode.Value);

            return entity;
        }

        private static Entity MapContract(DynamicsProgramApplicationContractPost c)
        {
            var entity = new Entity("vsd_contract");

            if (Guid.TryParse(c.vsd_contractid, out var contractId))
                entity.Id = contractId;

            SetString(entity, "vsd_name", c.vsd_name);
            SetString(entity, "vsd_cpu_humanresourcepolices", c.vsd_cpu_humanresourcepolices);
            SetString(entity, "vsd_cpu_specificunion", c.vsd_cpu_specificunion);
            SetString(entity, "vsd_authorizedsigningofficersignature", c.vsd_authorizedsigningofficersignature);
            SetString(entity, "vsd_signingofficersname", c.vsd_signingofficersname);
            SetString(entity, "vsd_signingofficertitle", c.vsd_signingofficertitle);

            if (c.vsd_cpu_subcontractedprogramstaff.HasValue)
                entity["vsd_cpu_subcontractedprogramstaff"] = new OptionSetValue(c.vsd_cpu_subcontractedprogramstaff.Value);
            if (c.vsd_cpu_unionizedstaff.HasValue)
                entity["vsd_cpu_unionizedstaff"] = new OptionSetValue(c.vsd_cpu_unionizedstaff.Value);
            if (c.vsd_cpu_insuranceoptions.HasValue)
                entity["vsd_cpu_insuranceoptions"] = new OptionSetValue(c.vsd_cpu_insuranceoptions.Value);
            if (c.vsd_cpu_memberofcssea.HasValue)
                entity["vsd_cpu_memberofcssea"] = new OptionSetValue(c.vsd_cpu_memberofcssea.Value);

            SetEntityReference(entity, "vsd_contactlookup1", "contact", c.vsd_ContactLookup1fortunecookiebind);
            SetEntityReference(entity, "vsd_contactlookup2", "contact", c.vsd_ContactLookup2fortunecookiebind);

            return entity;
        }

        private static Entity MapProgram(DynamicsProgramApplicationProgramPost p)
        {
            var entity = new Entity("vsd_program");

            if (Guid.TryParse(p.vsd_programid, out var programId))
                entity.Id = programId;

            SetString(entity, "vsd_addressline1", p.vsd_addressline1);
            SetString(entity, "vsd_addressline2", p.vsd_addressline2);
            SetString(entity, "vsd_city", p.vsd_city);
            SetString(entity, "vsd_country", p.vsd_country);
            SetString(entity, "vsd_emailaddress", p.vsd_emailaddress);
            SetString(entity, "vsd_fax", p.vsd_fax);
            SetString(entity, "vsd_governmentfunderagency", p.vsd_governmentfunderagency);
            SetString(entity, "vsd_mailingaddressline1", p.vsd_mailingaddressline1);
            SetString(entity, "vsd_mailingaddressline2", p.vsd_mailingaddressline2);
            SetString(entity, "vsd_mailingcity", p.vsd_mailingcity);
            SetString(entity, "vsd_mailingcountry", p.vsd_mailingcountry);
            SetString(entity, "vsd_mailingpostalcodezip", p.vsd_mailingpostalcodezip);
            SetString(entity, "vsd_mailingprovincestate", p.vsd_mailingprovincestate);
            SetString(entity, "vsd_phonenumber", p.vsd_phonenumber);
            SetString(entity, "vsd_postalcodezip", p.vsd_postalcodezip);
            SetString(entity, "vsd_provincestate", p.vsd_provincestate);

            entity["vsd_costshare"] = p.vsd_costshare;
            entity["vsd_cpu_programstaffsubcontracted"] = p.vsd_cpu_programstaffsubcontracted;
            entity["vsd_addresstransitionorsafehome"] = p.vsd_addresstransitionorsafehome;

            if (p.vsd_cpu_per.HasValue)
                entity["vsd_cpu_per"] = new OptionSetValue(p.vsd_cpu_per.Value);
            if (p.vsd_totaloncallstandbyhours.HasValue)
                entity["vsd_totaloncallstandbyhours"] = p.vsd_totaloncallstandbyhours.Value;
            if (p.vsd_totalscheduledhours.HasValue)
                entity["vsd_totalscheduledhours"] = p.vsd_totalscheduledhours.Value;

            SetEntityReference(entity, "vsd_contactlookup", "contact", p.vsd_ContactLookupfortunecookiebind);
            SetEntityReference(entity, "vsd_contactlookup2", "contact", p.vsd_ContactLookup2fortunecookiebind);
            SetEntityReference(entity, "vsd_contactlookup3", "contact", p.vsd_ContactLookup3fortunecookiebind);

            return entity;
        }

        private static Entity MapSchedule(DynamicsProgramApplicationSchedulePost s)
        {
            var entity = new Entity("vsd_schedule");

            if (Guid.TryParse(s.vsd_scheduleid, out var scheduleId))
                entity.Id = scheduleId;

            SetString(entity, "vsd_days", s.vsd_days);
            SetString(entity, "vsd_scheduledstarttime", s.vsd_scheduledstarttime);
            SetString(entity, "vsd_scheduledendtime", s.vsd_scheduledendtime);

            entity["vsd_cpu_scheduletype"] = new OptionSetValue(s.vsd_cpu_scheduletype);

            if (s.statecode.HasValue)
                entity["statecode"] = new OptionSetValue(s.statecode.Value);

            SetEntityReference(entity, "vsd_programid", "vsd_program", s.vsd_ProgramIdfortunecookiebind);

            return entity;
        }

        private static Entity MapProgramContact(DynamicsProgramApplicationProgramContactPost pc)
        {
            var entity = new Entity("vsd_contact_vsd_program");

            if (Guid.TryParse(pc.contactid, out var contactId))
                entity["contactid"] = new EntityReference("contact", contactId);

            if (Guid.TryParse(pc.vsd_programid, out var programId))
                entity["vsd_programid"] = new EntityReference("vsd_program", programId);

            return entity;
        }

        // Extracts a GUID from an OData bind string like "/contacts(guid)" or "/vsd_programs(guid)"
        private static Guid? ExtractGuidFromBind(string bindValue)
        {
            if (string.IsNullOrWhiteSpace(bindValue)) return null;

            var match = Regex.Match(bindValue, @"\(([0-9a-fA-F\-]{36})\)");
            if (match.Success && Guid.TryParse(match.Groups[1].Value, out var guid))
                return guid;

            return null;
        }

        private static void SetString(Entity entity, string attributeName, string value)
        {
            if (value != null)
                entity[attributeName] = value;
        }

        private static void SetEntityReference(Entity entity, string attributeName, string logicalName, string bindValue)
        {
            var guid = ExtractGuidFromBind(bindValue);
            if (guid.HasValue)
                entity[attributeName] = new EntityReference(logicalName, guid.Value);
        }

        private static void SetParentCustomerId(Entity entity, string accountId)
        {
            if (Guid.TryParse(accountId, out var guid))
                entity["parentcustomerid"] = new EntityReference("account", guid);
        }
    }
}
