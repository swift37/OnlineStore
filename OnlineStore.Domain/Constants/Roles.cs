namespace OnlineStore.Domain.Constants
{
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string Manager = $"{Administrator},Manager";
        public const string Employee = $"{Manager},Employee";
        public const string User = $"{Employee},User";
    }
}
