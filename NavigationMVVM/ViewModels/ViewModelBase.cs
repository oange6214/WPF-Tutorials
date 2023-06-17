using NavigationMVVM.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NavigationMVVM.ViewModels;

public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    // Property Change Notification
    protected virtual void OnPropertyChanged([CallerMemberName]string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // For property validation
    protected Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    public string? Error => null;

    public string this[string columnName] => _errors.ContainsKey(columnName) ? string.Join(Environment.NewLine, _errors[columnName]) : null;

    protected void ValidateProperty<T>(Expression<Func<T>> propertySelector, params IValidationRule<T>[] validationRules)
    {
        var propertyName = GetPropertyName(propertySelector);
        if (!_errors.ContainsKey(propertyName))
        {
            _errors[propertyName] = new List<string>();
        }
        else
        {
            _errors[propertyName].Clear();
        }

        var propertyValue = propertySelector.Compile()();
        foreach(var rule in validationRules)
        {
            if (!rule.Validate(propertyValue))
            {
                _errors[propertyName].Add(rule.ErrorMessage);
            }
        }

        OnPropertyChanged("Item[]");
        OnPropertyChanged(nameof(Error));
    }

    private string GetPropertyName<T>(Expression<Func<T>> propertySelector)
    {
        if (propertySelector.Body is MemberExpression memberExpression)
            return memberExpression.Member.Name;

        throw new ArgumentException("Property selector must be a member expression", nameof(propertySelector));
    }

    public virtual void Dispose() { }
}
