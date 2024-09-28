using Sistema.Cadastro.CrossCutting.Common.Entities;
using Sistema.Cadastro.Domain.Clientes.Endereco.DTOs;
using Sistema.Cadastro.Domain.Clientes.Endereco.ValueObjects;

namespace Sistema.Cadastro.Domain.Clientes.Endereco
{
    public class Enderecos : AggregateRoot
    {
        protected Enderecos()
        { }

        public required Cep Cep;
        public required Numero Numero;
        public string Endereco { get; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }

        public Enderecos(Cep cep, Numero numero, EnderecosDto dto)
        {
            Cep = cep;
            Numero = numero;
            Endereco = dto.Endereco;
            Complemento = dto.Complemento;
            Bairro = dto.Bairro;
            UF = dto.UF;
        }
    }
}
