using NavigationMVVM.Services;

namespace NavigationMVVM.Commands;

public class NavigateCommand : CommandBase
{
    private readonly INavigationService _navigateService;

    public NavigateCommand(INavigationService navigateService)
    {
        _navigateService = navigateService;
    }

    public override void Execute(object? parameter)
    {
        _navigateService.Navigate();
    }
}
