using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightSpecFlowFramework.Pages;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;

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

    [Then(@"the search results show '(.*)' as the first result")]
    public async Task ThenTheSearchResultsShowAsTheFirstResult(string searchTerm)
    {
        //Assert the page content
        await _resultsPage.AssertPageContent(searchTerm);
        
        //Assert the first search result (at index of 0)
        await _resultsPage.AssertSearchResultAtIndex(searchTerm, 0);
    }
}