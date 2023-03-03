using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;

namespace UtilitiesLibrary
{
    public class CosmosDBLogging
    {
        public static async Task WriteLogAsync(string message)
        {
            var cosmosEndpoint = "https://nibecosmosdb.documents.azure.com:443/";
            var cosmosKey = "HN9tvVmcGNHIXH3zIf09DBOH9VANbH0xrnEjhKPUwDdmcGxQ7aGUSx8277BEK2Kd3Oc9FJooa8CUACDbmJUgTQ==";
            var cosmosDatabase = "LoggingDB";
            var cosmosContainer = "LogMessages";

            var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
            var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosDatabase);
            var containerInstance = await database.Database.CreateContainerIfNotExistsAsync(cosmosContainer, "/id");

            var guid = Guid.NewGuid();

            var document = JObject.FromObject(new
            {
                id = guid,
                data = JObject.Parse(message)
            });

            //var response = await container.UpsertItemAsync(document, new PartitionKey("123"));
            //Console.WriteLine($"Status code: {response.StatusCode}");

            await containerInstance.Container.UpsertItemAsync(document, new PartitionKey(guid.ToString()));
        }
    }
}