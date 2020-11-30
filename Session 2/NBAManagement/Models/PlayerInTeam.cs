namespace NBAManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlayerInTeam")]
    public partial class PlayerInTeam : ISeasonParticipant
    {
        public int PlayerInTeamId { get; set; }

        public int PlayerId { get; set; }

        public int TeamId { get; set; }

        public int SeasonId { get; set; }

        [Required]
        [StringLength(10)]
        public string ShirtNumber { get; set; }

        public decimal Salary { get; set; }

        public int StarterIndex { get; set; }

        public virtual Player Player { get; set; }

        public virtual Season Season { get; set; }

        public virtual Team Team { get; set; }

        public string FullNameAndNum => $@"{Player.FullName} (#{ShirtNumber})";
    }
}
