using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class ZaliczkaPitBuilder
{
	public static void Configure(EntityTypeBuilder<ZaliczkaPit> builder)
	{
		builder.ToTable(nameof(ZaliczkaPit));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.Miesiac).IsRequired();
		builder.Property(e => e.Przychody).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Koszty).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.SkladkiZus).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Podatek).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Przeniesiony).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.DoPrzeniesienia).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.DoWplaty).HasDefaultValue(0).IsRequired();
	}
}
