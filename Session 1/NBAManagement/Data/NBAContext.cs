namespace NBAManagement.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NBAContext : DbContext
    {
        public static NBAContext Instance { get; }
            = new NBAContext();
        public NBAContext()
            : base("name=NBAContext")
        {
        }

        public virtual DbSet<Conference> Conference { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PositionName> PositionName { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conference>()
                .HasMany(e => e.Division)
                .WithRequired(e => e.Conference)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.CountyCode)
                .IsFixedLength();

            modelBuilder.Entity<Country>()
                .Property(e => e.CountryName)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Player)
                .WithRequired(e => e.Country)
                .HasForeignKey(e => e.CountryCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Division>()
                .HasMany(e => e.Team)
                .WithRequired(e => e.Division)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Height)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Player>()
                .Property(e => e.Weight)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Player>()
                .Property(e => e.ShirtNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.CountryCode)
                .IsFixedLength();

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PositionName)
                .WithMany(e => e.Player)
                .Map(m => m.ToTable("PositionOfPlayer").MapLeftKey("PlayerId").MapRightKey("PositionId"));

            modelBuilder.Entity<PositionName>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Abbr)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Coach)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Stadium)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Player)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
