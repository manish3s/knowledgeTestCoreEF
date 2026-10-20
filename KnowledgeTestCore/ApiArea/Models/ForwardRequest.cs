namespace KnowledgeTestCore.ApiArea.Models
{
    public class ForwardRequest
    {
        // The external URL to forward the POST to (full URL with scheme)
        public string TargetUrl { get; set; }

        // Raw payload to send. If ContentType is application/json, provide a JSON string.
        public string Payload { get; set; }

        // Optional content-type header for the forwarded request. Defaults to application/json.
        public string ContentType { get; set; } = "application/json";
    }
}
