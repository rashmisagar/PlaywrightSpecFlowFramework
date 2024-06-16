using Bogus;

namespace PlaywrightSpecFlowFramework.Data;

public class SearchTermSeeder
{
    public static void Seed(DatabaseContext context)
    {
        if (!context.SearchTerm.Any())
        {
            var faker = new Faker();
            var searchTerms = new List<SearchTerm>();

            /* for (int i = 0; i < 10; i++)
            {
                var term = faker.Lorem.Word();
                searchTerms.Add(new SearchTerm(term));
            } */
            var technicalWords = new[] { "algorithm", "playwright",  "specflow", "array"};
            var randomTerm = faker.PickRandom(technicalWords);
            searchTerms.Add(new SearchTerm(randomTerm));

            context.SearchTerm.AddRange(searchTerms);
            context.SaveChanges();
        }
    }
}