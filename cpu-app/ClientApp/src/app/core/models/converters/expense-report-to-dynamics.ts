import { DynamicsScheduleGCollectionPost } from "../../api/models/dynamicsScheduleGCollectionPost";
import { DynamicsScheduleGLineItemCollectionPost } from "../../api/models/dynamicsScheduleGLineItemCollectionPost";
import { ExpenseReportPost } from "../../api/models/expenseReportPost";
import { TransmogrifierExpenseReport } from "../transmogrifier-expense-report.class";

export function convertExpenseReportToDynamics(
  trans: TransmogrifierExpenseReport,
  isSubmit: boolean = false,
): ExpenseReportPost {
  const sg: DynamicsScheduleGCollectionPost = {
    // administration costs
    vsd_programadministrationexplanation:
      trans.expenseReport.administrationDescription,
    vsd_programadministrationcurrentquarter:
      trans.expenseReport.administrationValue,
    vsd_quarterlyvarianceprogramadministration:
      (trans.expenseReport.administrationQuarterlyBudget || 0) -
      (trans.expenseReport.administrationValue || 0),
    vsd_yeartodateprogramadministration:
      (trans.expenseReport.administrationValue || 0) +
      (trans.expenseReport.administrationYearToDate || 0),
    vsd_yeartodatevarianceprogramadministration:
      (trans.expenseReport.administrationAnnualBudget || 0) -
      ((trans.expenseReport.administrationValue || 0) +
        (trans.expenseReport.administrationYearToDate || 0)),

    // program delivery costs
    vsd_programdeliveryexplanations:
      trans.expenseReport.programDeliveryDescription,
    vsd_programdeliverycurrentquarter: trans.expenseReport.programDeliveryValue,
    vsd_quarterlyvarianceprogramdelivery:
      (trans.expenseReport.programDeliveryQuarterlyBudget || 0) -
      (trans.expenseReport.programDeliveryValue || 0),
    vsd_yeartodateprogramdelivery:
      (trans.expenseReport.programDeliveryValue || 0) +
      (trans.expenseReport.programDeliveryYearToDate || 0),
    vsd_yeartodatevarianceprogramdelivery:
      (trans.expenseReport.programDeliveryAnnualBudget || 0) -
      ((trans.expenseReport.programDeliveryValue || 0) +
        (trans.expenseReport.programDeliveryYearToDate || 0)),

    // salaries and benefits costs
    vsd_salariesandbenefitsexplanation:
      trans.expenseReport.salariesBenefitsDescription,
    vsd_salariesbenefitscurrentquarter:
      trans.expenseReport.salariesBenefitsValue,
    vsd_quarterlyvariancesalariesbenefits:
      (trans.expenseReport.salariesBenefitsQuarterlyBudget || 0) -
      (trans.expenseReport.salariesBenefitsValue || 0),
    vsd_yeartodatesalariesandbenefits:
      (trans.expenseReport.salariesBenefitsValue || 0) +
      (trans.expenseReport.salariesBenefitsYearToDate || 0),
    vsd_yeartodatevariancesalariesbenefits:
      (trans.expenseReport.salariesBenefitsAnnualBudget || 0) -
      ((trans.expenseReport.salariesBenefitsValue || 0) +
        (trans.expenseReport.salariesBenefitsYearToDate || 0)),

    // contract service hours
    vsd_actualhoursthisquarter: trans.expenseReport.serviceHoursQuarterlyActual,

    vsd_reportreviewed: isSubmit
      ? trans.expenseReport.executiveReview || false
      : false,
    vsd_schedulegid: trans.expenseReport.expenseReportId,
  };

  const lineItems: DynamicsScheduleGLineItemCollectionPost[] =
    trans.expenseReport.programExpenseLineItems.map((y) => ({
      vsd_scheduleglineitemid: y.itemId,
      vsd_actualexpensescurrentquarter: y.actual || 0,
      vsd_quarterlyvariance: (y.quarterlyBudget || 0) - (y.actual || 0),
      vsd_actualexpendituresyeartodate:
        (y.actual || 0) + (y.actualYearToDate || 0),
      vsd_yeartodatevariance:
        (y.annualBudget || 0) - ((y.actual || 0) + (y.actualYearToDate || 0)),
      vsd_explanationforvariance: y.description,
    }));

  return {
    businessBCeID: trans.organizationId,
    userBCeID: trans.userId,
    scheduleGCollection: [sg],
    scheduleGLineItemCollection: lineItems,
  };
}
