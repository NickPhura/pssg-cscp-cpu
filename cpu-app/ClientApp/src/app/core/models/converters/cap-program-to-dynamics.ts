import {
  CAPApplicationPost,
  DynamicsCAPApplicationProgramContactPost,
  DynamicsCAPApplicationProgramPost,
} from "../../api/models";
import { boolOptionSet } from "../../constants/bool-optionset-values";
import { nameAssemble } from "../../constants/name-assemble";
import { iCAPProgram } from "../cap-program.interface";
import { iPerson } from "../person.interface";
import { TransmogrifierCAPApplication } from "../transmogrifier-cap-application.class";

export function convertCAPProgramToDynamics(
  trans: TransmogrifierCAPApplication,
  isSubmit: boolean = false,
): CAPApplicationPost {
  const post: CAPApplicationPost = {
    businessBCeID: trans.organizationId,
    userBCeID: trans.userId,
    contractCollection: [
      {
        vsd_contractid: trans.contractId,
        vsd_name: trans.contractNumber,
        vsd_authorizedsigningofficersignature:
          trans.signature.signature && isSubmit
            ? trans.signature.signature
            : null,
        vsd_signingofficersname:
          trans.signature.signer && isSubmit
            ? nameAssemble(
                trans.signature.signer.firstName,
                trans.signature.signer.middleName,
                trans.signature.signer.lastName,
              )
            : null,
        vsd_signingofficertitle:
          trans.signature.signer && isSubmit
            ? trans.signature.signer.title
            : null,
        vsd_cpu_insuranceoptions: trans.applyingForInsurance
          ? 100000001
          : 100000000,
        vsd_collaborationwithkeystakeholders:
          trans.collaborationWithKeyStakeholders ? 100000001 : 100000000,
        vsd_complaintandfeedbackprocessforparticipant:
          trans.complaintProcessForParticipant ? 100000001 : 100000000,
        vsd_criminalrecordchecks: trans.criminalRecordChecks,
        vsd_letterofreferencefromreferralsources: trans.letterOfReference
          ? 100000001
          : 100000000,
        vsd_establishedconfidentialityguidelines:
          trans.establishedConfidentialityGuidelines ? 100000001 : 100000000,
      },
    ],
    organization: {
      accountid: trans.accountId,
      address1_city: trans.applicantInformation.mailingAddress.city,
      address1_country: trans.applicantInformation.mailingAddress.country,
      address1_line1: trans.applicantInformation.mailingAddress.line1,
      address1_line2: trans.applicantInformation.mailingAddress.line2,
      address1_postalcode: trans.applicantInformation.mailingAddress.postalCode,
      address1_stateorprovince:
        trans.applicantInformation.mailingAddress.province,
      name: trans.organizationName,
    },
  };

  const addProgramContactCollection: DynamicsCAPApplicationProgramContactPost[] =
    [];
  const removeProgramContactCollection: DynamicsCAPApplicationProgramContactPost[] =
    [];
  trans.capPrograms.forEach((program: iCAPProgram) => {
    // in each program add the list of staff by their id
    program.additionalStaff.forEach((s: iPerson): void => {
      addProgramContactCollection.push({
        contactid: s.personId,
        vsd_programid: program.programId,
      });
    });

    program.removedStaff.forEach((s: iPerson): void => {
      removeProgramContactCollection.push({
        contactid: s.personId,
        vsd_programid: program.programId,
      });
    });
  });
  if (addProgramContactCollection.length)
    post.addProgramContactCollection = addProgramContactCollection;
  if (removeProgramContactCollection.length)
    post.removeProgramContactCollection = removeProgramContactCollection;

  const programCollection: DynamicsCAPApplicationProgramPost[] = [];
  trans.capPrograms.forEach((program: iCAPProgram) => {
    programCollection.push({
      vsd_ContactLookupfortunecookiebind: program.programContact
        ? program.programContact.personId
        : null,
      vsd_programid: program.programId,
      vsd_cpu_fundingamountrequested: parseFloat(
        program.applicationAmount ? program.applicationAmount.toString() : "0",
      ),
      vsd_cpu_programmodeltypes: program.typesOfModels,
      vsd_otherprogrammodels: program.otherModel,
      vsd_cpu_programevaluationefforts: program.evaluation
        ? boolOptionSet.isTrue
        : boolOptionSet.isFalse,
      vsd_cpu_programevaluationdescription: program.evaluationDescription,
      vsd_cpu_capprogramoperationscomments: program.additionalComments,
    });
    if (programCollection.length) post.programCollection = programCollection;
  });
  return post;
}
