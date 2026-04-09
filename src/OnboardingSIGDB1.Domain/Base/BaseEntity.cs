using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Domain.Base;

using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema; // Necessário para o [NotMapped]
using OnboardingSIGDB1.Domain.Notifications;


public abstract class BaseEntity<T> :  AbstractValidator<T> where T : class
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.Now;

    
    
    private readonly List<Notification> _notifications = new();

    [NotMapped]
    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
    
    [NotMapped] 
    public bool IsValid => !_notifications.Any();

    public abstract bool Validate();
    
    protected void AddNotification(string key, string message)
    {
        if (!_notifications.Any(n => n.field == key && n.Description == message))
            _notifications.Add(new Notification(key, message));
    }

    public void ClearNotifications() => _notifications.Clear();
}