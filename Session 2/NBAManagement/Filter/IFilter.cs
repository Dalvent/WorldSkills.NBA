using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    public interface IFilter<T>
    {
        IEnumerable<T> Use(IEnumerable<T> collection);
    }
}
