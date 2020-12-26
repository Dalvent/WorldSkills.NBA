namespace NBAManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Team")]
    public partial class Team
    {
        public Team()
        {
            MatchupsAway = new HashSet<Matchup>();
            MatchupHome = new HashSet<Matchup>();
            MatchupLog = new HashSet<MatchupLog>();
            PlayerInTeam = new HashSet<PlayerInTeam>();
            PlayerStatistics = new HashSet<PlayerStatistics>();
            PostSeason = new HashSet<PostSeason>();
        }

        public int TeamId { get; set; }

        [Required]
        [StringLength(50)]
        public string TeamName { get; set; }

        public int DivisionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Coach { get; set; }

        [Required]
        [StringLength(3)]
        public string Abbr { get; set; }

        [StringLength(100)]
        public string Stadium { get; set; }

        [Required]
        public byte[] Logo { get; set; }

        public decimal GetSeassonTotlaSalary(Season season)
        {
            decimal totalSalary = 0;
            foreach(var playerInTeam in PlayerInTeam.Where(pit => pit.Season == season))
            {
                totalSalary += playerInTeam.Salary;
            }
            return totalSalary;
        }
        public decimal CurrentSeasonTotalSlary
        {
            get
            {
                return GetSeassonTotlaSalary(NBAContext.Instance.Season.ToList().Last());
            }
        }

        public virtual Division Division { get; set; }

        public virtual ICollection<Matchup> MatchupsAway { get; set; }
        public virtual ICollection<Matchup> MatchupHome { get; set; }
        public virtual ICollection<MatchupLog> MatchupLog { get; set; }
        public virtual ICollection<PlayerInTeam> PlayerInTeam { get; set; }
        public virtual ICollection<PlayerStatistics> PlayerStatistics { get; set; }
        public virtual ICollection<PostSeason> PostSeason { get; set; }

        public override string ToString()
        {
            return TeamName;
        }
    }
}
