import {
  DynamicsRegisterNewUserContactPost,
  DynamicsRegisterNewUserServiceProviderPost,
  RegisterNewUserPost,
} from "../../api/models";
import { iServiceProvider } from "../service-provider.interface";
import { convertPersonToDynamics } from "./person-to-dynamics";
import { TransmogrifierNewUser } from "./transmogrifier-new-user.class";

export function convertNewUserToDynamics(
  trans: TransmogrifierNewUser,
): RegisterNewUserPost {
  const newContact: DynamicsRegisterNewUserContactPost =
    convertPersonToDynamics(trans.person) as DynamicsRegisterNewUserContactPost;
  const serviceProvider: DynamicsRegisterNewUserServiceProviderPost =
    convertServiceProviderToDynamics(trans.serviceProvider);
  if (trans.isContractorContact) {
    newContact.vsd_contactrole = 100000007;
  } else {
    newContact.vsd_contactrole = 100000005;
  }

  return {
    businessBCeID: trans.organizationId,
    userBCeID: trans.userId,
    newContact,
    newServiceProvider: serviceProvider,
  };
}

function convertServiceProviderToDynamics(
  sp: iServiceProvider,
): DynamicsRegisterNewUserServiceProviderPost {
  const post: DynamicsRegisterNewUserServiceProviderPost = {
    name: sp.name || null,
    address1_city: (sp.address && sp.address.city) || null,
    address1_line1: (sp.address && sp.address.line1) || null,
    address1_postalcode: (sp.address && sp.address.postalCode) || null,
    emailaddress1: sp.email || null,
    telephone1: sp.phone || null,
  };
  // add optional properties that are non null
  if (sp.address && sp.address.line2) post.address1_line2 = sp.address.line2;
  if (sp.address && sp.address.province)
    post.address1_stateorprovince = sp.address.province;
  if (sp.fax) post.fax = sp.fax;
  // return the service provider
  return post;
}
