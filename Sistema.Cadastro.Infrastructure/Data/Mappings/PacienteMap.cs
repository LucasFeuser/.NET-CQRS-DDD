using Microsoft.EntityFrameworkCore;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;

namespace Sistema.Cadastro.Infrastructure.Data.Mappings
{
    internal class PacienteMap : IEntityTypeConfiguration<Pacientes>
    {
        public void Configure(EntityTypeBuilder<Pacientes> builder)
        {
            builder.ToTable("pacientes");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                 .HasColumnName("id_paciente")
                 .ValueGeneratedOnAdd();

            builder.Property(s => s.Cpf)
                .HasColumnName("num_cpf")
                .IsRequired();

            builder.Property(s => s.NomeCompleto)
                .HasColumnName("nome_completo")
                .IsRequired();

            builder.Property(s => s.DataNascimento)
                .HasColumnName("data_nascimento")
                .IsRequired()
                .HasConversion(c => c.ToUniversalTime(), c => DateTime.SpecifyKind(c, DateTimeKind.Utc));

            builder.Property(s => s.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(s => s.Sexo)
                .HasColumnName("genero")
                .HasConversion(c => (char)c, c => (ESexo)c)
                .HasColumnType("char(1)");

            builder.Property(s => s.Telefone)
                .HasColumnName("num_telefone")
                .HasColumnType("varchar(11)");

            builder.Property(s => s.PlanoSaude)
                .HasColumnName("plano_saude");

            builder.Property(s => s.NumeroCarterinha)
                .HasColumnName("num_carteirinha");

            builder.Property(s => s.ReceberNotificacoesWhats)
                .HasColumnName("notificacao_wpp")
                .HasColumnType("boolean");

            builder.Property(s => s.EnderecoId)
                .IsRequired()
                .HasColumnName("endereco_id");

            builder.HasOne(p => p.Endereco)
               .WithOne(e => e.Paciente)
               .HasForeignKey<Pacientes>(p => p.EnderecoId);

            builder.Ignore(x => x.DataUltimaAtualizacao);
            builder.Ignore(x => x.DataCadastro);
        }
    }
}
