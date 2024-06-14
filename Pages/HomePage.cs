using Microsoft.Playwright;

namespace PlaywrightSpecFlowFramework.Pages;

public class HomePage
{
    private readonly IPage _user;

    public HomePage(Hooks.Hooks hooks)
    {
        _user = hooks.User;
    }
    
    private ILocator SearchInput => _user.Locator("input[id='searchbox_input']");
    private ILocator SearchButton => _user.Locator("button[type='submit']");

    private ILocator SearchInputBox => _user.Locator("input[name='q']");
    
    public async Task NavigateAsync(string url)
    {
        await _user.GotoAsync(url);
    }
    
    
    public async Task SearchTermAndEnter(string searchTerm)
    {
        await SearchInput.FillAsync(searchTerm);
        await SearchButton.ClickAsync();
    }
    
    public async Task SearchAsync(string searchTerm)
    {
        await SearchInputBox.FillAsync(searchTerm);
        await SearchInputBox.PressAsync("Enter");
    }
}