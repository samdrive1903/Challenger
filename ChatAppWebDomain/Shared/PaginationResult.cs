namespace ChatAppWebDomain.Shared
{
    public class PaginationResult<T> : IDisposable
    {
        public int TotalRowCount { get; set; }
        public int PageCount { get; set; }
        public int TotalPages { get; set; }
        public ICollection<T> Rows { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
