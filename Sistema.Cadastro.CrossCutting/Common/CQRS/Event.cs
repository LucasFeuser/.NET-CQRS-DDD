using MediatR;
using System.Text.Json.Serialization;
using Sistema.Cadastro.CrossCutting.Messages;

namespace Sistema.Cadastro.CrossCutting.Common.CQRS
{
    public abstract class Event : Message, INotification
    {
        protected Event()
        {   }

        protected Event(string topic)
        {
            Topic = topic;
        }

        [JsonIgnore]
        public string JobId { get; private set; }

        [JsonIgnore]
        public int? RetryCount { get; private set; }

        [JsonIgnore]
        public string Topic { get; private set; }

        public void SetJobId(string jobId)
            => JobId = jobId;

        public void SetRetryCount(int? retryCount)
            => RetryCount = retryCount;

        public virtual void Validate()
        {
            throw new NotImplementedException();
        }


    }
}
