namespace Gov.Cscp.Victims.Public.Models
{
    public class ScheduleGLineItemDto
    {
        public string Vsd_ScheduleGLineItemId { get; set; }

        // Lookup references
        public string Vsd_ExpenseLineItemValue { get; set; }
        public string Vsd_ScheduleGIdValue { get; set; }

        // Budget fields (read-only from Dynamics)
        public decimal? Vsd_AnnualBudgetedAmount { get; set; }
        public decimal? Vsd_QuarterlyBudgetedAmount { get; set; }

        public decimal? Vsd_ActualExpensesCurrentQuarter { get; set; }
        public decimal? Vsd_QuarterlyVariance { get; set; }
        public decimal? Vsd_ActualExpendituresYearToDate { get; set; }
        public decimal? Vsd_YearToDateVariance { get; set; }

        public string Vsd_ExplanationForVariance { get; set; }

        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
