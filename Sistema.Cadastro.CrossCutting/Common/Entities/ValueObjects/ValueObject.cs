namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects
{
    public abstract class ValueObject
    {
        protected ValueObject() {}

        public virtual void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
