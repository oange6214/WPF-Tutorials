namespace NavigationMVVM.ValidationRules;

public interface IValidationRule<T>
{
    bool Validate(T value);

    string ErrorMessage { get; }
}
