using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Shared.Services
{

    public class RequestBridge : BaseRequest
    {
        public RequestBridge(string urlPrefix)
            : base(urlPrefix)
        {

        }

        public async Task<Response<object>> GetResponseAsync()
        {
            string url = $"{_UrlPrefix}api/Bridge_Products?code=App Key Azure Functions";
            return await GetAsync<Response<object>>(url);
        }
    }

    public class BaseRequest
    {
        protected string _UrlPrefix = String.Empty;

        public string UrlPrefix
        {
            get
            {
                return _UrlPrefix;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) ||
                    !Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    return;
                }

                _UrlPrefix = value;
            }
        }

        public BaseRequest(string urlPrefix)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            if (!urlPrefix.EndsWith("/"))
                urlPrefix = string.Concat(urlPrefix, "/");

            _UrlPrefix = urlPrefix;
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        protected async Task<T> GetAsync<T>(string url)
            where T : new()
        {
            HttpClient httpClient = CreateHttpClient();
            T result;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer");
                var response = await httpClient.GetStringAsync(url);
          
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(response));
            }
            catch (Exception ex)
            {
                result = new T();
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

    }

}
