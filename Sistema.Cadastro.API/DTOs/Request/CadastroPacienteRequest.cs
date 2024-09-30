
namespace Sistema.Cadastro.API.DTOs.Request
{
    public class CadastroPacienteRequest
    {
        public string Cpf { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; } = new();
        public string Sexo { get; set; } = "U, M ou F";
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PlanoSaude { get; set; } = 0;
        public string NumeroCarterinha { get; set; } = string.Empty;
        public bool ReceberNotificacoesWhats { get; set; } = false;
        public EnderecoRequest Endereco { get; set; } = new();
    }

    public class EnderecoRequest
    {
        public string Cep { get; set; } = string.Empty;
        public int Numero { get; set; } = 0;
        public string Complemento {  get; set; } = string.Empty;
    }
}
