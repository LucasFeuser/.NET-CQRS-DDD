using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Cadastro.Domain.Clientes.Endereco;
using Microsoft.EntityFrameworkCore;

namespace Sistema.Cadastro.Infrastructure.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Enderecos>
    {
        public void Configure(EntityTypeBuilder<Enderecos> builder)
        {
            builder.ToTable("enderecos");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id_endereco")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Cep)
                .IsRequired()
                .HasColumnName("cep")
                .HasMaxLength(8);

            builder.Property(e => e.Numero)
                .IsRequired()
                .HasColumnName("numero");

            builder.Property(e => e.Rua)
                .IsRequired()
                .HasColumnName("rua")
                .HasMaxLength(100);

            builder.Property(e => e.Complemento)
                .HasColumnName("complemento")
                .HasMaxLength(100);

            builder.Property(e => e.Bairro)
                .IsRequired()
                .HasColumnName("bairro")
                .HasMaxLength(100);

            builder.Property(e => e.Cidade)
                .IsRequired()
                .HasColumnName("cidade")
                .HasMaxLength(100);

            builder.Property(e => e.UF)
                .IsRequired()
                .HasColumnName("uf")
                .HasMaxLength(2);

            builder.Ignore(x => x.DataUltimaAtualizacao);
            builder.Ignore(x => x.DataCadastro);
        }
    }
}
