using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    public class FilteredEnumerable<T> : IFilteredEnumerable<T>
    {
        private readonly FilterMultiplier<T> filterMultiplier = new FilterMultiplier<T>();
        public FilteredEnumerable(IEnumerable<T> collection)
        {
            Original = collection;
            Filtered = Original;
        }
        public IEnumerable<T> Original { get; set; }
        public IEnumerable<T> Filtered { get; private set; }
        public int OriginalCount => Original.Count();
        public int FilteredCount => Filtered.Count();
        public void SetFilter(string filterKey, IFilter<T> filter)
        {
            filterMultiplier.SetFilter(filterKey, filter);
            UpdateFiltered();
        }
        public void RemoveFilter(string filterKey)
        {
            filterMultiplier.RemoveFilter(filterKey);
            UpdateFiltered();
        }
        public void ClearFilters()
        {
            filterMultiplier.Clear();
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
            Filtered = filterMultiplier.Use(Original);
        }
    }
}
