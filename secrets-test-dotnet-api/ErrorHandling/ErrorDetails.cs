using System.Text.Json;

namespace secrets_test_dotnet_api.ErrorHandling
{
    public class ErrorDetails
    {

        public string TraceId { get; set; }
        public string Message { get; set; }

        public ErrorDetails(string traceId, string message)
        {
            TraceId = traceId;
            Message = message;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}