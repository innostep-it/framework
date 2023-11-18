namespace InnostepIT.Framework.Core.Contract.Web
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public long TotalItems { get; set; }
    }
}
