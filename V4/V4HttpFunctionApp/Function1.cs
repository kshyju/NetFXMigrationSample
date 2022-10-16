using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace V1HttpFunctionApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            HttpStatusCode statusCode;
            string body;
            dynamic payload = await req.ReadFromJsonAsync<object>();
            if (payload != null)
            {
                statusCode = HttpStatusCode.OK;
                body = $"Hello {payload.name}";
            }
            else
            {
                statusCode = HttpStatusCode.BadRequest;
                body = "Please pass a name in the request body";
            }

            var response = req.CreateResponse(statusCode);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(body);

            return response;
        }
    }
}
