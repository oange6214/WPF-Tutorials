using NavigationMVVM.Commands;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class PeopleListingViewModel : ViewModelBase
{
    private PeopleStore _peopleStore;

    public ObservableCollection<PersonViewModel> _people { get; }

    public IEnumerable<PersonViewModel> People => _people;

    public ICommand AddPersonCommand { get; } 

    public PeopleListingViewModel(PeopleStore peopleStore, INavigationService addPersonNavigationService)
    {
        _peopleStore = peopleStore;

        AddPersonCommand = new NavigateCommand(addPersonNavigationService);

        _people = new ObservableCollection<PersonViewModel>
        {
            new PersonViewModel("Jed"),
            new PersonViewModel("Jim"),
            new PersonViewModel("Bill")
        };

        _peopleStore.PersonAdded += OnPersonAdded;
    }

    private void OnPersonAdded(string name)
    {
        _people.Add(new PersonViewModel(name));
    }

    public override void Dispose()
    {
        _peopleStore.PersonAdded -= OnPersonAdded;
        base.Dispose();
    }
}
