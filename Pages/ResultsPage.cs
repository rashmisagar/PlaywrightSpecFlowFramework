using Microsoft.Playwright;

namespace PlaywrightSpecFlowFramework.Pages;

public class ResultsPage
{
    
    private readonly IPage _user;

    public ResultsPage(Hooks.Hooks hooks)
    {
        _user = hooks.User;
    }
    
    private ILocator SearchContent => _user.Locator("body");
    private ILocator ResultLink => _user.Locator("a.result__a");

    public async Task<bool> VerifySearchResultsAsync(string query)
    {
        var content = await SearchContent.TextContentAsync();
        return content != null && content.Contains(query);
    }

    public async Task NavigateToResultByIndexAsync(int index)
    {
        var resultLink = ResultLink.Nth(index);
        await resultLink.ClickAsync();
    }

    public async Task<bool> IsElementPresentAsync(string selector)
    {
        var element = _user.Locator(selector);
        return await element.CountAsync() > 0;
    }
}