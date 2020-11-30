using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    class PlayerStatisticDateIntervalFilter : IFilter<PlayerStatistics>
    {
        public PlayerStatisticDateIntervalFilter(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IEnumerable<PlayerStatistics> Use(IEnumerable<PlayerStatistics> collection)
        {
            return collection.Where(s => s.Matchup.Starttime >= Start && s.Matchup.Starttime <= End);
        }
    }
}
