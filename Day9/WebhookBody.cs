namespace _25_days_of_serverless_2019.Day9
{
    public class WebhookBody
    {
        public string RepoName { get; set; }
        public string RepoOwner { get; set; }
        public string IssueCreator { get; set; }
        public int IssueNumber { get; set; }
        public IssueEventAction Action { get; set; }
    }
}