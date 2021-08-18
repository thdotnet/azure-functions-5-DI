using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using DemoAzureFunctions5.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DemoAzureFunctions5
{
    public class SequentialGuidFunction
    {
        private readonly ISequentialGuidService _sequentialGuidService;
        public SequentialGuidFunction(ISequentialGuidService sequentialGuidService)
        {
            _sequentialGuidService = sequentialGuidService;
        }

        [Function(nameof(SequentialGuidFunction))]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("Azure Functions running in .NET 5");


            var guid = _sequentialGuidService.Generate();

            logger.LogInformation(JsonSerializer.Serialize(
                guid,
                new JsonSerializerOptions() { WriteIndented = true }
            ));

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString($"Generated guid: {guid}");

            return response;
        }

    }
}
