using System;

namespace NBAManagement.Models
{
    public class PlayerStatisticCharPoint
    {
        public PlayerStatisticCharPoint(int x, DateTime y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public DateTime Y { get; set; }
    }
}
