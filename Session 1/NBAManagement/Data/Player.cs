namespace NBAManagement.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            PositionName = new HashSet<PositionName>();
        }

        public int PlayerId { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        [Required]
        [StringLength(10)]
        public string ShirtNumber { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryCode { get; set; }

        public int TeamId { get; set; }

        public virtual Country Country { get; set; }

        public virtual Team Team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PositionName> PositionName { get; set; }
    }
}
