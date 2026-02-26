using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class DeklaracjaVatBuilder
{
	public static void Configure(EntityTypeBuilder<DeklaracjaVat> builder)
	{
		builder.ToTable(nameof(DeklaracjaVat));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.Miesiac).IsRequired();
		builder.Property(e => e.NettoZW).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Netto0).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Netto5).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Netto8).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Netto23).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NettoWDT).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NettoWNT).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Nalezny5).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Nalezny8).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.Nalezny23).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NaleznyWNT).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NettoSrodkiTrwale).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NettoPozostale).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NaliczonyPrzeniesiony).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NaliczonySrodkiTrwale).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.NaliczonyPozostale).HasDefaultValue(0).IsRequired();
	}
}
