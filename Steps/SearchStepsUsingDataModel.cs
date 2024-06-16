using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightSpecFlowFramework.Pages;
using PlaywrightSpecFlowFramework.Data;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;
using PlaywrightSpecFlowFramework.Utils;

namespace PlaywrightSpecFlowFramework.Steps;

[Binding]
public class SearchStepsUsingDataModel
{
    private readonly IPage _user;
    private readonly IConfiguration _configuration;
    private readonly HomePage _homePage;
    private readonly ResultsPage _resultsPage;
    private readonly DatabaseContext? _dbContext;

    public SearchStepsUsingDataModel(Hooks.Hooks hooks, IConfiguration configuration, HomePage homePage, ResultsPage resultsPage)
    {
        _user = hooks.User;
        _configuration = configuration;
        _homePage = homePage;
        _resultsPage = resultsPage;
        _dbContext = hooks.GetDbContext();
    }

    [When(@"the user searches for a random term from the database")]
    public async Task WhenTheUserSearchesForARandomTermFromTheDatabase()
    {
        if (_dbContext != null)
        {
            var searchTerm = DatabaseUtils.GetFirstSearchTerm(_dbContext);
            await _homePage.SearchTermAndEnter(searchTerm);
        }
    }

    [Then(@"the search results show the random term from the database as the first result")]
    public async Task ThenTheSearchResultsShowTheRandomTermFromTheDatabaseAsTheFirstResult()
    {
        if (_dbContext != null)
        {
            var searchTerm = DatabaseUtils.GetFirstSearchTerm(_dbContext);
            //Assert the page content
            await _resultsPage.AssertPageContent(searchTerm);
            //Assert the first search result (at index of 0)
            await _resultsPage.AssertSearchResultAtIndex(searchTerm, 0);
        }
    }
}