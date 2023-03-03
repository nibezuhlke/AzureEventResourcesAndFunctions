// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json.Linq;

Console.WriteLine("Publishing to the Azure Event Grid!");

var topicEndpoint = "https://nibetopic1.switzerlandnorth-1.eventgrid.azure.net/api/events";
var topicKey = "B8c7GxnlSy/SQpBYAA6awukoO8jp33H9RTFX+qvcJ4o=";
var topicHostname = new Uri(topicEndpoint).Host;

var credentials = new TopicCredentials(topicKey);
var client = new EventGridClient(credentials);

var events = new[]
{
    new EventGridEvent
    {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "ExampleEvent",
                    EventTime = DateTime.UtcNow,
                    Subject = "MyExampleSubject",
                    Data = JObject.FromObject(new {
                        Name = "Nikolaos Bellias",
                        Age = 56
                    }),
                    DataVersion = "1.0"
    }
};

await client.PublishEventsAsync(topicHostname, events);
