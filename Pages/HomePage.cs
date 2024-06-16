using Microsoft.Playwright;

namespace PlaywrightSpecFlowFramework.Pages
{
    public class HomePage : BasePage
    {
        private ILocator SearchInput => GetLocator("input[id='searchbox_input']");
        private ILocator SearchButton => GetLocator("button[type='submit']");

        public HomePage(Hooks.Hooks hooks) : base(hooks) { }

        public async Task SearchTermAndEnter(string searchTerm)
        {
            await SearchInput.FillAsync(searchTerm);
            await SearchButton.ClickAsync();
        }
    }
}