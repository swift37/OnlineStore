namespace OnlineStore.MVC.Services.ApiClient
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient => _httpClient;

        public Action<HttpClient, HttpRequestMessage, string> RequestPreparation { get; set; }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url) =>
            RequestPreparation(client, request, url); 
    }
}
