using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models
{
    public struct ChartPoint<TX, TY>
    {
        public TX X { get; set; }
        public TY Y { get; set; }
    }
}
