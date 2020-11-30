using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    public class FilterMultiplier<T> : IFilter<T>
    {
        private readonly IDictionary<string, IFilter<T>> filters = 
            new Dictionary<string, IFilter<T>>();
        public FilterMultiplier()
        {
        }
        public void SetFilter(string filterKey, IFilter<T> filter)
        {
            if(filters.ContainsKey(filterKey))
            {
                filters[filterKey] = filter;
            }
            else
            {
                filters.Add(filterKey, filter);
            }
        }
        public void RemoveFilter(string filterKey)
        {
            filters.Remove(filterKey);
        }

        public void Clear()
        {
            filters.Clear();
        }
        public IEnumerable<T> Use(IEnumerable<T> collection)
        {
            var resultCollection = collection;
            foreach(var filter in filters)
            {
                resultCollection = filter.Value.Use(resultCollection);
            };
            return resultCollection;
        }
    }
}
