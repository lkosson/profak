using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class KonfiguracjaBuilder
	{
		public static void Configure(EntityTypeBuilder<Konfiguracja> builder)
		{
			builder.ToTable(nameof(Konfiguracja));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.SMTPSerwer);
			builder.Property(e => e.SMTPLogin);
			builder.Property(e => e.SMTPHaslo);
			builder.Property(e => e.SMTPPort);
			builder.Property(e => e.EMailNadawca);
			builder.Property(e => e.EMailTemat);
			builder.Property(e => e.EMailTresc);

			builder.HasData(new Konfiguracja 
			{
				Id = 1,
				SMTPSerwer = "smtp.example.com",
				SMTPPort = 465,
				SMTPLogin = "biuro",
				SMTPHaslo = "tajnehaslo",
				EMailNadawca = "[SPRZEDAWCA-NAZWA] <[SPRZEDAWCA-EMAIL]>",
				EMailTemat = "Faktura - [NUMER]",
				EMailTresc = "Dzień dobry,\r\n\r\nw załączniku znajduje się faktura numer [NUMER] z dnia [DATA] na kwotę [KWOTA-BRUTTO].\r\n\r\nWiadomość wygenerowana automatycznie.\r\n\r\n-- \r\n[SPRZEDAWCA-NAZWA]\r\n[SPRZEDAWCA-ADRES]"
			});
		}
	}
}
