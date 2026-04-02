import {
  BudgetProposalPost,
  DynamicsBudgetProposalExpensePost,
  DynamicsBudgetProposalRevenueSourcePost,
} from "../../api/models";
import { nameAssemble } from "../../constants/name-assemble";
import { revenueSourceValue } from "../../constants/revenue-source-type";
import { iExpenseItem } from "../expense-item.interface";
import { iProgramBudget } from "../program-budget.interface";
import { iRevenueSource } from "../revenue-source.interface";
import { iSalaryAndBenefits } from "../salary-and-benefits.interface";
import { TransmogrifierBudgetProposal } from "../transmogrifier-budget-proposal.class";

const expenseType = {
  // vsd_cpu_programexpensetype converter
  salaries_and_benefits: 100000000,
  program_delivery: 100000001,
  administrative: 100000002,
};
export function convertBudgetProposalToDynamics(
  trans: TransmogrifierBudgetProposal,
  isSubmit: boolean = false,
): BudgetProposalPost {
  // when we need the matching guid we can look it up from the text. this flips the dict's property and value. Which is fine because it is a string.
  const reverseDict = {};
  for (let property in trans.dict) {
    if (trans.dict.hasOwnProperty(property)) {
      reverseDict[trans.dict[property]] = property;
    }
  }
  const p: BudgetProposalPost = {
    businessBCeID: trans.organizationId,
    userBCeID: trans.userId,
    programExpenseCollection: [],
    programRevenueSourceCollection: [],
    programCollection: [],
  };
  trans.programBudgets.forEach((pb: iProgramBudget) => {
    // ADMINISTRATIVE costs the 100000002's as defined in the excel sheet
    pb.administrationCosts
      .map((e: iExpenseItem): DynamicsBudgetProposalExpensePost => {
        return {
          // what guid location is the label? Null values get removed serverside
          vsd_EligibleExpenseItemIdfortunecookiebind:
            reverseDict[e.itemName] || null,
          // which program is this expense item associated to?
          vsd_ProgramIdfortunecookiebind: pb.programId,
          vsd_cpu_fundedfromvscp: e.fundedFromVscp || 0,
          vsd_cpu_programexpensetype: expenseType.administrative,
          vsd_inputamount: e.cost || 0,
          vsd_programexpenseid: e.uuid || null,
        };
      })
      .forEach((x: DynamicsBudgetProposalExpensePost) => {
        p.programExpenseCollection.push(x);
      });
    pb.administrationOtherExpenses
      .map((e: iExpenseItem): DynamicsBudgetProposalExpensePost => {
        return {
          // what guid location is the label? Null values get removed serverside
          // if null this gets created as a new "other" and will be returned in the eligibleexpenseitems collection on get
          vsd_EligibleExpenseItemIdfortunecookiebind:
            reverseDict[e.itemName] || null,
          // this is an other expense. Include the text
          // which program is this expense item associated to?
          vsd_ProgramIdfortunecookiebind: pb.programId,
          vsd_cpu_fundedfromvscp: e.fundedFromVscp || 0,
          vsd_cpu_otherexpense: e.otherExpenseDescription || null,
          vsd_cpu_programexpensetype: expenseType.administrative,
          vsd_inputamount: e.cost || 0,
          vsd_programexpenseid: e.uuid || null,
          statecode: e.isActive ? 0 : 1,
        };
      })
      .forEach((x: DynamicsBudgetProposalExpensePost) => {
        p.programExpenseCollection.push(x);
      });

    // PROGRAM DELIVERY costs the 100000001's as defined in the excel sheet
    pb.programDeliveryCosts
      .map((e: iExpenseItem): DynamicsBudgetProposalExpensePost => {
        return {
          // what guid location is the label? Null values get removed serverside
          vsd_EligibleExpenseItemIdfortunecookiebind:
            reverseDict[e.itemName] || null,
          // which program is this expense item associated to?
          vsd_ProgramIdfortunecookiebind: pb.programId,
          vsd_cpu_fundedfromvscp: e.fundedFromVscp || 0,
          vsd_cpu_programexpensetype: expenseType.program_delivery,
          vsd_inputamount: e.cost || 0,
          vsd_programexpenseid: e.uuid || null,
        };
      })
      .forEach((x: DynamicsBudgetProposalExpensePost) => {
        p.programExpenseCollection.push(x);
      });
    pb.programDeliveryOtherExpenses
      .map((e: iExpenseItem): DynamicsBudgetProposalExpensePost => {
        return {
          // what guid location is the label? Null values get removed serverside
          // if null this gets created as a new "other" and will be returned in the eligibleexpenseitems collection on get
          vsd_EligibleExpenseItemIdfortunecookiebind:
            reverseDict[e.itemName] || null,
          // this is an other expense. Include the text
          vsd_cpu_otherexpense: e.otherExpenseDescription || null,
          // which program is this expense item associated to?
          vsd_ProgramIdfortunecookiebind: pb.programId,
          vsd_cpu_programexpensetype: expenseType.program_delivery,
          vsd_inputamount: e.cost || 0,
          vsd_cpu_fundedfromvscp: e.fundedFromVscp || 0,
          vsd_programexpenseid: e.uuid || null,
          statecode: e.isActive ? 0 : 1,
        };
      })
      .forEach((x) => {
        p.programExpenseCollection.push(x);
      });

    // SALARY AND BENEFITS costs the 10000000 as defined in the excel sheet
    pb.salariesAndBenefits
      .map((e: iSalaryAndBenefits): DynamicsBudgetProposalExpensePost => {
        return {
          vsd_ProgramIdfortunecookiebind: pb.programId,
          vsd_cpu_benefits: e.benefits || 0,
          vsd_cpu_fundedfromvscp: e.fundedFromVscp || 0,
          vsd_cpu_programexpensetype: expenseType.salaries_and_benefits,
          vsd_cpu_salary: e.salary || 0,
          vsd_cpu_titleposition: e.title || "No title",
          vsd_programexpenseid: e.uuid || null,
          statecode: e.isActive ? 0 : 1,
        };
      })
      .forEach((x: DynamicsBudgetProposalExpensePost) => {
        p.programExpenseCollection.push(x);
      });

    // REVENUE SOURCES as defined in the excel sheet
    pb.revenueSources
      .map((e: iRevenueSource): DynamicsBudgetProposalRevenueSourcePost => {
        return {
          vsd_ProgramIdfortunecookiebind: pb.programId,
          vsd_cashcontribution: e.cash,
          vsd_cpu_revenuesourcetype: revenueSourceValue(e.revenueSourceName),
          vsd_inkindcontribution: e.inKindContribution,
          vsd_programrevenuesourceid: e.revenueSourceId,
          statecode: e.isActive ? 0 : 1,
          vsd_cpu_otherrevenuesource: e.other || null,
        };
      })
      .forEach((x: DynamicsBudgetProposalRevenueSourcePost) => {
        p.programRevenueSourceCollection.push(x);
      });

    p.programCollection.push({
      vsd_programid: pb.programId,
      vsd_signingofficersignature:
        trans.signature.signature && isSubmit
          ? trans.signature.signature
          : null,
      vsd_signingofficerfullname:
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
    });
  });
  return p;
}
