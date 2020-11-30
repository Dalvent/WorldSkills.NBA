namespace NBAManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatchupDetail")]
    public partial class MatchupDetail
    {
        public int Id { get; set; }

        public int? MatchupId { get; set; }

        public int? Quarter { get; set; }

        public int? Team_Away_Score { get; set; }

        public int? Team_Home_Score { get; set; }

        public virtual Matchup Matchup { get; set; }

        public string QuaterString
        {
            get
            {
                switch(Quarter)
                {
                case 0: return "T";
                case 1: return "1st";
                case 2: return "2nd";
                case 3: return "3rd";
                case 4: return "4th";
                default: return $"OT{Quarter}";
                }
            }
        }

        public override string ToString()
        {
            return QuaterString;
        }
    }
}
