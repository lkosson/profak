using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class KontrahentBuilder
	{
		public static void Configure(EntityTypeBuilder<Kontrahent> builder)
		{
			builder.ToTable(nameof(Kontrahent));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).HasConversion(v => v.Id, v => v);
			builder.Property(e => e.Nazwa).IsRequired();
			builder.Property(e => e.PelnaNazwa).IsRequired();
			builder.Property(e => e.NIP).IsRequired();
			builder.Property(e => e.AdresRejestrowy);
			builder.Property(e => e.AdresKorespondencyjny);
			builder.Property(e => e.RachunekBankowy);
			builder.Property(e => e.Telefon);
			builder.Property(e => e.EMail);
			builder.Property(e => e.Uwagi);
			builder.Property(e => e.CzyArchiwalny).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyPodmiot).HasDefaultValue(false).IsRequired();
		}
	}
}
