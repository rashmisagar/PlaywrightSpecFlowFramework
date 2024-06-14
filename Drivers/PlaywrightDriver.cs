using Microsoft.Playwright;

namespace PlaywrightSpecFlowFramework.Drivers;

public class PlaywrightDriver
{
    private static IPlaywright? _playwright;
    private static IBrowser? _browser;
    private IPage? _page;

    public static async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
    }

    public async Task<IPage?> NewPageAsync()
    {
        if (_browser != null) _page = await _browser.NewPageAsync();
        return _page;
    }

    public async Task ClosePageAsync()
    {
        if (_page != null) await _page.CloseAsync();
    }

    public static async Task DisposeAsync()
    {
        if (_browser != null) await _browser.CloseAsync();
        _playwright?.Dispose();
    }
}