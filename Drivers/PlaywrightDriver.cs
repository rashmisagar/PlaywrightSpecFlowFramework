using System.Threading.Tasks;
using Microsoft.Playwright;

public class PlaywrightDriver
{
    public async Task<IBrowser> InitialiseBrowser(IPlaywright playwright)
    {
        return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
    }

    public async Task<IPage> InitialisePage(IBrowserContext browserContext)
    {
        return await browserContext.NewPageAsync();
    }
}