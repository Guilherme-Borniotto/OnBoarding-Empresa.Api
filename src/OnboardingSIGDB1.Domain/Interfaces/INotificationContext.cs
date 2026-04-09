using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface INotificationContext
{
    bool HasNotifications { get; }
    IReadOnlyCollection<Notification> Notifications { get; }
    void AddNotification(string key, string message);
    void AddNotifications(IEnumerable<Notification> notifications);
    void DomainAddNotification(IEnumerable<ValidationFailure> validationFailures);
}