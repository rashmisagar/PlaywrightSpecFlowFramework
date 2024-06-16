using System.Threading.Tasks;
using BoDi;
using Microsoft.Extensions.Configuration;
using PlaywrightSpecFlowFramework.Data;
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
        private static DatabaseContext? _dbContext;
        public IPage User { get; private set; } = null!;
        private readonly PlaywrightDriver _playwrightDriver = new();

        [BeforeScenario]
        public async Task BeforeScenario(ObjectContainer testThreadContainer)
        {
            _configuration = LoadConfiguration();
            testThreadContainer.RegisterInstanceAs<IConfiguration>(_configuration);
            
            // Set up the database context
            _dbContext = new DatabaseContext();
            SearchTermSeeder.Seed(_dbContext);
            

            var playwright = await Playwright.CreateAsync();
            var browser = await _playwrightDriver.InitialiseBrowser(playwright);

            var browserContext = await browser.NewContextAsync();
            User = await _playwrightDriver.InitialisePage(browserContext);
        }
        
        /*[AfterScenario]
        public async Task AfterScenario()
        {
            if (_dbContext != null)
            {
                _dbContext.SearchTerm.RemoveRange(_dbContext.SearchTerm);
                await _dbContext.SaveChangesAsync();
            }
        }*/

        private IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("TestData/appsettings.json")
                .Build();
        }
        public DatabaseContext? GetDbContext() => _dbContext;
    }
}