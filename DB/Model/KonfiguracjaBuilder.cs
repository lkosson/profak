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
			builder.Property(e => e.SMTPSerwer).HasDefaultValue("").IsRequired();
			builder.Property(e => e.SMTPLogin).HasDefaultValue("").IsRequired();
			builder.Property(e => e.SMTPHaslo).HasDefaultValue("").IsRequired();
			builder.Property(e => e.SMTPPort);
			builder.Property(e => e.EMailNadawca).HasDefaultValue("").IsRequired();
			builder.Property(e => e.EMailTemat).HasDefaultValue("").IsRequired();
			builder.Property(e => e.EMailTresc).HasDefaultValue("").IsRequired();

			builder.HasData(Konfiguracja.Domyslna);
		}
	}
}
