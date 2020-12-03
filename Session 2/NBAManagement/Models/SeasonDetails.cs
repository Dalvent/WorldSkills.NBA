using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models
{
    public class SeasonDetails
    {
        public Season Season { get; set; }
        public MatchupTypeEnum SeasonType { get; set; }
        public int NumberOfMatchups { get; set; }

        public IEnumerable<Matchup> MatchupOfSeasonType => Season.Matchup
            .Where(m => m.MatchupTypeEnum == SeasonType);
    }
}
