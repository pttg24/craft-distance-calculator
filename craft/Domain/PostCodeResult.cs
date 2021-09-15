namespace craft.Domain
{
    using System.Collections.Generic;

    public class PostCodeResult
    {
        public Distance Distance { get; set; }
        public PostCodeResponse PostCode { get; set; }
        public List<PostCodeRecord> Last3Records { get; set; }
    }
}
