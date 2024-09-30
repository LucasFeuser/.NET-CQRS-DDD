using Sistema.Cadastro.CrossCutting.Common.CQRS;
using System.Runtime.Serialization;

namespace Sistema.Cadastro.Application.Cadastro.Paciente.Queries
{
    [DataContract]
    public class ListarPacientesQuery : Query
    {
    }
}
