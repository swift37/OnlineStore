namespace OnlineStore.MVC.Services.Base
{
    public class ValidationFailure
    {
        public ValidationFailure() { }

        public ValidationFailure(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
