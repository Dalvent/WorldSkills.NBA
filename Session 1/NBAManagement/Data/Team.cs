namespace NBAManagement.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Team")]
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Player = new HashSet<Player>();
        }

        public int TeamId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int DivisionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Abbr { get; set; }

        [Required]
        [StringLength(80)]
        public string Coach { get; set; }

        [Required]
        [StringLength(80)]
        public string Stadium { get; set; }

        [Required]
        public byte[] Logo { get; set; }

        public virtual Division Division { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Player { get; set; }
    }
}
