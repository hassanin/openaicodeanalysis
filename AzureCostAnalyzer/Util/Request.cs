using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using System;
using System.Net.Http;



namespace AzureCostAnalyzer.Util
{
    internal class Request
    {
        public static async Task EndToEnd()
        {
            string subscriptionId = "072e0879-9b66-40bb-87ec-5eef5e810c29";
            string costApiUrl = $"https://management.azure.com/subscriptions/{subscriptionId}/providers/Microsoft.CostManagement/Query?api-version=2019-11-01";

            // Use Azure.Identity to get a token
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions() {TenantId= "88f738c6-baab-45a8-b695-ee3cadd61660" });
            var token = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { "https://management.azure.com/.default" }));

            // Prepare the query for the Cost Management API
            var query = new
            {
                type = "ActualCost",
                timeframe = "Custom",
                timePeriod = new
                {
                    from = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    to = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss")
                },
                dataSet = new
                {
                    granularity = "Daily",
                    grouping = new[] { new { type = "Dimension", name = "ResourceGroupName" } }
                }
            };

            string queryJson = System.Text.Json.JsonSerializer.Serialize(query);

            // Use HttpClient to make a POST request to the Cost Management API
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                var content = new StringContent(queryJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(costApiUrl, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Here jsonResponse will have the cost breakdown information
                Console.WriteLine(jsonResponse);
            }
        }
    }
}
