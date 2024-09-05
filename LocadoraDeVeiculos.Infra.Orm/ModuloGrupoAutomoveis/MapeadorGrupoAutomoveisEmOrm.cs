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

            builder.Property(s => s.EmpresaId)
	            .HasColumnType("int")
	            .HasColumnName("Empresa_Id")
	            .IsRequired();

            builder.HasOne(g => g.Empresa)
	            .WithMany()
	            .HasForeignKey(s => s.EmpresaId)
	            .OnDelete(DeleteBehavior.Restrict);

	}

    }
