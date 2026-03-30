import { iSignature } from "../../authenticated/subforms/program-authorizer/program-authorizer.component";
import {
  BudgetProposalDto,
  EligibleExpenseItemDto,
  ProgramBudgetDto,
  ProgramExpenseDto,
  ProgramRevenueSourceDto,
  ProgramTypeDto,
} from "../api/models";
import { revenueSourceType } from "../constants/revenue-source-type";
import { iExpenseItem } from "./expense-item.interface";
import { iProgramBudget } from "./program-budget.interface";
import { iRevenueSource } from "./revenue-source.interface";
import { iSalaryAndBenefits } from "./salary-and-benefits.interface";

export class TransmogrifierBudgetProposal {
  public organizationId: string;
  public userId: string;
  public contractId: string;
  public programBudgets: iProgramBudget[];
  public dict: object;
  public signature: iSignature;
  constructor(g: BudgetProposalDto) {
    // make private dict for looking up guids
    this.dict = this.buildDict(g);
    this.userId = g.userbceid; // this is the user's bceid
    this.organizationId = g.businessbceid; // this is the organization's bceid
    this.contractId = g.contract.vsd_ContractId; // the contract's id
    this.programBudgets = this.buildBudgetProposals(g);
    this.signature = this.buildSignature(g);
  }
  private buildSignature(b: BudgetProposalDto): iSignature {
    return {
      signer: undefined,
      signature: "",
      signatureDate: undefined,
      termsConfirmation: false,
    };
  }
  private buildDict(g: BudgetProposalDto): object {
    // Note: This only works if all guids in dynamics are unique.
    // create a lookup dictionary. It makes an object where a guid is the property and holds a human readable name as the value.
    // e.g. {qw87e6radsa:"Human name", ew491278938:"Useful description"}
    // in the case that there are no eligible expense items how can we reduce? Null check to avoid reducing an empty set.
    if (
      g.eligibleExpenseItemCollection &&
      g.eligibleExpenseItemCollection.length
    ) {
      const dict = g.eligibleExpenseItemCollection
        .map((s: EligibleExpenseItemDto): object => {
          if (s.vsd_EligibleExpenseItemId && s.vsd_Name) {
            // make an object to hold the kv pair
            const tmp = {};
            // assign the name to a property with matching guid
            tmp[s.vsd_EligibleExpenseItemId] = s.vsd_Name;
            return tmp;
          }
        })
        .reduce((prev, curr) => {
          // put the objects together into one mega lookup object
          return { ...curr, ...prev };
        });
      // add all of the program type properties to the dict as well
      g.programTypeCollection.forEach((p: ProgramTypeDto) => {
        dict[p.vsd_ProgramTypeId] = p.vsd_Name;
      });
      return dict;
    } else {
      return {};
    }
  }
  private buildBudgetProposals(g: BudgetProposalDto): iProgramBudget[] {
    return g.programCollection.map((d: ProgramBudgetDto): iProgramBudget => {
      return {
        contractId: g.contract.vsd_ContractId || "",
        programId: d.vsd_ProgramId || "",
        name: d.vsd_Name || "",
        type: this.dict[d.vsd_ProgramTypeValue]
          ? this.dict[d.vsd_ProgramTypeValue]
          : "Program type not set.",
        email: d.vsd_EmailAddress || "",
        administrationCosts: this.expenseItems(
          g.administrationCostCollection,
          d.vsd_ProgramId,
        ),
        administrationOtherExpenses: this.expenseItems(
          g.administrationCostCollection,
          d.vsd_ProgramId,
          true,
        ),
        programDeliveryCosts: this.expenseItems(
          g.programDeliveryCostCollection,
          d.vsd_ProgramId,
        ),
        programDeliveryOtherExpenses: this.expenseItems(
          g.programDeliveryCostCollection,
          d.vsd_ProgramId,
          true,
        ),
        revenueSources: this.buildRevenueSources(g, d.vsd_ProgramId),
        salariesAndBenefits: this.buildSalariesAndBenefits(g, d.vsd_ProgramId),
        contactLookupId: d.vsd_ContactLookupValue || null,
        currentTab: "Program Revenue Information",
      };
    });
  }
  private buildRevenueSources(
    g: BudgetProposalDto,
    programId: string,
  ): iRevenueSource[] {
    // for each revenue source in the collection build it into something useful
    return (
      g.programRevenueSourceCollection
        // get rid of all other programs
        .filter(
          (prs: ProgramRevenueSourceDto) =>
            prs.vsd_ProgramIdValue === programId,
        )
        .map((prs: ProgramRevenueSourceDto): iRevenueSource => {
          return {
            revenueSourceName:
              revenueSourceType(prs.vsd_Cpu_RevenueSourceType) || "",
            cash: prs.vsd_CashContribution || 0,
            cashMask: prs.vsd_CashContribution
              ? prs.vsd_CashContribution.toString()
              : "0",
            inKindContribution: prs.vsd_InKindContribution || 0,
            inKindContributionMask: prs.vsd_InKindContribution
              ? prs.vsd_InKindContribution.toString()
              : "0",
            other: prs.vsd_Cpu_OtherRevenueSource || "",
            revenueSourceId: prs.vsd_ProgramRevenueSourceId || null,
            total:
              prs.vsd_CashContribution || 0 + prs.vsd_InKindContribution || 0,
            totalMask: (
              prs.vsd_CashContribution ||
              0 + prs.vsd_InKindContribution ||
              0
            ).toString(),
            isActive: true,
          };
        })
    );
  }
  private buildSalariesAndBenefits(
    g: BudgetProposalDto,
    programId: string,
  ): iSalaryAndBenefits[] {
    return (
      g.salaryAndBenefitCollection
        // get rid of all other programs
        .filter((e: ProgramExpenseDto) => e.vsd_ProgramIdValue === programId)
        .map((e: ProgramExpenseDto): iSalaryAndBenefits => {
          // data munging
          return {
            title: e.vsd_Cpu_TitlePosition || "",
            salary: e.vsd_Cpu_Salary || 0,
            salaryMask: e.vsd_Cpu_Salary ? e.vsd_Cpu_Salary.toString() : "0",
            benefits: e.vsd_Cpu_Benefits || 0,
            benefitsMask: e.vsd_Cpu_Benefits
              ? e.vsd_Cpu_Benefits.toString()
              : "0",
            fundedFromVscp: e.vsd_Cpu_FundedFromVscp || 0,
            fundedFromVscpMask: e.vsd_Cpu_FundedFromVscp
              ? e.vsd_Cpu_FundedFromVscp.toString()
              : "0",
            totalCost: e.vsd_TotalCost || 0,
            totalCostMask: e.vsd_TotalCost ? e.vsd_TotalCost.toString() : "0",
            uuid: e.vsd_ProgramExpenseId || null,
            isActive: true,
          };
        })
    );
  }
  private expenseItems(
    items: ProgramExpenseDto[],
    programId: string,
    other = false,
  ): iExpenseItem[] {
    // by turning on the other variable we return only the "other" category of items from the list
    // we determine what is considered other by checking if a property exists for "vsd_cpu_otherexpense"
    return (
      items
        // get rid of all other programs
        .filter(
          (pdc: ProgramExpenseDto) => pdc.vsd_ProgramIdValue === programId,
        )
        // if we want to return the "other expenses" we check for the existence of the other expense property and if it exists we return true otherwise we pick the values that are missing the "other expense" property
        .filter((pdc: ProgramExpenseDto) => {
          let name = this.dict[pdc.vsd_EligibleExpenseItemIdValue];
          if (other) {
            return (
              !!pdc.vsd_Cpu_OtherExpense ||
              name === "Other Program Related Expenses" ||
              name === "Other Administration Costs"
            );
          } else {
            return (
              !pdc.vsd_Cpu_OtherExpense &&
              name !== "Other Program Related Expenses" &&
              name !== "Other Administration Costs"
            );
          }
        })
        .map((pe: ProgramExpenseDto): iExpenseItem => {
          return {
            uuid: pe.vsd_ProgramExpenseId || null,
            // if we are returning only the other expenses we use the other expense field as the name
            itemName:
              this.dict[pe.vsd_EligibleExpenseItemIdValue] || "Name error!",
            otherExpenseDescription: other ? pe.vsd_Cpu_OtherExpense : null,
            fundedFromVscp: pe.vsd_Cpu_FundedFromVscp || 0,
            fundedFromVscpMask: pe.vsd_Cpu_FundedFromVscp
              ? pe.vsd_Cpu_FundedFromVscp.toString()
              : "0",
            cost: pe.vsd_TotalCost,
            costMask: pe.vsd_TotalCost ? pe.vsd_TotalCost.toString() : "0",
            isActive: true,
          };
        })
    );
  }
}
