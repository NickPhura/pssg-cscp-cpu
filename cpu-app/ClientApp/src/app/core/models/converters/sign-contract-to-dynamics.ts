import { iSignature } from "../../../authenticated/subforms/program-authorizer/program-authorizer.component";
import { nameAssemble } from "../../constants/name-assemble";
import { iDynamicsDocument } from "../dynamics-blob";
import { iDynamicsPostSignedContract } from "../dynamics-post";

//this is a mapper function for converting people into dynamics users
export function convertContractPackageToDynamics(
  userId: string,
  organizationId: string,
  documents: iDynamicsDocument[],
  signature: iSignature,
  isModificationAgreement: boolean = false,
): iDynamicsPostSignedContract {
  let post: iDynamicsPostSignedContract = {
    Businessbceid: organizationId,
    Userbceid: userId,
    DocumentCollection: documents,
    Signature: convertSignatureToDynamics(signature),
    IsModificationAgreement: isModificationAgreement,
  };
  return post;
}

function convertSignatureToDynamics(signature: iSignature) {
  let ret = {
    vsd_authorizedsigningofficersignature: signature.signature
      ? signature.signature
      : null,
    vsd_signingofficertitle: signature.signer ? signature.signer.title : null,
    vsd_signingofficersname: signature.signer
      ? nameAssemble(
          signature.signer.firstName,
          signature.signer.middleName,
          signature.signer.lastName,
        )
      : null,
  };
  return ret;
}
