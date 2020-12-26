using NBAManagement.Filter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NBAManagement
{
    public class PagedEnumerable<T> : IFilteredEnumerable<T>
    {
        private const int START_PAGE_NUM = 1;
        private readonly PageFilter<T> pageFilter;
        public PagedEnumerable(IEnumerable<T> collection, int pageSize)
        {
            this.Original = collection;
            pageFilter = new PageFilter<T>(pageSize, START_PAGE_NUM);
            UpdateFiltered();
        }
        public int CurrentPageItemCount => Original.Count();
        public int ItemCount => Filtered.Count();
        public IEnumerable<T> Original { get; set; }
        public IEnumerable<T> Filtered { get; private set; }
        public int PageCount => pageFilter.GetPageCount(Original);
        public int PageSize
        {
            get => pageFilter.PageSize;
            set 
            {
                pageFilter.PageSize = value;
                UpdateFiltered();
            }
        }
        public int CurrentPageNum
        {
            get => pageFilter.CurrentPageNum;
            set
            {
                if(value > PageCount)
                {
                    pageFilter.CurrentPageNum = PageCount;
                }
                else if(value <= 0)
                {
                    pageFilter.CurrentPageNum = 1;
                }
                else
                {
                    pageFilter.CurrentPageNum = value;
                }
                UpdateFiltered();
            }
        }
        public void NextPage()
        {
            CurrentPageNum++;
            UpdateFiltered();
        }
        public void PriviusPage()
        {
            CurrentPageNum--;
            UpdateFiltered();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return Filtered.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Filtered.GetEnumerator();
        }
        private void UpdateFiltered()
        {
            Filtered = pageFilter.Use(Original);
        }
    }
}
