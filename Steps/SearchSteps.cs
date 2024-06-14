using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightSpecFlowFramework.Pages;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;
using PlaywrightSpecFlowFramework.Utils;

namespace PlaywrightSpecFlowFramework.Steps;

[Binding]
public class SearchSteps
{
    private readonly IPage _user;
    private readonly IConfiguration _configuration;
    private readonly HomePage _homePage;
    private readonly ResultsPage _resultsPage;

    public SearchSteps(Hooks.Hooks hooks, IConfiguration configuration, HomePage homePage, ResultsPage resultsPage)
    {
        _user = hooks.User;
        _configuration = configuration;
        _homePage = homePage;
        _resultsPage = resultsPage;
    }
    
    [Given(@"the user is on the DuckDuckGo home page")]
    public async Task GivenTheUserIsOnTheDuckDuckGoHomePage()
    {
        var ddgUrl = _configuration["DDGUrl"];
        if (ddgUrl != null) await _homePage.NavigateAsync(ddgUrl);
    }

    [When(@"the user searches for '([^']*)'")]
    public async Task WhenTheUserSearchesFor(string searchTerm)
    {
        await _homePage.SearchTermAndEnter(searchTerm);
    }

    [Then(@"the search results page shows '([^']*)' in the search results")]
    public async Task ThenTheSearchResultsPageShowsInTheSearchResults(string searchTerm)
    { 
        var result = await _resultsPage.VerifySearchResultsAsync(searchTerm);
        Assert.IsTrue(result, "Search results did not contain the expected text.");
    }
}