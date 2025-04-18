﻿namespace LeHoangNhatTanRazorPages.Shared.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData()
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        }

        public PagedList(IEnumerable<T> list, MetaData metaData)
        {
            MetaData = metaData;
            AddRange(list);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, MetaData metaData)
        {
            return new PagedList<T>(source, metaData);
        }
    }
}
