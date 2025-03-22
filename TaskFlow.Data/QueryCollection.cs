namespace TaskFlow.Data;

public static class QueryCollection
{
    public static string LoadQuery(string dirName, string fileName)
    {
        string filePath = Path.Combine("Queries", dirName, fileName);
        return File.ReadAllText(filePath);
    }
}