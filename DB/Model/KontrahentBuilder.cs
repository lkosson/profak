using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class KontrahentBuilder
{
	public static void Configure(EntityTypeBuilder<Kontrahent> builder)
	{
		builder.ToTable(nameof(Kontrahent));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.Nazwa).HasDefaultValue("").IsRequired();
		builder.Property(e => e.PelnaNazwa).HasDefaultValue("").IsRequired();
		builder.Property(e => e.NIP).HasDefaultValue("").IsRequired();
		builder.Property(e => e.AdresRejestrowy).HasDefaultValue("").IsRequired();
		builder.Property(e => e.AdresKorespondencyjny).HasDefaultValue("").IsRequired();
		builder.Property(e => e.RachunekBankowy).HasDefaultValue("").IsRequired();
		builder.Property(e => e.NazwaBanku).HasDefaultValue("").IsRequired();
		builder.Property(e => e.Telefon).HasDefaultValue("").IsRequired();
		builder.Property(e => e.EMail).HasDefaultValue("").IsRequired();
		builder.Property(e => e.UwagiPubliczne).HasDefaultValue("").IsRequired();
		builder.Property(e => e.UwagiWewnetrzne).HasDefaultValue("").IsRequired();
		builder.Property(e => e.CzyArchiwalny).HasDefaultValue(false).IsRequired();
		builder.Property(e => e.CzyPodmiot).HasDefaultValue(false).IsRequired();
		builder.Property(e => e.CzyTP).HasDefaultValue(false).IsRequired();
		builder.Property(e => e.KodUrzedu).HasDefaultValue("").IsRequired();
		builder.Property(e => e.OsobaFizycznaImie).HasDefaultValue("").IsRequired();
		builder.Property(e => e.OsobaFizycznaNazwisko).HasDefaultValue("").IsRequired();
		builder.Property(e => e.OsobaFizycznaDataUrodzenia);
		builder.Property(e => e.FormaOpodatkowania);
		builder.Property(e => e.TokenKSeF).HasDefaultValue("").IsRequired();
		builder.Property(e => e.SrodowiskoKSeF).HasDefaultValue(SrodowiskoKSeF.Test).IsRequired();

		builder.Property(e => e.SposobPlatnosciId);
		builder.Property(e => e.DomyslnaWalutaId);

		builder.Ignore(e => e.SposobPlatnosciRef);
		builder.Ignore(e => e.DomyslnaWalutaRef);

		builder.HasOne(e => e.SposobPlatnosci).WithMany().HasForeignKey(e => e.SposobPlatnosciId).OnDelete(DeleteBehavior.SetNull);
		builder.HasOne(e => e.DomyslnaWaluta).WithMany().HasForeignKey(e => e.DomyslnaWalutaId).OnDelete(DeleteBehavior.SetNull);
	}
}
