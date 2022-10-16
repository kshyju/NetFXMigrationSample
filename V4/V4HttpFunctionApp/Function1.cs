using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace V4HttpFunctionApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            HttpStatusCode statusCode;
            string body;
            Person payload = await req.ReadFromJsonAsync<Person>();
            if (payload is null)
            {
                statusCode = HttpStatusCode.BadRequest;
                body = "Please pass a payload with Name in the request body";
            }
            else
            {
                statusCode = HttpStatusCode.OK;
                body = $"Hello {payload.Name}";
            }

            var response = req.CreateResponse(statusCode);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(body);

            return response;
        }
    }
    public class Person
    {
        public string Name { get; set; }
    }
}
