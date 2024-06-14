using Microsoft.Playwright;

namespace PlaywrightSpecFlowFramework.Pages
{
    public class HomePage : BasePage
    {
        private ILocator SearchInput => GetLocator("input[id='searchbox_input']");
        private ILocator SearchButton => GetLocator("button[type='submit']");
        private ILocator SearchInputBox => GetLocator("input[name='q']");

        public HomePage(Hooks.Hooks hooks) : base(hooks) { }

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
}