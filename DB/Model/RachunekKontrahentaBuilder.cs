using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ProFak.DB.Model;

class RachunekKontrahentaBuilder
{
	public static void Configure(EntityTypeBuilder<RachunekKontrahenta> builder)
	{
		builder.ToTable(nameof(RachunekKontrahenta));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.NumerRachunku).HasDefaultValue("").IsRequired();
		builder.Property(e => e.NazwaBanku).HasDefaultValue("").IsRequired();

		builder.Property(e => e.KontrahentId).IsRequired();
		builder.Property(e => e.WalutaId);

		builder.Ignore(e => e.KontrahentRef);
		builder.Ignore(e => e.WalutaRef);

		builder.HasOne(e => e.Kontrahent).WithMany(e => e.Rachunki).HasForeignKey(e => e.KontrahentId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(e => e.Waluta).WithMany().HasForeignKey(e => e.WalutaId).OnDelete(DeleteBehavior.SetNull);
	}
}
