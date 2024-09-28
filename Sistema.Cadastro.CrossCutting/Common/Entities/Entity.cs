using Sistema.Cadastro.CrossCutting.Common.CQRS;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Cadastro.CrossCutting.Common.Entities
{
    public abstract class Entity
    {
        protected Entity(bool useUtc = false)
        {
            UseUtc = useUtc;
            DataCadastro = UseUtc ? DateTime.UtcNow : DateTime.Now;
        }

        [NotMapped]
        public bool UseUtc { get;}
        public long Id { get;}
        public virtual DateTime DataCadastro { get;}
        public virtual DateTime? DataUltimaAtualizacao { get;}

        #region Eventos

        private List<Event> _eventos = new();
        public IReadOnlyCollection<Event> Eventos => _eventos;

        public void AdicionarEvento(Event evento)
            => _eventos.Add(evento);

        public void RemoverEvento(Event evento)
            => _eventos?.Remove(evento);

        public void LimparEventos()
            => _eventos?.Clear();

        #endregion

        #region Comparações
        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (ReferenceEquals(null, compareTo))
                return false;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 907 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public static bool operator ==(Entity a, Entity b)
        {

            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public virtual void Validate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
