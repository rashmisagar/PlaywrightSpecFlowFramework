using System.ComponentModel.DataAnnotations.Schema;

namespace PlaywrightSpecFlowFramework.Data
{
    [Table("searchterm")]
    public class SearchTerm(string term)
    {
        public int id { get; set; }
        public string term { get; set; } = term;
    }
}