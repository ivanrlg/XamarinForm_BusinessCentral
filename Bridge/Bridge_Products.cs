using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Services;
using Shared.Helper;
using Shared.Models;

namespace Bridge
{
    public static class Bridge_Products
    {
        [FunctionName("Bridge_Products")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Bridge_Products");

            Response<object> response = new Response<object>();

            try
            {
                response = await BCApiServices.GetDataFromBC(Constants.APi_Products);

                return new BadRequestObjectResult(response);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                
                return new OkObjectResult(response);
            }
            
                      
        }

  
    }
}
