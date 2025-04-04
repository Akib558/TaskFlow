namespace TaskFlow.Data;

public static class QueryCollection
{
    public static string LoadQuery(string dirName, string fileName)
    {
        string basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
        string filePath = Path.Combine(basePath, "TaskFlow.Data", "Queries", dirName, fileName + ".sql");
        // string filePath = Path.Combine("Queries", dirName, fileName) + ".sql";
        var res = File.ReadAllText(filePath);
        return res;
    }
}