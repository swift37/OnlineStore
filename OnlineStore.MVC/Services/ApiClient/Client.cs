namespace OnlineStore.MVC.Services.ApiClient
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient => _httpClient;

        public Action RequestPreparation { get; set; }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url) =>
            RequestPreparation(); 
    }
}
