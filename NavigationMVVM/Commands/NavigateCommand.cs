using NavigationMVVM.Services;
using NavigationMVVM.ViewModels;

namespace NavigationMVVM.Commands;

public class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly INavigationService<TViewModel> _navigateService;

    public NavigateCommand(INavigationService<TViewModel> navigateService)
    {
        _navigateService = navigateService;
    }

    public override void Execute(object? parameter)
    {
        _navigateService.Navigate();
    }
}
