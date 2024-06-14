using System.Threading.Tasks;
using BoDi;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;
[assembly:Parallelizable(ParallelScope.Fixtures)]

namespace PlaywrightSpecFlowFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private static IConfiguration? _configuration;
        public IPage User { get; private set; } = null!;

        [BeforeScenario]
        public async Task BeforeScenario(ObjectContainer testThreadContainer)
        {
            _configuration = LoadConfiguration();
            testThreadContainer.RegisterInstanceAs<IConfiguration>(_configuration);

            var playwright = await Playwright.CreateAsync();
            var browser = await InitialiseBrowser(playwright);

            var browserContext = await browser.NewContextAsync();
            User = await InitialisePage(browserContext);
        }

        private IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private async Task<IBrowser> InitialiseBrowser(IPlaywright playwright)
        {
            return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
        }

        private async Task<IPage> InitialisePage(IBrowserContext browserContext)
        {
            return await browserContext.NewPageAsync();
        }
    }
}