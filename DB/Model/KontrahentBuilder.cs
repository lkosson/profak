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

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Nazwa).HasDefaultValue("").IsRequired();
			builder.Property(e => e.PelnaNazwa).HasDefaultValue("").IsRequired();
			builder.Property(e => e.NIP).HasDefaultValue("").IsRequired();
			builder.Property(e => e.AdresRejestrowy).HasDefaultValue("");
			builder.Property(e => e.AdresKorespondencyjny).HasDefaultValue("");
			builder.Property(e => e.RachunekBankowy).HasDefaultValue("");
			builder.Property(e => e.Telefon).HasDefaultValue("");
			builder.Property(e => e.EMail).HasDefaultValue("");
			builder.Property(e => e.Uwagi).HasDefaultValue("");
			builder.Property(e => e.CzyArchiwalny).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyPodmiot).HasDefaultValue(false).IsRequired();
		}
	}
}
