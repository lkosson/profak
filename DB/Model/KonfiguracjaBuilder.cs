using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model;

class KonfiguracjaBuilder
{
	public static void Configure(EntityTypeBuilder<Konfiguracja> builder)
	{
		builder.ToTable(nameof(Konfiguracja));

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Wersja);
		builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
		builder.Property(e => e.SMTPSerwer).HasDefaultValue("").IsRequired();
		builder.Property(e => e.SMTPLogin).HasDefaultValue("").IsRequired();
		builder.Property(e => e.SMTPHaslo).HasDefaultValue("").IsRequired();
		builder.Property(e => e.SMTPPort);
		builder.Property(e => e.EMailNadawca).HasDefaultValue("").IsRequired();
		builder.Property(e => e.EMailTemat).HasDefaultValue("").IsRequired();
		builder.Property(e => e.EMailTresc).HasDefaultValue("").IsRequired();

		builder.Property(e => e.SkrotyKlawiaturoweAkcji);
		builder.Property(e => e.SkrotyKlawiaturoweZakladek);
		builder.Property(e => e.SkrotyKlawiaturowePrzyciskow);
		builder.Property(e => e.IkonyAkcji);
		builder.Property(e => e.DomyslnyPodgladStrony);
		builder.Property(e => e.PotwierdzanieZamknieciaEdytora);
		builder.Property(e => e.PotwierdzanieZamknieciaProgramu);
		builder.Property(e => e.WstepneLadowanieReportingServices);
		builder.Property(e => e.SzerokoscMenu);
		builder.Property(e => e.RozmiarCzcionki);
		builder.Property(e => e.NazwaCzcionki).HasDefaultValue("").IsRequired();
	}
}
