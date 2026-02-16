using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class StawkaVatBuilder
{
	public static void Configure(EntityTypeBuilder<StawkaVat> builder)
	{
		builder.ToTable(nameof(StawkaVat));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.Skrot).HasDefaultValue("").IsRequired();
		builder.Property(e => e.Wartosc).HasDefaultValue(0).IsRequired();
		builder.Property(e => e.CzyDomyslna).HasDefaultValue(false).IsRequired();
	}
}
