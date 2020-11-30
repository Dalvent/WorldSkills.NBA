namespace NBAManagement.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Matchup")]
    public partial class Matchup : ISeasonParticipant, IComparable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Matchup()
        {
            MatchupDetail = new HashSet<MatchupDetail>();
            MatchupLog = new HashSet<MatchupLog>();
            PlayerStatistics = new HashSet<PlayerStatistics>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatchupId { get; set; }

        public int SeasonId { get; set; }

        public int MatchupTypeId { get; set; }

        public int Team_Away { get; set; }

        public int Team_Home { get; set; }

        public DateTime Starttime { get; set; }

        public int Team_Away_Score { get; set; }

        public int Team_Home_Score { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        public int Status { get; set; }

        public string ResultString => $"{Team_Away_Score}-{Team_Home_Score}";

        [StringLength(50)]
        public string CurrentQuarter { get; set; }

        public virtual MatchupType MatchupType { get; set; }

        public virtual Season Season { get; set; }

        public virtual Team TeamAway { get; set; }

        public virtual Team TeamHome { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchupDetail> MatchupDetail { get; set; }
        public IEnumerable<MatchupDetail> FullDetails 
        { 
            get
            {
                var matchupDetails = new List<MatchupDetail>();
                matchupDetails.Add(new MatchupDetail() { 
                    Matchup = this, 
                    Quarter = 0, 
                    Team_Away_Score = this.Team_Away_Score,
                    Team_Home_Score = this.Team_Home_Score
                });
                matchupDetails.AddRange(MatchupDetail.ToArray());
                return matchupDetails;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchupLog> MatchupLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerStatistics> PlayerStatistics { get; set; }

        public int CompareTo(object obj)
        {
            return Starttime.CompareTo(((Matchup)obj).Starttime);
        }

        public IEnumerable<PlayerStatistics> AwayPlayerStatistics
        {
            get
            {
                return PlayerStatistics.Where(s => s.Team == TeamAway);
            }
        }

        public IEnumerable<PlayerStatistics> TeamAwayStarters => AwayPlayerStatistics.Where(s => s.IsStarting == 1);
        public IEnumerable<PlayerStatistics> TeamHomeStarters => HomePlayerStatistics.Where(s => s.IsStarting == 1);

        public IEnumerable<PlayerStatistics> HomePlayerStatistics
        {
            get
            {
                return PlayerStatistics.Where(s => s.Team == TeamHome);
            }
        }
        public string TeamAwayFGMadeAttempted =>
            $"{AwayPlayerStatistics.Sum(s => s.FieldGoalMade)}-{AwayPlayerStatistics.Sum(s => s.FieldGoalMissed)}";
        public string TeamHomeFGMadeAttempted =>
            $"{HomePlayerStatistics.Sum(s => s.FieldGoalMade)}-{AwayPlayerStatistics.Sum(s => s.FieldGoalMissed)}";
        public string TeamAwayC3PointFieldGoal=>
            $"{AwayPlayerStatistics.Sum(s => s.C3_PointFieldGoalMade)}-{AwayPlayerStatistics.Sum(s => s.C3_PointFieldGoalMissed)}";
        public string TeamHomeC3PointFieldGoal =>
            $"{HomePlayerStatistics.Sum(s => s.C3_PointFieldGoalMade)}-{AwayPlayerStatistics.Sum(s => s.C3_PointFieldGoalMissed)}";
        public string TeamAwayFreeThrow =>
            $"{AwayPlayerStatistics.Sum(s => s.FreeThrowMade)}-{AwayPlayerStatistics.Sum(s => s.FreeThrowMissed)}";
        public string TeamHomeFreeThrow =>
            $"{HomePlayerStatistics.Sum(s => s.FreeThrowMade)}-{AwayPlayerStatistics.Sum(s => s.FreeThrowMissed)}";

        public int TeamAwayFGMadeAttemptedMade => AwayPlayerStatistics.Sum(s => s.FieldGoalMade);
        public int TeamHomeFGMadeAttemptedMade => HomePlayerStatistics.Sum(s => s.FieldGoalMade);
        public int TeamAwayC3PointFieldGoalMade => AwayPlayerStatistics.Sum(s => s.C3_PointFieldGoalMade);
        public int TeamHomeC3PointFieldGoalMade => HomePlayerStatistics.Sum(s => s.C3_PointFieldGoalMade);
        public int TeamAwayFreeThrowMade => AwayPlayerStatistics.Sum(s => s.FreeThrowMade);
        public int TeamHomeFreeThrowMade => HomePlayerStatistics.Sum(s => s.FreeThrowMade);
        public int TeamAwayFGMadeAttemptedMissed => AwayPlayerStatistics.Sum(s => s.FieldGoalMissed);
        public int TeamHomeFGMadeAttemptedMissed => HomePlayerStatistics.Sum(s => s.FieldGoalMissed);
        public int TeamAwayC3PointFieldGoalMissed => AwayPlayerStatistics.Sum(s => s.C3_PointFieldGoalMissed);
        public int TeamHomeC3PointFieldGoalMissed => HomePlayerStatistics.Sum(s => s.C3_PointFieldGoalMissed);
        public int TeamAwayFreeThrowMissed => AwayPlayerStatistics.Sum(s => s.FreeThrowMissed);
        public int TeamHomeFreeThrowMissed => HomePlayerStatistics.Sum(s => s.FreeThrowMissed);

        public int TeamAwayRebounds => AwayPlayerStatistics.Sum(s => s.Rebound);
        public int TeamHomeRebounds => HomePlayerStatistics.Sum(s => s.Rebound);
        public int TeamAwayAssists => AwayPlayerStatistics.Sum(s => s.Assist);
        public int TeamHomeAssists => HomePlayerStatistics.Sum(s => s.Assist);
        public int TeamAwaySteals => AwayPlayerStatistics.Sum(s => s.Steal);
        public int TeamHomeSteals => HomePlayerStatistics.Sum(s => s.Rebound);
        public int TeamAwayBlocks => AwayPlayerStatistics.Sum(s => s.Block);
        public int TeamHomeBlocks => HomePlayerStatistics.Sum(s => s.Block);
        public int TeamAwayTurnovers => AwayPlayerStatistics.Sum(s => s.Turnover);
        public int TeamHomeTurnovers => HomePlayerStatistics.Sum(s => s.Turnover);

        public double TeamAwayFGPersent => ((double)TeamAwayFGMadeAttemptedMade) / 
            (TeamAwayFGMadeAttemptedMade + TeamAwayFGMadeAttemptedMissed) * 100.0;
        public double TeamHomeFGPersent => ((double)TeamHomeFGMadeAttemptedMade) / 
            (TeamHomeFGMadeAttemptedMade + TeamHomeFGMadeAttemptedMissed) * 100.0;
        public double TeamAway3PPersent => ((double)TeamAwayC3PointFieldGoalMade) /
            (TeamAwayC3PointFieldGoalMade + TeamAwayC3PointFieldGoalMissed) * 100.0;
        public double TeamHome3PPersent => ((double)TeamHomeC3PointFieldGoalMade) /
            (TeamHomeC3PointFieldGoalMade + TeamHomeC3PointFieldGoalMissed) * 100.0;

    }
}
