using Azure.Core;
using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCostAnalyzer.TokenProvider
{
    public class LocalTokenProvider: ITokenProvider
    {
        private readonly string subscribtionId, tenantId;
        public async Task<AccessToken> GetToken()
        {
            
            string costApiUrl = $"https://management.azure.com/subscriptions/{subscribtionId}/providers/Microsoft.CostManagement/Query?api-version=2019-11-01";

            // Use Azure.Identity to get a token
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions() { TenantId = tenantId});
            var token = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { "https://management.azure.com/.default" }));
            return token;

        }
        public LocalTokenProvider(string subscribtionId, string tenantId) {
            this.tenantId = tenantId;
            this.subscribtionId = subscribtionId;
        }
    }
}
