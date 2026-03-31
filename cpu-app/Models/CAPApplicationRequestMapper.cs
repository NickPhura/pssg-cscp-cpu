using Database.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class CAPApplicationRequestMapper
    {
        public static Vsd_SetCpuOrgContractsRequest ToRequest(CAPApplicationPost model)
        {
            var request = new Vsd_SetCpuOrgContractsRequest
            {
                BusinessBcEId = model.BusinessBCeID,
                UserBcEId = model.UserBCeID
            };

            if (model.Organization != null)
                request.Organization = MapOrganization(model.Organization);

            if (model.ContractCollection != null)
                request.ContractCollection = new EntityCollection(
                    model.ContractCollection.Select(MapContract).ToList());

            if (model.ProgramCollection != null)
                request.ProgramCollection = new EntityCollection(
                    model.ProgramCollection.Select(MapProgram).ToList());

            if (model.AddProgramContactCollection != null)
                request.AddProgramContactCollection = new EntityCollection(
                    model.AddProgramContactCollection.Select(MapProgramContact).ToList());

            if (model.RemoveProgramContactCollection != null)
                request.RemoveProgramContactCollection = new EntityCollection(
                    model.RemoveProgramContactCollection.Select(MapProgramContact).ToList());

            return request;
        }

        private static Entity MapOrganization(DynamicsCAPApplicationOrganizationPost org)
        {
            var entity = new Account();

            if (Guid.TryParse(org.accountid, out var accountId))
                entity.Id = accountId;

            if (org.name != null) entity.Name = org.name;
            if (org.address1_city != null) entity.Address1_City = org.address1_city;
            if (org.address1_line1 != null) entity.Address1_Line1 = org.address1_line1;
            if (org.address1_line2 != null) entity.Address1_Line2 = org.address1_line2;
            if (org.address1_postalcode != null) entity.Address1_PostalCode = org.address1_postalcode;
            if (org.address1_stateorprovince != null) entity.Address1_StateOrProvince = org.address1_stateorprovince;
            if (org.address1_country != null) entity.Address1_Country = org.address1_country;

            return entity;
        }

        private static Entity MapContract(DynamicsCAPApplicationContractPost c)
        {
            var entity = new Vsd_Contract();

            if (Guid.TryParse(c.vsd_contractid, out var contractId))
                entity.Id = contractId;

            if (c.vsd_name != null) entity.Vsd_Name = c.vsd_name;
            if (c.vsd_authorizedsigningofficersignature != null) entity.Vsd_AuthorizedSigningOfficerSignature = c.vsd_authorizedsigningofficersignature;
            if (c.vsd_signingofficersname != null) entity.Vsd_SigningOfficersName = c.vsd_signingofficersname;
            if (c.vsd_signingofficertitle != null) entity.Vsd_SigningOfficerTitle = c.vsd_signingofficertitle;

            if (c.vsd_cpu_insuranceoptions.HasValue)
                entity.Vsd_Cpu_InsuranceOptions = (Vsd_Contract_Vsd_Cpu_InsuranceOptions)c.vsd_cpu_insuranceoptions.Value;

            if (c.vsd_collaborationwithkeystakeholders.HasValue)
                entity.Vsd_CollaborationWithKeyStakeholders = (Vsd_YesNo)c.vsd_collaborationwithkeystakeholders.Value;

            if (c.vsd_complaintandfeedbackprocessforparticipant.HasValue)
                entity.Vsd_ComplaintAndFeedbackProcessForParticipant = (Vsd_YesNo)c.vsd_complaintandfeedbackprocessforparticipant.Value;

            entity.Vsd_CriminalRecordChecks = c.vsd_criminalrecordchecks;

            if (c.vsd_letterofreferencefromreferralsources.HasValue)
                entity.Vsd_LetterOfReferenceFromReferralSources = (Vsd_YesNo)c.vsd_letterofreferencefromreferralsources.Value;

            if (c.vsd_establishedconfidentialityguidelines.HasValue)
                entity.Vsd_EstablishedConfidentialityGuidelines = (Vsd_YesNo)c.vsd_establishedconfidentialityguidelines.Value;

            return entity;
        }

        private static Entity MapProgram(DynamicsCAPApplicationProgramPost p)
        {
            var entity = new Vsd_Program();

            if (Guid.TryParse(p.vsd_programid, out var programId))
                entity.Id = programId;

            entity.Vsd_Cpu_FundingAmountRequested = new Money(p.vsd_cpu_fundingamountrequested);

            // vsd_cpu_programmodeltypes is a multi-select optionset; carry as raw string
            SetString(entity, "vsd_cpu_programmodeltypes", p.vsd_cpu_programmodeltypes);

            if (p.vsd_otherprogrammodels != null) entity.Vsd_OtherProgramModels = p.vsd_otherprogrammodels;
            if (p.vsd_cpu_programevaluationdescription != null) entity.Vsd_Cpu_ProgramEvaluationDescription = p.vsd_cpu_programevaluationdescription;
            if (p.vsd_cpu_capprogramoperationscomments != null) entity.Vsd_Cpu_CapProgramOperationsComments = p.vsd_cpu_capprogramoperationscomments;

            entity.Vsd_Cpu_ProgramEvaluationEfforts = (Vsd_YesNo)p.vsd_cpu_programevaluationefforts;

            // vsd_ContactLookupfortunecookiebind returns "/contacts(<guid>)" — extract the GUID
            SetEntityReference(entity, "vsd_contactlookup", "contact", p.vsd_ContactLookupfortunecookiebind);

            return entity;
        }

        private static Entity MapProgramContact(DynamicsCAPApplicationProgramContactPost pc)
        {
            var entity = new Entity("vsd_contact_vsd_program");

            if (Guid.TryParse(pc.contactid, out var contactId))
                entity["contactid"] = new EntityReference("contact", contactId);

            if (Guid.TryParse(pc.vsd_programid, out var programId))
                entity["vsd_programid"] = new EntityReference("vsd_program", programId);

            return entity;
        }

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
    }
}
