using NavigationMVVM.Services;
using NavigationMVVM.ViewModels;

namespace NavigationMVVM.Commands;

public class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly NavigationService<TViewModel> _navigateService;

    public NavigateCommand(NavigationService<TViewModel> navigateService)
    {
        _navigateService = navigateService;
    }

    public override void Execute(object? parameter)
    {
        _navigateService.Navigate();
    }
}
