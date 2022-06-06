using Microsoft.Identity.Client;
using Shared.Helper;
using Shared.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Services
{
    public class BCApiServices
    {
        private static AuthenticationResult AuthResult = null;

        public BCApiServices()
        {
        }

        public static async Task<string> GetAccessToken()
        {
            string result = string.Empty;
            if ((AuthResult == null) || (AuthResult.ExpiresOn < DateTime.Now))
            {
                AuthResult = await GetAccessToken(Constants.AadTenantId);
            }

            return AuthResult.AccessToken;
        }

        static async Task<AuthenticationResult> GetAccessToken(string aadTenantId)
        {
            Uri uri = new(Constants.Authority.Replace("{AadTenantId}", aadTenantId));
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(Constants.ClientId)
                .WithClientSecret(Constants.ClientSecret)
                .WithAuthority(uri)
                .Build();
            string[] scopes = new string[] { @"https://api.businesscentral.dynamics.com/.default" };
            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            }
            catch (MsalServiceException ex)
            {
            }
            return result;
        }

        public static async Task<Response<object>> GetDataFromBC(string BCUrl)
        {
            string result = string.Empty;
            if ((AuthResult == null) || (AuthResult.ExpiresOn < DateTime.Now))
            {
                AuthResult = await GetAccessToken(Constants.AadTenantId);
            }

            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthResult.AccessToken);

                Uri uri = new(BCUrl);

                string request = string.Empty;
                StringContent content = new(request, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                result = await response.Content.ReadAsStringAsync();

                if ((response.StatusCode == HttpStatusCode.OK) || (response.StatusCode == HttpStatusCode.Created))
                {
                    return new Response<object>
                    {
                        IsSuccess = true,
                        Message = result
                    };
                }
                result = $"Call to Business Central API failed: {response.StatusCode} {response.ReasonPhrase}";
            }

            return new Response<object>
            {
                IsSuccess = false,
                Message = result
            };
        }
    }
}
