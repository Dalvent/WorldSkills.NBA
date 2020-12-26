namespace NBAManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NBAContext : DbContext
    {
        public static NBAContext Instance { get; } = new NBAContext();
        public NBAContext()
            : base("name=NBAContext")
        {
        }

        public virtual DbSet<ActionType> ActionType { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Conference> Conference { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Matchup> Matchup { get; set; }
        public virtual DbSet<MatchupDetail> MatchupDetail { get; set; }
        public virtual DbSet<MatchupLog> MatchupLog { get; set; }
        public virtual DbSet<MatchupType> MatchupType { get; set; }
        public virtual DbSet<Pictures> Pictures { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerInTeam> PlayerInTeam { get; set; }
        public virtual DbSet<PlayerStatistics> PlayerStatistics { get; set; }
        public virtual DbSet<PositionName> PositionName { get; set; }
        public virtual DbSet<PostSeason> PostSeason { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Season> Season { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ActionType>()
                .HasMany(e => e.MatchupLog)
                .WithRequired(e => e.ActionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Jobnumber)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Passwords)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.RoleId)
                .IsFixedLength()
                .IsUnicode(false);

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

            modelBuilder.Entity<Matchup>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Matchup>()
                .Property(e => e.CurrentQuarter)
                .IsUnicode(false);

            modelBuilder.Entity<Matchup>()
                .HasMany(e => e.MatchupLog)
                .WithRequired(e => e.Matchup)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Matchup>()
                .HasMany(e => e.PlayerStatistics)
                .WithRequired(e => e.Matchup)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MatchupLog>()
                .Property(e => e.OccurTime)
                .IsUnicode(false);

            modelBuilder.Entity<MatchupLog>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<MatchupType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MatchupType>()
                .HasMany(e => e.Matchup)
                .WithRequired(e => e.MatchupType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pictures>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Height)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Player>()
                .Property(e => e.Weight)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Player>()
                .Property(e => e.College)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.CountryCode)
                .IsFixedLength();

            modelBuilder.Entity<Player>()
                .HasMany(e => e.MatchupLog)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerInTeam)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerStatistics)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PositionName)
                .WithMany(e => e.Player)
                .Map(m => m.ToTable("PositionOfPlayer").MapLeftKey("PlayerId").MapRightKey("PositionId"));

            modelBuilder.Entity<PlayerInTeam>()
                .Property(e => e.ShirtNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PlayerInTeam>()
                .Property(e => e.Salary)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PlayerStatistics>()
                .Property(e => e.Min)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PositionName>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PositionName>()
                .Property(e => e.Abbr)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Admin)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.Matchup)
                .WithRequired(e => e.Season)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.PlayerInTeam)
                .WithRequired(e => e.Season)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Season>()
                .HasMany(e => e.PostSeason)
                .WithRequired(e => e.Season)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.TeamName)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Coach)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Abbr)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Stadium)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.MatchupsAway)
                .WithRequired(e => e.TeamAway)
                .HasForeignKey(e => e.Team_Away)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.MatchupHome)
                .WithRequired(e => e.TeamHome)
                .HasForeignKey(e => e.Team_Home)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.MatchupLog)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.PlayerInTeam)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.PlayerStatistics)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.PostSeason)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
