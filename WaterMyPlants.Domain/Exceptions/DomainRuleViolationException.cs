namespace WaterMyPlants.Domain.Exceptions;

public sealed class DomainRuleViolationException : DomainException
{
    public DomainRuleViolationException(string message) : base(message)
    {
    }
}
