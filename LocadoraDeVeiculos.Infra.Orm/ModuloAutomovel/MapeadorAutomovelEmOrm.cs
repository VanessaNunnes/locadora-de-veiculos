using LocadoraDeVeiculos.Dominio.ModuloAutomoveis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloAutomovel;
    public class MapeadorAutomovelEmOrm : IEntityTypeConfiguration<Automovel>
{
        public void Configure(EntityTypeBuilder<Automovel> builder)
        {
            builder.ToTable("TBAutomovel");

            builder.Property(a => a.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(a => a.Placa)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(a => a.Marca)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(a => a.Cor)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(a => a.Modelo)
                .HasColumnType("varchar(100)")
                .IsRequired();


            builder.Property(a => a.TipoCombustivel)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(a => a.CapacidadeMax)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(a => a.GrupoAutomoveisId)
                .HasColumnType("int")
                .IsRequired();

			builder.HasOne(a => a.GrupoAutomoveis)
                .WithMany(e => e.Automoveis)
                .HasForeignKey(a => a.GrupoAutomoveisId)
                .OnDelete(DeleteBehavior.Restrict);

        }
}

