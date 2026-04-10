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
    
    public abstract bool Validate();
}