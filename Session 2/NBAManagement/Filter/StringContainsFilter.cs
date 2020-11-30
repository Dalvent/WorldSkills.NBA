using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    class StringContainsFilter<T> : IFilter<T>
    {
        public string Argument { get; }
        public Func<T, string> GetStringDelegate { get; }
        public StringContainsFilter(string argument, Func<T, string> getString)
        {
            this.Argument = argument;
            this.GetStringDelegate = getString;
        }

        public IEnumerable<T> Use(IEnumerable<T> collection)
        {
            return collection.Where(item => GetStringDelegate(item)
                .Contains(Argument));
        }
    }
}
