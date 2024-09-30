using Sistema.Cadastro.CrossCutting.Common.Entities;
using Sistema.Cadastro.Domain.Clientes.Endereco.DTOs;
using Sistema.Cadastro.Domain.Clientes.Endereco.ValueObjects;
using Sistema.Cadastro.Domain.Clientes.Paciente;

namespace Sistema.Cadastro.Domain.Clientes.Endereco
{
    public class Enderecos : AggregateRoot
    {
        protected Enderecos()
        { }

        public string Cep { get; }
        public int Numero { get; }
        public string Rua { get; }
        public string Complemento { get; } = string.Empty;
        public string Bairro { get; }
        public string Cidade { get; }
        public string UF { get; }

        public virtual Pacientes Paciente { get; set; }

        public Enderecos(Cep cep, Numero numero, string complemento, EnderecoDto dto)
        {
            Cep = cep.Value;
            Numero = numero.Value;
            Rua = dto.Endereco;
            Complemento = complemento;
            Bairro = dto.Bairro;
            Cidade = dto.Cidade;
            UF = dto.UF;
        }
    }
}
