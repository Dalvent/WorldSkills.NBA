using NBAManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace NBAManagement.Filter
{
    class TeamDivisionFilter : IFilter<Team>
    {
        public TeamDivisionFilter(Division division)
        {
            Division = division;
        }

        public Division Division { get; set; }
        public IEnumerable<Team> Use(IEnumerable<Team> collection)
        {
            return collection.Where(t => t.Division == Division);
        }
    }
}
