namespace NBAManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatchupLog")]
    public partial class MatchupLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int MatchupId { get; set; }

        public int Quarter { get; set; }

        [Required]
        [StringLength(10)]
        public string OccurTime { get; set; }

        public int TeamId { get; set; }

        public int PlayerId { get; set; }

        public int ActionTypeId { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

        public virtual ActionType ActionType { get; set; }

        public virtual Matchup Matchup { get; set; }

        public virtual Player Player { get; set; }
        public virtual PlayerInTeam ActualPlayerInTeam => Player.GetTeamBySeason(Matchup.Season); 

        public virtual Team Team { get; set; }
    }
}
