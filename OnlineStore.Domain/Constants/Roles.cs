namespace OnlineStore.Domain.Constants
{
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string ManagerOrHigher = $"{Administrator},Manager";
        public const string Manager = $"Manager";
        public const string EmployeeOrHigher = $"{ManagerOrHigher},Employee";
        public const string Employee = $"Employee";
        public const string UserOrHigher = $"{EmployeeOrHigher},User";
        public const string User = $"User";
    }
}
