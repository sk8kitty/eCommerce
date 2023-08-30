namespace eCommerce.Models
{
    public class ItemCatalogViewModel
    {
        public ItemCatalogViewModel(List<Item> items, int lastPage, int currPage)
        {
            Items = items;
            LastPage = lastPage;
            CurrentPage = currPage;
        }


        public List<Item> Items { get; set; }

        /// <summary>
        /// Last page of the catalog. Quotient of total num of items / num items displayed per page.
        /// </summary>
        public int LastPage { get; set; }

        /// <summary>
        /// Page the user is currently viewing.
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
