using System.Diagnostics.CodeAnalysis;

namespace StarFood.Application.Base
{
    [SuppressMessage("Readability", "RCS1194")]
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
