using NBAManagement.Models;
using System.Linq;

namespace NBAManagement.ViewModels
{
    public class SeasonViewModel
    {
        public Season Model { get; }

        public SeasonViewModel(Season model)
        {
            this.Model = model;
        }

        public string Name => Model.Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
