namespace OnboardingSIGDB1.Domain.Notifications;

public abstract class Notifiable
{
   //lista de erro para todos os erros do obj / possibilita add,delete, etc
   private readonly List<Notification> _notifications = new();
   //lista de erro para todos os erros do obj / Não possibilita
   public IReadOnlyCollection<Notification> Notifications => _notifications;
   
   // proporciona a possibilidade de verificarno Service se a entidade está valida antes de salvar
   public bool IsValid => !_notifications.Any();
   
   public void AddNotification(string campo, string descricao)
   {
      _notifications.Add(new Notification(campo, descricao));
   }
   
   //adiciona varias notificações
   public void AddNotifications(IEnumerable<Notification> notifications)
   {
      _notifications.AddRange(notifications);
   }
}