using Sistema.Cadastro.CrossCutting.Common.CQRS;
using System.Runtime.Serialization;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Queries
{
    [DataContract]
    public class ListarPacientesQuery : Query
    {
        protected ListarPacientesQuery()
        {   }
    }
}
