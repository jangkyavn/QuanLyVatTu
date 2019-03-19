namespace Absoft.Helpers
{
    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string SortValue { get; set; }
        public string SortKey { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string KeywordCol { get; set; }
        public string ColName { get; set; }
    }
}
