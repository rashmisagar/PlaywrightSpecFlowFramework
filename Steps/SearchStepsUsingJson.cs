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
public class SearchStepsUsingJson
{
    private readonly IPage _user;
    private readonly IConfiguration _configuration;
    private readonly HomePage _homePage;
    private readonly ResultsPage _resultsPage;
    private readonly SearchData _searchData;

    public SearchStepsUsingJson(Hooks.Hooks hooks, IConfiguration configuration, HomePage homePage, ResultsPage resultsPage)
    {
        _user = hooks.User;
        _configuration = configuration;
        _homePage = homePage;
        _resultsPage = resultsPage;
        _searchData = JsonReader.LoadJsonData<SearchData>("TestData/searchTerms.json");
    }

    [Given(@"the user navigates to the DuckDuckGo home page")]
    public async Task GivenTheUserNavigatesToTheDuckDuckGoHomePage()
    {
        var ddgUrl = _configuration["DDGUrl"];
        if (ddgUrl != null) await _homePage.NavigateAsync(ddgUrl);
    }

    [When(@"the user searches for a term from the data")]
    public async Task WhenTheUserSearchesForATermFromTheData()
    {
        var searchTerm = _searchData.SearchTerms.First();
        await _homePage.SearchTermAndEnter(searchTerm);
    }

    [Then(@"the search results show the term as the first result")]
    public async Task ThenTheSearchResultsShowTheTermAsTheFirstResult()
    {
        var searchTerm = _searchData.SearchTerms.First();
        //Assert the page content
        await _resultsPage.AssertPageContent(searchTerm);
        //Assert the first search result (at index of 0)
        await _resultsPage.AssertSearchResultAtIndex(searchTerm, 0);
    }
}