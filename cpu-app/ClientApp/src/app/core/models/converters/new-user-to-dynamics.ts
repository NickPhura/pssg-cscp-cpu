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
  const post: DynamicsRegisterNewUserServiceProviderPost = {};
  // add all properties that are non null
  if (sp.address && sp.address.city) post.address1_city = sp.address.city;
  if (sp.address && sp.address.line1) post.address1_line1 = sp.address.line1;
  if (sp.address && sp.address.line2) post.address1_line2 = sp.address.line2;
  if (sp.address && sp.address.postalCode)
    post.address1_postalcode = sp.address.postalCode;
  if (sp.address && sp.address.province)
    post.address1_stateorprovince = sp.address.province;
  if (sp.email) post.emailaddress1 = sp.email;
  if (sp.fax) post.fax = sp.fax;
  if (sp.phone) post.telephone1 = sp.phone;
  if (sp.name) post.name = sp.name;
  // return the person
  return post;
}
