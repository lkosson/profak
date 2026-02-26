using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class KolumnaSpisuBuilder
{
	public static void Configure(EntityTypeBuilder<KolumnaSpisu> builder)
	{
		builder.ToTable(nameof(KolumnaSpisu));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.Spis).IsRequired();
		builder.Property(e => e.Kolumna).IsRequired();
		builder.Property(e => e.Kolejnosc).IsRequired();
		builder.Property(e => e.Szerokosc).IsRequired();
		builder.Property(e => e.PoziomSortowania).IsRequired();
	}
}
