namespace Talabat.Api.Helper
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> data,int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }
        public int PageSize { set; get; }
        public int PageIndex { set; get; }
        public int Count { set; get; }
        public IReadOnlyList<T> Data { set; get; }
    }
}
