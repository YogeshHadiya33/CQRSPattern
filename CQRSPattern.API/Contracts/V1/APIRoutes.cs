namespace CQRSPattern.API.Contracts.V1;

internal static class APIRoutes
{
    private const string Base = "api/v1";

    internal static class Employee
    {
        private const string EmployeeBase = $"{Base}/employee";
        public const string GetAll = $"{EmployeeBase}";
        public const string Get = $"{EmployeeBase}/{{id}}";
        public const string Create = $"{EmployeeBase}/create";
        public const string Update = $"{EmployeeBase}/update";
        public const string Delete = $"{EmployeeBase}/delete/{{id}}";
    }

    internal static class User
    {
        public const string UserBase = $"{Base}/user";
        public const string Register = $"{UserBase}/register";
        public const string Login = $"{UserBase}/login";
        public const string GetAll = $"{UserBase}";
        public const string GetById = $"{UserBase}/{{id}}";
    }
}