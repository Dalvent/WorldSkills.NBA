using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    class FilteredEnumerable<T> : IFilteredEnumerable<T>
    {
        public readonly FilterMultiplier<T> filterMultiplier = new FilterMultiplier<T>();
        public FilteredEnumerable(IEnumerable<T> collection)
        {
            Original = collection;
        }
        public IEnumerable<T> Original { get; private set; }
        public IEnumerable<T> Filtered => filterMultiplier.Use(Original);
        public void SetFilter(string filterKey, IFilter<T> filter)
        {
            filterMultiplier.SetFilter(filterKey, filter);
        }
        public void RemoveFilter(string filterKey)
        {
            filterMultiplier.RemoveFilter(filterKey);
        }
        public void ClearFilters()
        {
            filterMultiplier.Clear();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return Filtered.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Filtered.GetEnumerator();
        }
    }
}
