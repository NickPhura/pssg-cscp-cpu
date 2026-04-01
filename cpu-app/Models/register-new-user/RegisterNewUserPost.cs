using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class RegisterNewUserPost
    {
        [Required]
        public string BusinessBCeID { get; set; }
        [Required]
        public string UserBCeID { get; set; }
        [Required]
        public DynamicsRegisterNewUserContactPost NewContact { get; set; }
        // NewServiceProvider is optional: the new-user-only flow does not send it
        public DynamicsRegisterNewUserServiceProviderPost NewServiceProvider { get; set; }
    }
}
