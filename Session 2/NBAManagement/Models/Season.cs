namespace NBAManagement.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Season")]
    public partial class Season
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Season()
        {
            Matchup = new HashSet<Matchup>();
            PlayerInTeam = new HashSet<PlayerInTeam>();
            PostSeason = new HashSet<PostSeason>();
        }

        public static Season LastSeason => NBAContext.Instance.Season.ToList().Last();

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeasonId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matchup> Matchup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerInTeam> PlayerInTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostSeason> PostSeason { get; set; }

        public IEnumerable<SeasonDetails> SeasonDetails
        {
            get 
            {
                var seasonDetails = new List<SeasonDetails>(3);

                foreach(MatchupTypeEnum matchupType in Enum.GetValues(typeof(MatchupTypeEnum)))
                {
                    var matchupOfCurrentType = Matchup
                        .Where(m => m.MatchupTypeEnum == matchupType);
                    seasonDetails.Add(new Models.SeasonDetails() { 
                        Season = this,
                        SeasonType = matchupType,
                        NumberOfMatchups = matchupOfCurrentType.Count()
                    });
                }
                return seasonDetails;
            } 
        }       

        public override string ToString()
        {
            return Name;
        }
    }
}
