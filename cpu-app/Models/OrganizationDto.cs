namespace Gov.Cscp.Victims.Public.Models
{
    public class OrganizationDto
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Telephone1 { get; set; }
        public string EmailAddress1 { get; set; }
        public string Fax { get; set; }
        public string Address1City { get; set; }
        public string Address1Line1 { get; set; }
        public string Address1Line2 { get; set; }
        public string Address1PostalCode { get; set; }
        public string Address1StateOrProvince { get; set; }
        public string Address1Country { get; set; }
        public string Address2City { get; set; }
        public string Address2Line1 { get; set; }
        public string Address2Line2 { get; set; }
        public string Address2PostalCode { get; set; }
        public string Address2StateOrProvince { get; set; }
        public string Address2Country { get; set; }
        public string ExecutiveContactIdValue { get; set; }
        public string BoardContactIdValue { get; set; }

        // Metadata fields
        public string FortuneCookieType { get; set; }
        public string FortuneCookieEtag { get; set; }
    }
}
