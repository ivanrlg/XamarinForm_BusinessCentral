namespace Shared.Helper
{
    public static class Constants
    {
        public const string ClientId = "Your Client ID";
        public const string ClientSecret = "Your Client Secret";
        public const string AadTenantId = "Your Tenant Id";
        public const string Authority = "https://login.microsoftonline.com/{AadTenantId}/oauth2/v2.0/token";
        
        static string UrlBCSandbox = "https://api.businesscentral.dynamics.com/v2.0/{AadTenantId}/YourSandbox/ODataV4/";
        static string CompanyID = "Your Company ID";
        public static string APi_Products = GetBCAPIUrl(UrlBCSandbox, "GetData_GetItemsToJson", CompanyID);

        public static string URLAzure = "https://YourAzureFunctions.azurewebsites.net/";

        private static string GetBCAPIUrl(string BCBaseUrl, string Action, string bcCompanyId)
        {
            return BCBaseUrl + $"{Action}?company=({bcCompanyId})";
        }
    }
}
