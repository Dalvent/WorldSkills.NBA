using NBAManagement.Command;
using NBAManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.ViewModels
{
    class VisitorMainViewModel
    {
        public ICommand NavigateMainTeamsViewCommand { get; }
        public ICommand NavigateMainPlayersViewCommand { get; }
        public ICommand NavigateMatchupListViewCommand { get; }
        public ICommand NavigatePhotosViewCommand { get; }
        public VisitorMainViewModel()
        {
            NavigateMainTeamsViewCommand = new ActionCommand(() => NavigationManager.OpenPage(new TeamsMainView()));
            NavigateMainPlayersViewCommand = new ActionCommand(() => NavigationManager.OpenPage(new PlayersMainView()));
            NavigateMatchupListViewCommand = new ActionCommand(() => NavigationManager.OpenPage(new MutchupListPage()));
            NavigatePhotosViewCommand = new ActionCommand(() => NavigationManager.OpenPage(new PhotosPage()));
        }
    }
}
