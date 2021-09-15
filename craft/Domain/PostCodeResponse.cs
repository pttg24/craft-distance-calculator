namespace craft.Domain
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class PostCodeResponse
    {
        public string Status { get; set; }
        public PostCodeResponseDetails Result { get; set; }

    }
}
