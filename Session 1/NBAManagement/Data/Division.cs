namespace NBAManagement.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Division")]
    public partial class Division
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Division()
        {
            Team = new HashSet<Team>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DivisionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int ConferenceId { get; set; }

        public virtual Conference Conference { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Team { get; set; }
    }
}
