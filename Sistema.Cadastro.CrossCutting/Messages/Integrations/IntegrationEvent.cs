using Sistema.Cadastro.CrossCutting.Common.CQRS;

namespace Sistema.Cadastro.CrossCutting.Messages.Integrations
{
    public abstract class IntegrationEvent : Event
    {
        protected IntegrationEvent(Command command, string topic) : base(topic)
        {
            AggregateId = command.AggregateId;
        }
    }
}
