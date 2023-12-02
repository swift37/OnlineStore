namespace OnlineStore.MVC.Services.ApiClient
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient => _httpClient;
    }
}
