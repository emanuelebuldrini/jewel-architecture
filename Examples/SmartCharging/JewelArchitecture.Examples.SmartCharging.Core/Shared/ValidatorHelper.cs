namespace JewelArchitecture.Examples.SmartCharging.Core.Shared;

internal class ValidatorHelper
{
    internal static void ValidateGuid(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("The Guid must be set to a non-empty value.", nameof(id));
        }
    }

    internal static void ValidateName(string name)
    {
        if (name.Length > 100)
        {
            throw new ArgumentException("The name must be set to a value of maximum 100 characters.", nameof(name));
        }
    }
}
