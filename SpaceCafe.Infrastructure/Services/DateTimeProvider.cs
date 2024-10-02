using SpaceCafe.Application.Common.Interfaces.Services;

namespace SpaceCafe.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
