import { EligibleExpenseItemDto, ProgramSurplusDto } from "../api/models";
import { iSurplusItem } from "./surplus-item.interface";

export class TransmogrifierProgramSurplus {
  submitDate: Date;
  contractId: string;
  contractNumber: string;
  organizationId: string;
  organizationName: string;
  programId: string;
  programName: string;

  surplusPlanId: string;
  surplusAmount: number;
  pay_with_cheque: boolean;
  lineItems: iSurplusItem[];

  userId: string;

  constructor(g: ProgramSurplusDto) {
    this.contractId = g.contract?.vsd_ContractId;
    this.contractNumber = g.contract?.vsd_Name;
    this.organizationId = g.businessbceid;
    this.organizationName = g.organization?.name;
    this.programId = g.program?.vsd_ProgramId;
    this.programName = g.program?.vsd_Name;
    this.userId = g.userbceid;
    this.surplusPlanId = g.surplusPlan?.vsd_SurplusPlanReportId;
    this.surplusAmount = g.surplusPlan?.vsd_SurplusAmount || 0;
    this.pay_with_cheque = g.surplusPlan?.vsd_SurplusRemittance;
    this.lineItems = this.buildSurplusLineItems(g);
  }

  buildSurplusLineItems(g: ProgramSurplusDto) {
    const ret: iSurplusItem[] = [];
    const lineItems = g.surplusPlanLineItems || [];

    lineItems.forEach((item) => {
      const obj: iSurplusItem = {
        id: item.vsd_SurplusLineItemId,
        name: item.vsd_Name,
        expense_name: this.getExpenseName(
          item.vsd_EligibleExpenseItemIdValue,
          g.eligibleExpenseItemCollection || [],
        ),
        justification: item.vsd_JustificationDetails,
        surplus_plan_id: item.vsd_SurplusPlanIdValue,
        proposed_amount: item.vsd_ProposedExpenditures || 0,
        proposed_amount_mask: item.vsd_ProposedExpenditures
          ? item.vsd_ProposedExpenditures.toString()
          : "0",
        allocated_amount: item.vsd_AllocatedAmount || 0,
        allocated_amount_mask: item.vsd_AllocatedAmount
          ? item.vsd_AllocatedAmount.toString()
          : "0",
        expenditures_q1: item.vsd_ActualExpenditures || 0,
        q1_mask: item.vsd_ActualExpenditures
          ? item.vsd_ActualExpenditures.toString()
          : "0",
        expenditures_q2: item.vsd_ActualExpenditures2 || 0,
        q2_mask: item.vsd_ActualExpenditures2
          ? item.vsd_ActualExpenditures2.toString()
          : "0",
        expenditures_q3: item.vsd_ActualExpenditures3 || 0,
        q3_mask: item.vsd_ActualExpenditures3
          ? item.vsd_ActualExpenditures3.toString()
          : "0",
        expenditures_q4: item.vsd_ActualExpenditures4 || 0,
        q4_mask: item.vsd_ActualExpenditures4
          ? item.vsd_ActualExpenditures4.toString()
          : "0",
      };
      ret.push(obj);
    });

    return ret;
  }

  getExpenseName(expense_id: string, expenses: EligibleExpenseItemDto[]) {
    const found = expenses.find(
      (ex) => ex.vsd_EligibleExpenseItemId === expense_id,
    );
    return found ? found.vsd_Name : "";
  }
}
