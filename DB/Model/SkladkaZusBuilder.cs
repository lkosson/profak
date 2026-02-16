using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class SkladkaZusBuilder
{
	public static void Configure(EntityTypeBuilder<SkladkaZus> builder)
	{
		builder.ToTable(nameof(SkladkaZus));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.Miesiac).IsRequired();
		builder.Property(e => e.PodstawaSpoleczne).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.PodstawaZdrowotne).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SkladkaEmerytalna).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SkladkaRentowa).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SkladkaWypadkowa).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SkladkaSpoleczna).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SkladkaZdrowotna).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.RozliczenieRoczneSkladkiZdrowotnej).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SkladkaFunduszPracy).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SumaSkladek).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.OdliczenieOdDochodu).HasDefaultValue(0).IsRequired();
	}
}
