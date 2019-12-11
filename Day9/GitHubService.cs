using System.Threading.Tasks;
using Octokit;

namespace _25_days_of_serverless_2019.Day9
{
    public class GitHubService
    {
        private readonly GitHubClient _github;

        public GitHubService(string githubToken, string productName)
        {
            _github = new GitHubClient(new ProductHeaderValue(productName));
            _github.Credentials = new Credentials(githubToken);
        }

        public async Task PostComment(string repoOwner, string repoName, int issueNumber, string comment)
        {
            await _github.Issue.Comment.Create(repoOwner, repoName, issueNumber, comment);
        }
    }
}