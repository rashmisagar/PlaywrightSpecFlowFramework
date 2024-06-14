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
        private readonly PlaywrightDriver _playwrightDriver = new PlaywrightDriver();

        [BeforeScenario]
        public async Task BeforeScenario(ObjectContainer testThreadContainer)
        {
            _configuration = LoadConfiguration();
            testThreadContainer.RegisterInstanceAs<IConfiguration>(_configuration);

            var playwright = await Playwright.CreateAsync();
            var browser = await _playwrightDriver.InitialiseBrowser(playwright);

            var browserContext = await browser.NewContextAsync();
            User = await _playwrightDriver.InitialisePage(browserContext);
        }

        private IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}