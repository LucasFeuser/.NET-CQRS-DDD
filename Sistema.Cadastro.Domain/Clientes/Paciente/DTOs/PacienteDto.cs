using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.DTOs
{
    public class PacienteDto : BasePacienteDto
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
