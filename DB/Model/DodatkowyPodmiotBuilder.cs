using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ProFak.DB.Model;

class DodatkowyPodmiotBuilder
{
	public static void Configure(EntityTypeBuilder<DodatkowyPodmiot> builder)
	{
		builder.ToTable(nameof(DodatkowyPodmiot));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.Rodzaj).IsRequired();
		builder.Property(e => e.IDwew).HasDefaultValue("").IsRequired();
		builder.Property(e => e.Nazwa).HasDefaultValue("").IsRequired();
		builder.Property(e => e.NIP).HasDefaultValue("").IsRequired();
		builder.Property(e => e.VatUE).HasDefaultValue("").IsRequired();
		builder.Property(e => e.Adres).HasDefaultValue("").IsRequired();
		builder.Property(e => e.EMail).HasDefaultValue("").IsRequired();
		builder.Property(e => e.Telefon).HasDefaultValue("").IsRequired();
		builder.Property(e => e.Udzial);

		builder.Property(e => e.FakturaId).IsRequired();

		builder.Ignore(e => e.FakturaRef);

		builder.HasOne(e => e.Faktura).WithMany(e => e.DodatkowePodmioty).HasForeignKey(e => e.FakturaId).OnDelete(DeleteBehavior.Cascade);
	}
}
