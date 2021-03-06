namespace craft.Domain
{
    public class PostCodeResponseDetails
    {
        public string Postcode { get; set; }
        public int Quality { get; set; }
        public int Eastings { get; set; }
        public int Northings { get; set; }
        public string Country { get; set; }
        public string NhsHa { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string EuropeanElectoralRegion { get; set; }
        public string PrimaryCareTrust { get; set; }
        public string Region { get; set; }
        public string Lsoa { get; set; }
        public string Msoa { get; set; }
        public string Incode { get; set; }
        public string Outcode { get; set; }
        public string ParliamentaryConstituency { get; set; }
        public string AdminDistrict { get; set; }
        public string Parish { get; set; }
        public string AdminCounty { get; set; }
        public string AdminWard { get; set; }
        public string Ced { get; set; }
        public string Ccg { get; set; }
        public string Nuts { get; set; }
        public PostCode Codes { get; set; }
    }
}
