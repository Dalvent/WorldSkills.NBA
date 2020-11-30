using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models
{
    public interface ISeasonParticipant
    {
        Season Season { get; }
    }
}
