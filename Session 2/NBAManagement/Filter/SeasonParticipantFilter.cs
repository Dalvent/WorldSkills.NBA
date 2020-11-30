using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    class SeasonParticipantFilter<T> : IFilter<T> where T : ISeasonParticipant
    {
        private Season ArgumentSeason { get; set; }
        public SeasonParticipantFilter(Season argumentSeason)
        {
            this.ArgumentSeason = argumentSeason;
        }

        public IEnumerable<T> Use(IEnumerable<T> collection)
        {
            return collection.Where(participant => participant.Season == ArgumentSeason);
        }
    }
}
