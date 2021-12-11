using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class PozycjaFakturyBuilder
	{
		public static void Configure(EntityTypeBuilder<PozycjaFaktury> builder)
		{
			builder.ToTable(nameof(PozycjaFaktury));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Opis).HasDefaultValue("").IsRequired();
			builder.Property(e => e.CenaNetto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CenaVat).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CenaBrutto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.Ilosc).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.WartoscNetto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.WartoscVat).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.WartoscBrutto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CzyWedlugCenBrutto).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyWartosciReczne).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.LP).HasDefaultValue(1).IsRequired();
			builder.Property(e => e.CzyPrzedKorekta).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.GTU).HasDefaultValue(0).IsRequired();

			builder.Property(e => e.FakturaId).IsRequired();
			builder.Property(e => e.TowarId);
			builder.Property(e => e.StawkaVatId);

			builder.Ignore(e => e.FakturaRef);
			builder.Ignore(e => e.TowarRef);
			builder.Ignore(e => e.StawkaVatRef);

			builder.HasOne(e => e.Faktura).WithMany(e => e.Pozycje).HasForeignKey(e => e.FakturaId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.Towar).WithMany().HasForeignKey(e => e.TowarId).OnDelete(DeleteBehavior.SetNull);
			builder.HasOne(e => e.StawkaVat).WithMany().HasForeignKey(e => e.StawkaVatId).OnDelete(DeleteBehavior.NoAction);
		}
	}
}
