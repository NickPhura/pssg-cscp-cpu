import { ExpenseReportDto } from "../api/models/expenseReportDto";
import { ExpenseItemLabels } from "../constants/expense-item-labels";
import { REPORTING_PERIODS } from "../constants/reporting-period";
import { iExpenseReport } from "./expense-report.interface";

export class TransmogrifierExpenseReport {
  public organizationId: string;
  public userId: string;
  public expenseReport: iExpenseReport;

  constructor(g: ExpenseReportDto) {
    this.userId = g.userbceid;
    this.organizationId = g.businessbceid;
    this.expenseReport = this.buildExpenseReport(g);
  }

  buildExpenseReport(g: ExpenseReportDto): iExpenseReport {
    const sg = g.scheduleG;
    const program = g.program;

    const e: iExpenseReport = {
      expenseReportId: sg?.vsd_ScheduleGId || null,
      reportingPeriod: REPORTING_PERIODS[sg?.vsd_Cpu_ReportingPeriod],

      // salaries and benefits costs
      salariesBenefitsDescription: sg?.vsd_SalariesAndBenefitsExplanation || "",
      salariesBenefitsAnnualBudget: sg?.vsd_SalaryAndBenefitsBudgeted || 0,
      salariesBenefitsQuarterlyBudget:
        sg?.vsd_QuarterlyBudgetedSalariesBenefits || 0,
      salariesBenefitsValue: sg?.vsd_SalariesBenefitsCurrentQuarter || 0,
      salariesBenefitsMask:
        sg?.vsd_SalariesBenefitsCurrentQuarter?.toString() ?? "0",
      salariesBenefitsQuarterlyVariance:
        sg?.vsd_QuarterlyVarianceSalariesBenefits || 0,
      // computed FE-only: prior YTD = total YTD minus this quarter
      salariesBenefitsYearToDate:
        (sg?.vsd_YearToDateSalariesAndBenefits || 0) -
        (sg?.vsd_SalariesBenefitsCurrentQuarter || 0),
      salariesBenefitsYearToDateVariance:
        (sg?.vsd_YearToDateVarianceSalariesBenefits || 0) -
        (sg?.vsd_SalariesBenefitsCurrentQuarter || 0),

      // program delivery costs
      programDeliveryDescription: sg?.vsd_ProgramDeliveryExplanations || "",
      programDeliveryAnnualBudget: sg?.vsd_ProgramDeliveryBudgeted || 0,
      programDeliveryQuarterlyBudget:
        sg?.vsd_QuarterlyBudgetedProgramDelivery || 0,
      programDeliveryValue: sg?.vsd_ProgramDeliveryCurrentQuarter || 0,
      programDeliveryMask:
        sg?.vsd_ProgramDeliveryCurrentQuarter?.toString() ?? "0",
      programDeliveryQuarterlyVariance:
        sg?.vsd_QuarterlyVarianceProgramDelivery || 0,
      programDeliveryYearToDate:
        (sg?.vsd_YearToDateProgramDelivery || 0) -
        (sg?.vsd_ProgramDeliveryCurrentQuarter || 0),
      programDeliveryYearToDateVariance:
        (sg?.vsd_YearToDateVarianceProgramDelivery || 0) -
        (sg?.vsd_ProgramDeliveryCurrentQuarter || 0),

      // administration costs
      administrationDescription: sg?.vsd_ProgramAdministrationExplanation || "",
      administrationAnnualBudget: sg?.vsd_ProgramAdministrationBudgeted || 0,
      administrationQuarterlyBudget:
        sg?.vsd_QuarterlyBudgetedProgramAdministration || 0,
      administrationValue: sg?.vsd_ProgramAdministrationCurrentQuarter || 0,
      administrationMask:
        sg?.vsd_ProgramAdministrationCurrentQuarter?.toString() ?? "0",
      administrationQuarterlyVariance:
        sg?.vsd_QuarterlyVarianceProgramAdministration || 0,
      administrationYearToDate:
        (sg?.vsd_YearToDateProgramAdministration || 0) -
        (sg?.vsd_ProgramAdministrationCurrentQuarter || 0),
      administrationYearToDateVariance:
        (sg?.vsd_YearToDateVarianceProgramAdministration || 0) -
        (sg?.vsd_ProgramAdministrationCurrentQuarter || 0),

      // contract service hours
      serviceHoursQuarterlyActual: sg?.vsd_ActualHoursThisQuarter || 0,
      serviceHours: program?.vsd_Cpu_NumberOfHours || 0,
      perType: program?.vsd_Cpu_Per || 100000000,
      onCallStandByHours: program?.vsd_TotalOnCallStandbyHours || 0,
      executiveReview: sg?.vsd_ReportReviewed || false,

      programExpenseLineItems: [],
    };

    for (const item of g.scheduleGLineItems ?? []) {
      if (item.vsd_ScheduleGIdValue === sg?.vsd_ScheduleGId) {
        e.programExpenseLineItems.push({
          itemId: item.vsd_ScheduleGLineItemId,
          label:
            ExpenseItemLabels[item.vsd_ExpenseLineItemValue?.toUpperCase()] ||
            "Unknown Line Item Type",
          annualBudget: item.vsd_AnnualBudgetedAmount || 0,
          quarterlyBudget: item.vsd_QuarterlyBudgetedAmount || 0,
          actual: item.vsd_ActualExpensesCurrentQuarter || 0,
          mask: item.vsd_ActualExpensesCurrentQuarter?.toString() ?? "0",
          quarterlyVariance: item.vsd_QuarterlyVariance || 0,
          actualYearToDate:
            (item.vsd_ActualExpendituresYearToDate || 0) -
            (item.vsd_ActualExpensesCurrentQuarter || 0),
          yearToDateVariance:
            (item.vsd_YearToDateVariance || 0) -
            (item.vsd_ActualExpensesCurrentQuarter || 0),
          description: item.vsd_ExplanationForVariance,
        });
      }
    }
    return e;
  }
}
