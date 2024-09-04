using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomoveis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloGrupoAutomoveis;
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

        }

    }
