namespace NBAManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Player")]
    public partial class Player
    {
        public Player()
        {
            MatchupLog = new HashSet<MatchupLog>();
            PlayerInTeam = new HashSet<PlayerInTeam>();
            PlayerStatistics = new HashSet<PlayerStatistics>();
            PositionName = new HashSet<PositionName>();
        }
        [Key]
        [Column("PlayerId")]
        public int PlayerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Column(TypeName = "date")]
        public DateTime JoinYear { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string College { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryCode { get; set; }
        public byte[] Img { get; set; }

        public bool IsRetirment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RetirementTime { get; set; }
        public int YearExperience
        { 
            get
            {
                return (new DateTime(1, 1, 1) + (DateTime.Now - JoinYear)).Year - 1;
            }
        }
        public float CurrentSeasonPPG
        {
            get
            {
                float result = 0.0f;
                var statistics = CurrentSeasonStatistic;
                foreach(var statistic in statistics)
                {
                    result += statistic.Point;
                }
                return result / statistics.Count;
            }
        }
        public float CurrentSeasonAPG
        {
            get
            {
                float result = 0.0f;
                var statistics = CurrentSeasonStatistic;
                foreach(var statistic in statistics)
                {
                    result += statistic.Assist;
                }
                return result / statistics.Count;
            }
        }
        public float CurrentSeasonRPG
        {
            get
            {
                float result = 0.0f;
                var statistics = CurrentSeasonStatistic;
                foreach(var statistic in statistics)
                {
                    result += statistic.Rebound;
                }
                return result / statistics.Count;
            }
        }
        public float CareerPPG
        {
            get
            {
                float result = 0.0f;
                var statistics = PlayerStatistics;
                foreach(var statistic in statistics)
                {
                    result += statistic.Point;
                }
                return result / statistics.Count;
            }
        }
        public float CareerAPG
        {
            get
            {
                float result = 0.0f;
                var statistics = PlayerStatistics;
                foreach(var statistic in statistics)
                {
                    result += statistic.Assist;
                }
                return result / statistics.Count;
            }
        }
        public float CareerRPG
        {
            get
            {
                float result = 0.0f;
                var statistics = PlayerStatistics;
                foreach(var statistic in statistics)
                {
                    result += statistic.Rebound;
                }
                return result / statistics.Count;
            }
        }

        public PositionName MainPositionName => PositionName.First();
        public PlayerInTeam CurrentTeam => PlayerInTeam.Where(playerInTeam => playerInTeam.Season == Season.LastSeason).First();
        public PlayerInTeam GetTeamBySeason(Season season)
        {
            return PlayerInTeam.Where(pt => pt.Season == season).FirstOrDefault();
        }
        private IList<PlayerStatistics> CurrentSeasonStatistic => PlayerStatistics.ToArray().Where(statistic => statistic.Matchup.Season == Season.LastSeason).ToList();

        public virtual Country Country { get; set; }
        public virtual ICollection<MatchupLog> MatchupLog { get; set; }
        public virtual ICollection<PlayerInTeam> PlayerInTeam { get; set; }

        [ForeignKey("PlayerId")]
        public virtual ICollection<PlayerStatistics> PlayerStatistics { get; set; }
        public virtual ICollection<PositionName> PositionName { get; set; }
    }
}
