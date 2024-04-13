namespace Gallery.Shared
{
    public interface ISearchRequest { }

    public class PaginationSearchRequest<T> where T : ISearchRequest
    {
        private int _pageSize;
        public int pageNumber { get; set; } = 1;
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        public T RequestFilter { get; set; }
    }
}
