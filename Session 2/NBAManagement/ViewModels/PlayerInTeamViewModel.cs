using NBAManagement.Models;
using System;
using System.Linq;

namespace NBAManagement.ViewModels
{
    public class PlayerInTeamViewModel
    {
        public PlayerInTeam Model { get; }
        public PlayerInTeamViewModel(PlayerInTeam model)
        {
            this.Model = model;
        }
        public byte[] Photo => Model.Player.Img;
        public string ShirtNumber => Model.ShirtNumber;
        public string FullName => $"{Model.Player.FirstName} {Model.Player.LastName}";
        public PositionName PositionName => Model.Player.PositionName.FirstOrDefault();
        public DateTime? DateOfBirth => Model.Player.DateOfBirth;
        public string TeamName => Model.Team.TeamName;
        public string County => Model.Player.Country.CountryName;
        public decimal Weight => Model.Player.Weight;
        public decimal Height => Model.Player.Height;
        public string College => Model.Player.College;
        public decimal Salary => Model.Salary;
        public int YearExperience
        {
            get
            {
                return (new DateTime(1, 1, 1) + (DateTime.Now - Model.Player.JoinYear)).Year - 1;
            }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
