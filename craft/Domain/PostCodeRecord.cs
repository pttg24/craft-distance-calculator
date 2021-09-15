namespace craft.Domain
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class PostCodeRecord
    {
        [BsonId]
        public string Id { get; set; }
        public string Postcode { get; set; }
        public string Ccg { get; set; }
        public string Nuts { get; set; }
        public string AdminWard { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double Km { get; set; }
        public double Miles { get; set; }
        public DateTime ProcessedOn { get; set; }
    }
}