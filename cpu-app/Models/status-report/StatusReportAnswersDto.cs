namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>Returned by GET /api/StatusReport/data_collection/{businessBceid}/{userBceid}/{dataCollectionId}</summary>
    public class StatusReportAnswersDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public string Businessbceid { get; set; }
        public string Userbceid { get; set; }
        public int? ReportingPeriod { get; set; }

        public StatusReportAnswerItemDto[] AnswerCollection { get; set; }
        public StatusReportCategoryDto[] CategoryCollection { get; set; }
        public StatusReportContactDto Contact { get; set; }
        public StatusReportContractDto Contract { get; set; }
        public StatusReportOrganizationDto Organization { get; set; }
        public StatusReportProgramDto Program { get; set; }
    }

    /// <summary>Returned by POST /api/StatusReport/{taskId}</summary>
    public class SetStatusReportAnswersResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
    }
}
