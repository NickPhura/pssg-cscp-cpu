namespace Gov.Cscp.Victims.Public.Models
{
    public class ContactDto
    {
        public string ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress1 { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string JobTitle { get; set; }
        public string Address1City { get; set; }
        public string Address1Line1 { get; set; }
        public string Address1Line2 { get; set; }
        public string Address1PostalCode { get; set; }
        public string Address1StateOrProvince { get; set; }
        public string Vsd_BceId { get; set; }
        public string Vsd_MainPhoneExtension { get; set; }
        public string Telephone2 { get; set; }
        public string Vsd_HomePhoneExtension { get; set; }
        public int? Vsd_EmploymentStatus { get; set; }
        public int? StateCode { get; set; }

        // Metadata fields
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
