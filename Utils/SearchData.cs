namespace PlaywrightSpecFlowFramework.Utils
{
    public class SearchData
    {
        public SearchData(List<string>? searchTerms)
        {
            SearchTerms = searchTerms;
        }

        public List<string>? SearchTerms { get; set; }
    }
}