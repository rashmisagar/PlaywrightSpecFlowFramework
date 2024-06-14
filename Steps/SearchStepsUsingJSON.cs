using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightSpecFlowFramework.Pages;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;
using PlaywrightSpecFlowFramework.Utils;

namespace PlaywrightSpecFlowFramework.Steps;

[Binding]
public class SearchStepsUsingJSON
{
    private readonly IPage _user;
    private readonly IConfiguration _configuration;
    private readonly HomePage _homePage;
    private readonly ResultsPage _resultsPage;
    private readonly SearchData _searchData;

    public SearchStepsUsingJSON(Hooks.Hooks hooks, IConfiguration configuration, HomePage homePage, ResultsPage resultsPage)
    {
        _user = hooks.User;
        _configuration = configuration;
        _homePage = homePage;
        _resultsPage = resultsPage;
        _searchData = hooks.SearchData;
    }

    [Given(@"the user navigates to the DuckDuckGo home page")]
    public async Task GivenTheUserNavigatesToTheDuckDuckGoHomePage()
    {
        var bingUrl = _configuration["BingUrl"];
        if (bingUrl != null) await _homePage.NavigateAsync(bingUrl);
    }

    [When(@"the user searches for a term from the data")]
    public async Task WhenTheUserSearchesForATermFromTheData()
    {
        var searchTerm = _searchData.SearchTerms.First();
        await _homePage.SearchAsync(searchTerm);
    }

    [Then(@"the search results should contain the term from the data")]
    public async Task ThenTheSearchResultsShouldContainTheTermFromTheData()
    {
        var searchTerm = _searchData.SearchTerms.First();
        var result = await _resultsPage.VerifySearchResultsAsync(searchTerm);
        Assert.IsTrue(result, "Search results did not contain the expected text.");
    }
    
    [Then(@"I navigate to the search result at index (.*)")]
    public async Task WhenINavigateToTheSearchResultAtIndex(int index)
    {
        await _resultsPage.NavigateToResultByIndexAsync(index);
    }

    [Then(@"the element with selector ""(.*)"" should be present")]
    public async Task ThenTheElementWithSelectorShouldBePresent(string selector)
    {
        var isPresent = await _resultsPage.IsElementPresentAsync(selector);
        isPresent.Should().BeTrue();
    }
}