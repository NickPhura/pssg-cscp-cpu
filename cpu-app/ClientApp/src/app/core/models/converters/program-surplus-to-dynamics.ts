import {
  DynamicsProgramSurplusLineItemPost,
  ProgramSurplusPost,
} from "../../api/models";
import { TransmogrifierProgramSurplus } from "../transmogrifier-program-surplus.class";

export enum SurplusTypes {
  Plan,
  Report,
}

export function convertProgramSurplusToDynamics(
  trans: TransmogrifierProgramSurplus,
  type: SurplusTypes,
  isSubmit: boolean = false,
): ProgramSurplusPost {
  const ret: ProgramSurplusPost = {
    businessBCeID: trans.organizationId,
    userBCeID: trans.userId,
    surplusPlanCollection: [
      {
        vsd_surplusplanreportid: trans.surplusPlanId,
        vsd_surplusremittance: trans.pay_with_cheque,
      },
    ],
    surplusPlanLineItemCollection: [],
  };

  if (isSubmit) {
    ret.surplusPlanCollection[0].vsd_datesubmitted = new Date().toISOString();
  }

  trans.lineItems.forEach((item) => {
    const dynamicsItem: DynamicsProgramSurplusLineItemPost = {
      vsd_surpluslineitemid: item.id,
      vsd_allocatedamount: item.allocated_amount,
    };

    if (type === SurplusTypes.Plan) {
      dynamicsItem.vsd_justificationdetails = item.justification;
      dynamicsItem.vsd_proposedexpenditures = item.proposed_amount;
    }
    if (type === SurplusTypes.Report) {
      dynamicsItem.vsd_actualexpenditures = item.expenditures_q1;
      dynamicsItem.vsd_actualexpenditures2 = item.expenditures_q2;
      dynamicsItem.vsd_actualexpenditures3 = item.expenditures_q3;
      dynamicsItem.vsd_actualexpenditures4 = item.expenditures_q4;
    }
    if (isSubmit) {
      dynamicsItem.vsd_datesubmitted = new Date().toISOString();
    }
    ret.surplusPlanLineItemCollection.push(dynamicsItem);
  });

  return ret;
}
