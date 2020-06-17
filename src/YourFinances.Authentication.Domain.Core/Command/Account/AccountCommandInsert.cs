
namespace YourFinances.Authentication.Domain.Core.Command.Account
{
    public class AccountCommandInsert : ModularSystem.Messaging.RabbitMQ.Core.Command.Command
    {
        public int AccountId { get; set; }
    }
}
