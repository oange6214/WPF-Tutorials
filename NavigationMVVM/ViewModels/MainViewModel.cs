using NavigationMVVM.Stores;
using NavigationMVVM.ValidationRules;

namespace NavigationMVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }


        private string _myProperty;

        public string MyProperty
        {
            get => _myProperty;
            set
            {
                if (_myProperty != value)
                {
                    _myProperty = value;
                    this.ValidateProperty(() =>
                    MyProperty, new NotEmptyValidationRule());
                    OnPropertyChanged();
                }
            }
        }
    }
}
