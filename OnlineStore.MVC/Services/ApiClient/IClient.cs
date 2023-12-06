namespace OnlineStore.MVC.Services.ApiClient
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }

        public Action RequestPreparation { get; set; }
    }
}
