namespace Gov.Cscp.Victims.Public.Models
{
    public class ScheduleGDto
    {
        public string Vsd_ScheduleGId { get; set; }

        public decimal? Vsd_ActualHoursThisQuarter { get; set; }
        public decimal? Vsd_ContractedServiceHrsThisQuarter { get; set; }

        public decimal? Vsd_ProgramAdministrationCurrentQuarter { get; set; }
        public decimal? Vsd_QuarterlyVarianceProgramAdministration { get; set; }
        public decimal? Vsd_YearToDateProgramAdministration { get; set; }
        public decimal? Vsd_YearToDateVarianceProgramAdministration { get; set; }

        public decimal? Vsd_ProgramDeliveryCurrentQuarter { get; set; }
        public decimal? Vsd_QuarterlyVarianceProgramDelivery { get; set; }
        public decimal? Vsd_YearToDateProgramDelivery { get; set; }
        public decimal? Vsd_YearToDateVarianceProgramDelivery { get; set; }

        public decimal? Vsd_SalariesBenefitsCurrentQuarter { get; set; }
        public decimal? Vsd_QuarterlyVarianceSalariesBenefits { get; set; }
        public decimal? Vsd_YearToDateSalariesAndBenefits { get; set; }
        public decimal? Vsd_YearToDateVarianceSalariesBenefits { get; set; }

        // Budget fields (read-only from Dynamics)
        public decimal? Vsd_SalaryAndBenefitsBudgeted { get; set; }
        public decimal? Vsd_QuarterlyBudgetedSalariesBenefits { get; set; }
        public decimal? Vsd_ProgramDeliveryBudgeted { get; set; }
        public decimal? Vsd_QuarterlyBudgetedProgramDelivery { get; set; }
        public decimal? Vsd_ProgramAdministrationBudgeted { get; set; }
        public decimal? Vsd_QuarterlyBudgetedProgramAdministration { get; set; }

        // Reporting period (quarter)
        public int? Vsd_Cpu_ReportingPeriod { get; set; }

        public string Vsd_ProgramAdministrationExplanation { get; set; }
        public string Vsd_ProgramDeliveryExplanations { get; set; }
        public string Vsd_SalariesAndBenefitsExplanation { get; set; }

        public bool? Vsd_ReportReviewed { get; set; }

        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
