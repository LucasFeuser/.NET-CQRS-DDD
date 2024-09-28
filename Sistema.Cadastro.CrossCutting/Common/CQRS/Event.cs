using MediatR;
using System.Text.Json.Serialization;
using Sistema.Cadastro.CrossCutting.Messages;

namespace Sistema.Cadastro.CrossCutting.Common.CQRS
{
    public abstract class Event : Message, INotification
    {
        protected Event(string topic)
        {
            Topic = topic;
        }

        [JsonIgnore]
        public string? JobId { get; private set; }
        [JsonIgnore]
        public string Topic { get; }

        public void SetJobId(string jobId)
            => JobId = jobId;

        public virtual void Validate()
        {
            throw new NotImplementedException();
        }

    }
}
