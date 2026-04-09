using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Domain.Notifications;

    public class NotificationContext : INotificationContext
    {
    
        private readonly List<Notification> _notifications;

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }


        public void DomainAddNotification(IEnumerable<ValidationFailure> validationFailures)
        {
            if (validationFailures is null) return;
            
                foreach (var error in validationFailures)
                {
                    AddNotification(error.PropertyName, error.ErrorMessage);
                }
        }
    }
