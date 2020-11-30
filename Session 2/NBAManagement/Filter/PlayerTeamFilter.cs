using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    class PlayerTeamFilter : IFilter<PlayerInTeam>
    {
        public Team ArgumentTeam { get; set; }
        public PlayerTeamFilter(Team argumentTeam)
        {
            this.ArgumentTeam = argumentTeam;
        }

        public IEnumerable<PlayerInTeam> Use(IEnumerable<PlayerInTeam> collection)
        {
            return collection.Where(player => player.Team == ArgumentTeam);
        }
    }
}
