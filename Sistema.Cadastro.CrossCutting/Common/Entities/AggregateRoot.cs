namespace Sistema.Cadastro.CrossCutting.Common.Entities
{
    public class AggregateRoot : Entity
    {
        protected AggregateRoot(bool useUtc = false) : base(useUtc) { }
    }
}
