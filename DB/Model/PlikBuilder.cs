using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class PlikBuilder
{
	public static void Configure(EntityTypeBuilder<Plik> builder)
	{
		builder.ToTable(nameof(Plik));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.FakturaId).IsRequired();
		builder.Property(e => e.Nazwa).HasDefaultValue("").IsRequired();
		builder.Property(e => e.Rozmiar).HasDefaultValue(0).IsRequired();

		builder.Ignore(e => e.FakturaRef);
		builder.Ignore(e => e.ZawartoscRef);

		builder.HasOne(e => e.Faktura).WithMany(e => e.Pliki).HasForeignKey(e => e.FakturaId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(e => e.Zawartosc).WithOne(e => e.Plik).HasForeignKey<Zawartosc>(e => e.PlikId).OnDelete(DeleteBehavior.Cascade);
	}
}
