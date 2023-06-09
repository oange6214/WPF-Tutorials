namespace NavigationMVVM.ValidationRules;

public class NotEmptyValidationRule : IValidationRule<string>
{
    public bool Validate(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public string ErrorMessage => "Value cannot be empty.";
}
