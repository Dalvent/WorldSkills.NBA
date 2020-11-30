using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    public class PageFilter<T> : IFilter<T>
    {
        public int CurrentPageNum { get; set; }
        public int PageSize { get; set; }
        public PageFilter(int pageSize, int currentPageNum)
        {
            this.PageSize = pageSize;
            this.CurrentPageNum = currentPageNum;
        }
        public int GetPageCount(IEnumerable<T> collection)
        {
            var itemsCount = collection.Count();
            var incompletePageCount = (itemsCount % PageSize) > 0 ? 1 : 0;
            return itemsCount / PageSize + incompletePageCount;
        }
        private bool IsNumInPage(int pageNum)
        {
            int firstElementOnCurrentPageNum = PageSize * (CurrentPageNum - 1) + 1;
            return pageNum >= firstElementOnCurrentPageNum &&
                pageNum <= (firstElementOnCurrentPageNum + PageSize - 1);
        }
        public IEnumerable<T> Use(IEnumerable<T> collection)
        {
            bool elementsFind = false;
            int currentPageNum = 1;
            var resultCollection = new List<T>();
            foreach(var item in collection)
            {
                if(IsNumInPage(currentPageNum))
                {
                    resultCollection.Add(item);
                    elementsFind = true;
                }
                else if(elementsFind)
                {
                    break;
                }
                currentPageNum++;
            }
            return resultCollection;
        }
    }
}

/*
 * 3 * 1 + 1 = 1;
 */