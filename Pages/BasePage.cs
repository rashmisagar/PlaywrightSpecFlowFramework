using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightSpecFlowFramework.Pages
{
    public abstract class BasePage
    {
        protected readonly IPage User;

        protected BasePage(Hooks.Hooks hooks)
        {
            User = hooks.User;
        }
        
        protected ILocator GetLocator(string selector)
        {
            return User.Locator(selector);
        }

        public async Task NavigateAsync(string url)
        {
            await User.GotoAsync(url);
        }
    }
}