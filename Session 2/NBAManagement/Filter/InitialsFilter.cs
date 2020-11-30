using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Filter
{
    class InitialsFilter : IFilter<PlayerInTeam>
    {
        public char Letter { get; set; }
        public InitialsFilter(char letter)
        {
            this.Letter = letter;
        }

        public IEnumerable<PlayerInTeam> Use(IEnumerable<PlayerInTeam> collection)
        {
            return collection.Where(player => player.Player.FirstName[0] == Letter 
                || player.Player.LastName.FirstOrDefault() == Letter);
        }
    }
}
