namespace CQRSPattern.Common.Common;

public static class Constants
{
    public static string NotFound = "The requested data was not found";
    public static string Success = "The operation was successful";
    public static string Error = "An error occurred";
    public static string Invalid = "The input or data is invalid";
    public static string AlreadyExist = "Record already exists";
    public static string Delete = "Record deleted successfully";
    public const string ValidationMessage = "Validation failure";

    public static class CacheKeys
    {
        public const string Employee = "Employee_";
    }
}