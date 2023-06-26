using System;

namespace NavigationMVVM.Stores;

public class PeopleStore
{
    public event Action<string> PersonAdded = null!;

    public void AddPerson(string name)
    {
        PersonAdded?.Invoke(name);
    }
}
