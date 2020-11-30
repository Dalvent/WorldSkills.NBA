using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    class NullObjectFilter<T> : IFilter<T>
    {
        public NullObjectFilter()
        {
        }
        public IEnumerable<T> Use(IEnumerable<T> collection)
        {
            return collection;
        }
    }
}
