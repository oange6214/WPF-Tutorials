using NavigationMVVM.Commands;
using NavigationMVVM.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class PeopleListingViewModel : ViewModelBase
{
    public ObservableCollection<PersonViewModel> _people { get; }

    public IEnumerable<PersonViewModel> People => _people;

    public ICommand AddPersonCommand { get; } 

    public PeopleListingViewModel(INavigationService addPersonNavigationService)
    {
        AddPersonCommand = new NavigateCommand(addPersonNavigationService);

        _people = new ObservableCollection<PersonViewModel>();

        _people.Add(new PersonViewModel("Jed"));
        _people.Add(new PersonViewModel("Jim"));
        _people.Add(new PersonViewModel("Bill"));
    }
}
