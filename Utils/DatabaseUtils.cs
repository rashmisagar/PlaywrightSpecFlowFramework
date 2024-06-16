using PlaywrightSpecFlowFramework.Data;

namespace PlaywrightSpecFlowFramework.Utils;

public static class DatabaseUtils
{
    public static string GetFirstSearchTerm(DatabaseContext dbContext)
    {
        var searchTerm = dbContext?.SearchTerm.FirstOrDefault();
        if (searchTerm == null)
        {
            throw new InvalidOperationException("No search terms found in the database.");
        }

        return searchTerm.term ?? throw new InvalidOperationException("The term property is null.");
    }
}