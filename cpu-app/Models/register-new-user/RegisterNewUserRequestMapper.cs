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
            var entity = new Entity("contact");

            SetString(entity, "firstname", c.firstname);
            SetString(entity, "lastname", c.lastname);
            SetString(entity, "middlename", c.middlename);
            SetString(entity, "jobtitle", c.jobtitle);
            SetString(entity, "emailaddress1", c.emailaddress1);
            SetString(entity, "fax", c.fax);
            SetString(entity, "mobilephone", c.mobilephone);
            SetString(entity, "telephone2", c.telephone2);
            SetString(entity, "vsd_mainphoneextension", c.vsd_mainphoneextension);
            SetString(entity, "vsd_homephoneextension", c.vsd_homephoneextension);
            SetString(entity, "address1_city", c.address1_city);
            SetString(entity, "address1_line1", c.address1_line1);
            SetString(entity, "address1_line2", c.address1_line2);
            SetString(entity, "address1_postalcode", c.address1_postalcode);
            SetString(entity, "address1_stateorprovince", c.address1_stateorprovince);

            // Role is always expected (100000005 = Staff, 100000007 = Contractor)
            entity["vsd_contactrole"] = new OptionSetValue(c.vsd_contactrole);

            return entity;
        }

        private static Entity MapServiceProvider(DynamicsRegisterNewUserServiceProviderPost sp)
        {
            var entity = new Entity("account");

            SetString(entity, "name", sp.name);
            SetString(entity, "telephone1", sp.telephone1);
            SetString(entity, "emailaddress1", sp.emailaddress1);
            SetString(entity, "fax", sp.fax);
            SetString(entity, "address1_city", sp.address1_city);
            SetString(entity, "address1_line1", sp.address1_line1);
            SetString(entity, "address1_line2", sp.address1_line2);
            SetString(entity, "address1_postalcode", sp.address1_postalcode);
            SetString(entity, "address1_stateorprovince", sp.address1_stateorprovince);

            return entity;
        }

        private static void SetString(Entity entity, string attributeName, string value)
        {
            if (value != null)
                entity[attributeName] = value;
        }
    }
}
