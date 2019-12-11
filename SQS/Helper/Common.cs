using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.KeyVault;
using Newtonsoft.Json;
using SQS.ServiceBusQueue.Messages;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SQS.Helper
{
    public static class Common
    {
        public static string EncryptSecret { get; set; }
        static string appId = "Application ID";
        static string secretKey = "Secert key";
        static string tenantId = "TenantId";
        static string secretUri = "Secert Uri";

        /// <summary>
        /// GetAccessToken 
        /// </summary>
        /// <param name="azureTenantId"></param>
        /// <param name="azureAppId"></param>
        /// <param name="azureSecretKey"></param>
        /// <returns></returns>
        public static async Task<string> GetAccessToken(string azureTenantId, string azureAppId, string azureSecretKey)
        {

            var context = new AuthenticationContext("https://login.windows.net/" + tenantId);
            ClientCredential clientCredential = new ClientCredential(appId, secretKey);
            var tokenResponse = await context.AcquireTokenAsync("https://vault.azure.net", clientCredential);
            var accessToken = tokenResponse.AccessToken;
            return accessToken;
        }

        /// <summary>
        /// Get Queue Connection String from Key vault secret
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetSecret()
        {
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessToken));
            var secret=kv.GetSecretAsync(secretUri).Result;
            return secret.Value;
        }


    }
}
