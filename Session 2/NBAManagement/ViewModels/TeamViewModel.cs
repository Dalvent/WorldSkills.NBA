using NBAManagement.Models;

namespace NBAManagement.ViewModels
{
    public class TeamViewModel
    {
        public Team Model { get; }

        public TeamViewModel(Team model)
        {
            this.Model = model;
        }

        public override string ToString()
        {
            return Model.TeamName;
        }
    }
}
