using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightSpecFlowFramework.Pages
{
    public class ResultsPage : BasePage
    {
        private ILocator SearchInput => GetLocator("input[id='search_form_input']");
        private ILocator ResultLink => GetLocator("a.result__a");

        public ResultsPage(Hooks.Hooks hooks) : base(hooks) { }
   
        public async Task AssertPageContent(string searchTerm)
        {
            await Assertions.Expect(SearchInput).ToHaveValueAsync(searchTerm);
        }

        public async Task AssertSearchResultAtIndex(string searchTerm, int resultIndex)
        {
            await Assertions.Expect(ResultLink.Nth(resultIndex)).ToContainTextAsync(searchTerm);
        }
    }
}