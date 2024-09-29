using Microsoft.EntityFrameworkCore;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema.Cadastro.Infrastructure.Data.Mappings
{
    internal class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable(nameof(Paciente));

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasColumnName("id_paciente")
                   .IsRequired();

            builder.Property(s => s.Cpf)
                .HasColumnName("num_cpf")
                .IsRequired();

            builder.Property(s => s.NomeCompleto)
                .HasColumnName("nome_completo")
                .IsRequired();

            builder.Property(s => s.DataNascimento)
                .HasColumnName("data_nascimento")
                .IsRequired();

            builder.Property(s => s.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(s => s.Sexo)
                .HasColumnName("genero")
                .HasColumnType("char");

            builder.Property(s => s.Telefone)
                .HasColumnName("num_telefone")
                .HasColumnType("varchar(11)");

            builder.Property(s => s.PlanoSaude)
                .HasColumnName("plano_saude")
                .HasColumnType("char");

            builder.Property(s => s.NumeroCarterinha)
                .HasColumnName("num_carteirinha");

            builder.Property(s => s.ReceberNotificacoesWhats)
                .HasColumnName("notificacao_wpp")
                .HasColumnType("boolean");

            //AGGREGATE
            builder.Property(s => s.DataCadastro)
                   .HasColumnName("dta_cadastro")
                   .HasColumnType("timestamp");

            builder.Property(s => s.DataUltimaAtualizacao)
                   .HasColumnName("dta_modificacao")
                   .HasColumnType("timestamp");
        }
    }
}
