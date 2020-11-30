using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    interface IFilteredEnumerable<T> : IEnumerable<T>
    {
        IEnumerable<T> Original { get; }
        IEnumerable<T> Filtered { get; }
    }
}
