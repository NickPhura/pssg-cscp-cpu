using Database.Model;
using Microsoft.Xrm.Sdk;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class RegisterNewUserRequestMapper
    {
        /// <summary>
        /// Maps a <see cref="RegisterNewUserPost"/> DTO to a <see cref="Vsd_SetCpuOrgContractsRequest"/>
        /// that only populates the NewContact and NewServiceProvider parameters so that the
        /// vsd_SetCPUOrgContracts action creates the new user contact (and optionally a new
        /// service provider organisation) without touching any existing records.
        /// </summary>
        public static Vsd_SetCpuOrgContractsRequest ToRequest(RegisterNewUserPost model)
        {
            var request = new Vsd_SetCpuOrgContractsRequest
            {
                BusinessBcEId = model.BusinessBCeID,
                UserBcEId = model.UserBCeID
            };

            if (model.NewContact != null)
                request.NewContact = MapContact(model.NewContact);

            if (model.NewServiceProvider != null)
                request.NewServiceProvider = MapServiceProvider(model.NewServiceProvider);

            return request;
        }

        // ── Private mappers ──────────────────────────────────────────────────────

        private static Entity MapContact(DynamicsRegisterNewUserContactPost c)
        {
            var entity = new Contact();

            if (c.firstname != null) entity.FirstName = c.firstname;
            if (c.lastname != null) entity.LastName = c.lastname;
            if (c.middlename != null) entity.MiddleName = c.middlename;
            if (c.jobtitle != null) entity.JobTitle = c.jobtitle;
            if (c.emailaddress1 != null) entity.EmailAddress1 = c.emailaddress1;
            if (c.fax != null) entity.Fax = c.fax;
            if (c.mobilephone != null) entity.MobilePhone = c.mobilephone;
            if (c.telephone2 != null) entity.Telephone2 = c.telephone2;
            if (c.vsd_mainphoneextension != null) entity.Vsd_MainPhoneExtension = c.vsd_mainphoneextension;
            if (c.vsd_homephoneextension != null) entity.Vsd_HomePhoneExtension = c.vsd_homephoneextension;
            if (c.address1_city != null) entity.Address1_City = c.address1_city;
            if (c.address1_line1 != null) entity.Address1_Line1 = c.address1_line1;
            if (c.address1_line2 != null) entity.Address1_Line2 = c.address1_line2;
            if (c.address1_postalcode != null) entity.Address1_PostalCode = c.address1_postalcode;
            if (c.address1_stateorprovince != null) entity.Address1_StateOrProvince = c.address1_stateorprovince;

            // Role is always expected (100000005 = Staff, 100000007 = Contractor)
            entity.Vsd_ContactRole = (Contact_Vsd_ContactRole)c.vsd_contactrole;

            return entity;
        }

        private static Entity MapServiceProvider(DynamicsRegisterNewUserServiceProviderPost sp)
        {
            var entity = new Account();

            if (sp.name != null) entity.Name = sp.name;
            if (sp.telephone1 != null) entity.Telephone1 = sp.telephone1;
            if (sp.emailaddress1 != null) entity.EmailAddress1 = sp.emailaddress1;
            if (sp.fax != null) entity.Fax = sp.fax;
            if (sp.address1_city != null) entity.Address1_City = sp.address1_city;
            if (sp.address1_line1 != null) entity.Address1_Line1 = sp.address1_line1;
            if (sp.address1_line2 != null) entity.Address1_Line2 = sp.address1_line2;
            if (sp.address1_postalcode != null) entity.Address1_PostalCode = sp.address1_postalcode;
            if (sp.address1_stateorprovince != null) entity.Address1_StateOrProvince = sp.address1_stateorprovince;

            return entity;
        }

        private static void SetString(Entity entity, string attributeName, string value)
        {
            if (value != null)
                entity[attributeName] = value;
        }
    }
}
