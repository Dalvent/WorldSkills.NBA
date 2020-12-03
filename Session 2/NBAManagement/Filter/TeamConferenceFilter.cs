using NBAManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace NBAManagement.Filter
{
    class TeamConferenceFilter : IFilter<Team>
    {
        public TeamConferenceFilter(Conference conference)
        {
            Conference = conference;
        }

        public Conference Conference { get; set; }
        public IEnumerable<Team> Use(IEnumerable<Team> collection)
        {
            return collection.Where(t => t.Division.Conference == Conference);
        }
    }
}
