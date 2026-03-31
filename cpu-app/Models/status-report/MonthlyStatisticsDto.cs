using System;

namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>Returned by GET /api/StatusReport/monthly_stats/{businessBceid}/{userBceid}/{contractId}</summary>
    public class MonthlyStatisticsDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public string Businessbceid { get; set; }
        public string Userbceid { get; set; }

        public DataCollectionItemDto[] DataCollection { get; set; }
        public ContactMinimalDto[] ContactCollection { get; set; }
        public ProgramMinimalDto[] ProgramCollection { get; set; }
    }

    public class DataCollectionItemDto
    {
        public string vsd_datacollectionid { get; set; }
        public string vsd_name { get; set; }
        public int? vsd_reportingperiod { get; set; }
        public int? statuscode { get; set; }
        public string vsd_submissiondate { get; set; }
        public string _vsd_contact_value { get; set; }
        public DateTime? createdon { get; set; }
        public string _vsd_program_value { get; set; }
    }

    public class ContactMinimalDto
    {
        public string contactid { get; set; }
        public string fullname { get; set; }
    }

    public class ProgramMinimalDto
    {
        public string vsd_programid { get; set; }
        public string vsd_name { get; set; }
    }
}
