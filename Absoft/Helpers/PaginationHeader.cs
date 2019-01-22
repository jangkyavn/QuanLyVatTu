namespace Absoft.Helpers
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int FirstRowOnPage { get; set; }
        public int LastRowOnPage { get; set; }

        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages, int firstRowOnPage, int lastRowOnPage)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
            FirstRowOnPage = firstRowOnPage;
            LastRowOnPage = lastRowOnPage;
        }
    }
}
