using System.Dynamic;

namespace OnboardingSIGDB1.Domain.Notifications;

public class Notification
{
    
    public string Campo  { get; set; }
    public string Descricao { get; set; }

    public Notification(){}
    public Notification(string campo, string descricao)
    {
       Campo = campo;
       Descricao = descricao;
    }
}