using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace _25_days_of_serverless_2019.Day9
{
    public static class HolidayGreeting
    {
        [FunctionName("HolidayGreeting")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Processing GitHub IssueEvent...");

            var parser = new WebhookBodyParser(log);
            var body = await parser.Parse(req.Body);

            if (body != null)
            {
                if (body.Action == IssueEventAction.opened)
                {
                    log.LogInformation("Replying to new opened issue {issueNumber}...", body.IssueNumber);
                    string comment = $"Thank you @{body.IssueCreator} for creating this issue! Have a Happy Holiday season!";

                    string githubToken = Environment.GetEnvironmentVariable("GitHubToken");
                    string productName = Environment.GetEnvironmentVariable("ProductName");

                    var github = new GitHubService(githubToken, productName);
                    await github.PostComment(body.RepoOwner, body.RepoName, body.IssueNumber, comment);
                }

                log.LogInformation("Done processing GitHub IssueEvent (action: {action})", body.Action);
                return new OkResult();
            }
            else
            {
                log.LogError("Error processing GitHub IssueEvent. Unable to parse request body.");
                return new BadRequestObjectResult("Unexpected request body");
            }
        }
    }
}
