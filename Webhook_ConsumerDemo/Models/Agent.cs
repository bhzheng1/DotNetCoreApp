namespace Webhook_ConsumerDemo.Models;
public class Agent
{
    public int Id { get; set; }
    public List<CallLog> Activities { get; set; } = new List<CallLog>();
}
