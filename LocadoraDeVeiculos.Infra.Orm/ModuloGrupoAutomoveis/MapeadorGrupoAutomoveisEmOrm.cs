using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis
{
    public class MapeadorGrupoAutomoveisEmOrm : IEntityTypeConfiguration<GrupoAutomoveis>
    {

        public void Configure(EntityTypeBuilder<GrupoAutomoveis> builder)
        {
            builder.ToTable("TBGrupoAutomoveis");

            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(s => s.UsuarioId)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Usuario_Id");

            builder.HasOne(g => g.Usuario)
                .WithMany()
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        //private GrupoAutomoveis[] ObterRegistrosPadrao()
        //{
        //    return
        //    [
        //        new GrupoAutomoveis { Id = 1, Nome = "Caminhonete" },
        //        new GrupoAutomoveis { Id = 2, Nome = "Utilitário" },
        //        new GrupoAutomoveis { Id = 3, Nome = "Esportivo," },
        //        new GrupoAutomoveis { Id = 4, Nome = "Adaptado para PCD" }
        //    ];
        //}
    }
}
