namespace Gov.Cscp.Victims.Public.Models
{
    public class BudgetProposalDto
    {
        public ProgramExpenseDto[] AdministrationCostCollection { get; set; }
        public string Businessbceid { get; set; }
        public ContractBudgetDto Contract { get; set; }
        public EligibleExpenseItemDto[] EligibleExpenseItemCollection { get; set; }
        public bool IsSuccess { get; set; }
        public OrganizationDto Organization { get; set; }
        public PortalRoleDto[] PortalRoles { get; set; }
        public ProgramBudgetDto[] ProgramCollection { get; set; }
        public ProgramExpenseDto[] ProgramDeliveryCostCollection { get; set; }
        public ProgramRevenueSourceDto[] ProgramRevenueSourceCollection { get; set; }
        public ProgramTypeDto[] ProgramTypeCollection { get; set; }
        public string Result { get; set; }
        public ProgramExpenseDto[] SalaryAndBenefitCollection { get; set; }
        public string Userbceid { get; set; }
    }
}
