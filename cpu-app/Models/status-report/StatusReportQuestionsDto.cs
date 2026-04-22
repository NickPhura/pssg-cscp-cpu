using System;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>Returned by GET /api/StatusReport/{businessBceid}/{userBceid}/{taskId}</summary>
    public class StatusReportQuestionsDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public string Businessbceid { get; set; }
        public string Userbceid { get; set; }
        public int? ReportingPeriod { get; set; }
        public string DataCollectionid { get; set; }

        public StatusReportContractDto Contract { get; set; }
        public StatusReportProgramDto Program { get; set; }
        public StatusReportOrganizationDto Organization { get; set; }

        public StatusReportCategoryDto[] CategoryCollection { get; set; }
        public StatusReportQuestionItemDto[] QuestionCollection { get; set; }
        public StatusReportMcQuestionDto[] MultipleChoiceCollection { get; set; }
        public StatusReportProgramTypeDto[] ProgramTypeCollection { get; set; }
        public StatusReportChildQuestionDto[] ChildQuestionCollection { get; set; }
        public StatusReportChildMcQuestionDto[] ChildMultipleChoiceCollection { get; set; }
        public StatusReportAnswerItemDto[] AnswerCollection { get; set; }
    }

    public class StatusReportContractDto
    {
        public string vsd_contractid { get; set; }
        public string vsd_name { get; set; }
        public string _vsd_customer_value { get; set; }
    }

    public class StatusReportProgramDto
    {
        public string vsd_programid { get; set; }
        public string vsd_name { get; set; }
        public string _vsd_contractid_value { get; set; }
        public string _vsd_programtype_value { get; set; }
        public string _vsd_serviceproviderid_value { get; set; }
        public int? vsd_cpu_numberofhours { get; set; }
    }

    public class StatusReportOrganizationDto
    {
        public string accountid { get; set; }
        public string name { get; set; }
    }

    public class StatusReportContactDto
    {
        public string contactid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    public class StatusReportCategoryDto
    {
        public string vsd_monthlystatisticscategoryid { get; set; }
        public string vsd_name { get; set; }
        public int? vsd_categoryorder { get; set; }
    }

    public class StatusReportQuestionItemDto
    {
        public string vsd_cpustatisticsmasterdataid { get; set; }
        public string categoryID { get; set; }
        public string _vsd_categoryid_value { get; set; }
        public string _vsd_cpuprogramtype_value { get; set; }
        public string vsd_name { get; set; }
        public int? vsd_questionorder { get; set; }
        public int? vsd_questiontype { get; set; }
        public string vsd_tooltip { get; set; }
    }

    public class StatusReportChildQuestionDto
    {
        public string vsd_cpustatisticsmasterdataid { get; set; }
        public string _vsd_categoryid_value { get; set; }
        public string _vsd_parentid_value { get; set; }
        public string vsd_name { get; set; }
        public int? vsd_questionorder { get; set; }
        public int? vsd_questiontype { get; set; }
        public string vsd_tooltip { get; set; }
    }

    public class StatusReportMcQuestionDto
    {
        public string vsd_cpustatisticsmasterdataanswerid { get; set; }
        public string vsd_name { get; set; }
        public string _vsd_questionid_value { get; set; }
    }

    public class StatusReportChildMcQuestionDto
    {
        public string vsd_cpustatisticsmasterdataanswerid { get; set; }
        public string vsd_name { get; set; }
        public string _vsd_questionid_value { get; set; }
        public string _vsd_parentid_value { get; set; }
    }

    public class StatusReportAnswerItemDto
    {
        public string _vsd_categoryid_value;
        public string vsd_datacollectionlineitemid { get; set; }
        public string vsd_name { get; set; }
        public string _vsd_datacollectionid_value { get; set; }
        public string vsd_questioncategory { get; set; }
        public int? vsd_questionorder { get; set; }
        public int? vsd_questiontype1 { get; set; }
        public string vsd_tooltip { get; set; }
        public int? vsd_yesno { get; set; }
        public string vsd_textanswer { get; set; }
        public decimal? vsd_number { get; set; }
        public DateTime? createdon { get; set; }
    }

    public class StatusReportProgramTypeDto
    {
        public string vsd_programtypeid { get; set; }
        public string vsd_name { get; set; }
    }
}
