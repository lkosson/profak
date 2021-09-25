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
			builder.Property(e => e.FakturaId).IsRequired();
			builder.Property(e => e.TowarId).IsRequired();
			builder.Property(e => e.Opis).IsRequired();
			builder.Property(e => e.KwotaNetto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.KwotaVat).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.KwotaBrutto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.Ilosc).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.WartoscNetto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.WartoscVat).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.WartoscBrutto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CzyWedlugCenBrutto).HasDefaultValue(false).IsRequired();

			builder.Ignore(e => e.FakturaRef);
			builder.Ignore(e => e.TowarRef);

			builder.HasOne(e => e.Faktura).WithMany(e => e.Pozycje).HasForeignKey(e => e.FakturaId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.Towar).WithMany().HasForeignKey(e => e.TowarId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
