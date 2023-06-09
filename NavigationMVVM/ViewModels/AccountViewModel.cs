using NavigationMVVM.Commands;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public string Name => "BISP";
        public ICommand NavigationHomeCommand { get; }

        public AccountViewModel(NavigationStore navigationStore)
        {
            NavigationHomeCommand = new NavigateHomeCommand(navigationStore);
        }
    }
}
