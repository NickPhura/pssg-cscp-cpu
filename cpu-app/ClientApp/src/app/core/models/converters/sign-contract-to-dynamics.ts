import { iSignature } from "../../../authenticated/subforms/program-authorizer/program-authorizer.component";
import {
  DocumentItemDto,
  SignedContractPostFromPortal,
} from "../../api/models";
import { nameAssemble } from "../../constants/name-assemble";

//this is a mapper function for converting people into dynamics users
export function convertContractPackageToDynamics(
  userId: string,
  organizationId: string,
  documents: DocumentItemDto[],
  signature: iSignature,
  isModificationAgreement: boolean = false,
): SignedContractPostFromPortal {
  let post: SignedContractPostFromPortal = {
    businessBCeID: organizationId,
    userBCeID: userId,
    documentCollection: documents.map((d) => ({
      body: d.body,
      filename: d.filename,
      subject: d.subject,
    })),
    signature: convertSignatureToDynamics(signature),
    isModificationAgreement: isModificationAgreement,
  };
  return post;
}

function convertSignatureToDynamics(signature: iSignature) {
  return {
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
}
