using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace _25_days_of_serverless_2019.Day1
{
    public static class DreidelSpinner
    {
        private static readonly char[] dreidelResults = new char[] { 'נ', 'ג', 'ה', 'ש' };

        [FunctionName("DreidelSpinner")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Spinning dreidel...");

            Random rand = new Random();
            int randomSite = rand.Next(dreidelResults.Length);
            char dreidelResult = dreidelResults[randomSite];

            log.LogInformation("Dreidel landed on '{dreidel}'.", dreidelResult);

            return new OkObjectResult(dreidelResult);
        }
    }
}
