using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCostAnalyzer.TokenProvider
{
    public interface ITokenProvider
    {
        public Task<AccessToken> GetToken();
    }
}
