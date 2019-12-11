using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace _25_days_of_serverless_2019.Day9
{
    public class WebhookBodyParser
    {
        private readonly ILogger log;

        public WebhookBodyParser(ILogger log)
        {
            this.log = log;
        }

        public async Task<WebhookBody> Parse(Stream reqBody)
        {
            string requestBody = await new StreamReader(reqBody).ReadToEndAsync();
            log.LogInformation("Parsing request body: '{body}'", requestBody);

            dynamic data = JsonConvert.DeserializeObject(requestBody);

            Enum.TryParse<IssueEventAction>((string)data.action, out IssueEventAction action);

            var body = new WebhookBody
            {
                Action = action,
                RepoName = data.repository.name,
                RepoOwner = data.repository.owner.login,
                IssueCreator = data.issue.user.login,
                IssueNumber = data.issue.number
            };
            
            log.LogInformation("Request body parsed.");

            return body;
        }
    }
}