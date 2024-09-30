using System.Text.Json.Serialization;

namespace Sistema.Cadastro.Domain.Clientes.Endereco.DTOs
{
    public class EnderecoDto
    {
        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }
    }
}
