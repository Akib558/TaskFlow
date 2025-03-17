namespace TaskFlow.Data;

public static class QueryCollection
{
    private static readonly string AuthQueryDirectory = "Queries/AuthQueries";

    public static readonly string RegisterUserSql = LoadQuery("registeruser.sql");
    public static readonly string LoginUserSql = LoadQuery("loginuser.sql");
    public static readonly string RefreshTokenValidate = LoadQuery("validaterefereshtoken.sql");
    public static readonly string ValidateAndAddRefreshToken = LoadQuery("validateandaddrefreshtoken.sql");

    private static string LoadQuery(string fileName)
    {
        string filePath = Path.Combine(AuthQueryDirectory, fileName);
        return File.ReadAllText(filePath);
    }
}