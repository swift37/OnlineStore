namespace OnlineStore.MVC.Services.ApiClient
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }

        public Action<HttpClient, HttpRequestMessage, string> RequestPreparation { get; set; }
    }
}
