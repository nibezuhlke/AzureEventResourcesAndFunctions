// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using UtilitiesLibrary;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleEventGridSubscriber
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            //log.LogInformation(eventGridEvent.Data.ToString());
            await CosmosDBLogging.WriteLogAsync(eventGridEvent.Data.ToString());
        }
    }
}
