using System.Threading.Tasks;
using BoDi;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightSpecFlowFramework.Utils;
using TechTalk.SpecFlow;
[assembly:Parallelizable(ParallelScope.Fixtures)]

namespace PlaywrightSpecFlowFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private static IConfiguration? _configuration;
        public IPage User { get; private set; } = null!; //-> We'll call this property in the tests
        public SearchData? SearchData { get; private set; }
        
        [BeforeScenario] // -> Notice how we're doing these steps before each scenario
        public async Task BeforeScenario(ObjectContainer testThreadContainer)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            testThreadContainer.RegisterInstanceAs<IConfiguration>(_configuration);
            
            //Initialise Playwright
            var playwright = await Playwright.CreateAsync();
            //Initialise a browser - 'Chromium' can be changed to 'Firefox' or 'Webkit'
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // -> Use this option to be able to see your test running
            });
            
            // Load search data
            SearchData = JsonReader.ReadJsonFile<SearchData>("searchTerms.json");
            
            //Setup a browser context
            var browserContext = await browser.NewContextAsync();

            //Initialise a page on the browser context.
            User = await browserContext.NewPageAsync();
        }
    }
}