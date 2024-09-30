using Sistema.Cadastro.CrossCutting.Common.CQRS;
using System.Runtime.Serialization;

namespace Sistema.Cadastro.Application.Cadastro.Paciente.Queries
{
    [DataContract]
    public class ObterPacienteQuery : Query
    {
        protected ObterPacienteQuery() { }

        [DataMember]
        public string Cpf { get; set; } = string.Empty;

        public ObterPacienteQuery(string documento)
        {
            Cpf = documento;
        }
    }
}
