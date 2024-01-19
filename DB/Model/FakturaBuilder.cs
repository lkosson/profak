using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class FakturaBuilder
	{
		public static void Configure(EntityTypeBuilder<Faktura> builder)
		{
			builder.ToTable(nameof(Faktura));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Numer).HasDefaultValue("").IsRequired();
			builder.Property(e => e.DataWystawienia).IsRequired();
			builder.Property(e => e.DataSprzedazy).IsRequired();
			builder.Property(e => e.DataWprowadzenia).IsRequired();
			builder.Property(e => e.TerminPlatnosci).IsRequired();
			builder.Property(e => e.NIPSprzedawcy).HasDefaultValue("").IsRequired();
			builder.Property(e => e.NazwaSprzedawcy).HasDefaultValue("").IsRequired();
			builder.Property(e => e.DaneSprzedawcy).HasDefaultValue("").IsRequired();
			builder.Property(e => e.NIPNabywcy).HasDefaultValue("").IsRequired();
			builder.Property(e => e.NazwaNabywcy).HasDefaultValue("").IsRequired();
			builder.Property(e => e.DaneNabywcy).HasDefaultValue("").IsRequired();
			builder.Property(e => e.RachunekBankowy).HasDefaultValue("").IsRequired();
			builder.Property(e => e.UwagiWewnetrzne).HasDefaultValue("").IsRequired();
			builder.Property(e => e.UwagiPubliczne).HasDefaultValue("").IsRequired();
			builder.Property(e => e.RazemNetto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.RazemVat).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.RazemBrutto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.KursWaluty).HasDefaultValue(1).IsRequired();
			builder.Property(e => e.OpisSposobuPlatnosci).HasDefaultValue("").IsRequired();
			builder.Property(e => e.Rodzaj).HasDefaultValue(RodzajFaktury.Sprzedaż).IsRequired();
			builder.Property(e => e.CzyWartosciReczne).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.ProcentVatNaliczonego).HasDefaultValue(100m).IsRequired();
			builder.Property(e => e.ProcentKosztow).HasDefaultValue(100m).IsRequired();
			builder.Property(e => e.CzyTP).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyZakupSrodkowTrwalych).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyWDT).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyWNT).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.OpisZdarzenia).HasDefaultValue("").IsRequired();
			builder.Property(e => e.NumerKSeF).HasDefaultValue("").IsRequired();
			builder.Property(e => e.XMLKSeF).HasDefaultValue("").IsRequired();
			builder.Property(e => e.URLKSeF).HasDefaultValue("").IsRequired();
			builder.Property(e => e.DataKSeF);

			builder.Property(e => e.SprzedawcaId);
			builder.Property(e => e.NabywcaId);
			builder.Property(e => e.FakturaKorygowanaId);
			builder.Property(e => e.FakturaKorygujacaId);
			builder.Property(e => e.WalutaId);
			builder.Property(e => e.SposobPlatnosciId);
			builder.Property(e => e.DeklaracjaVatId);
			builder.Property(e => e.ZaliczkaPitId);

			builder.Ignore(e => e.SprzedawcaRef);
			builder.Ignore(e => e.NabywcaRef);
			builder.Ignore(e => e.FakturaKorygowanaRef);
			builder.Ignore(e => e.FakturaKorygujacaRef);
			builder.Ignore(e => e.WalutaRef);
			builder.Ignore(e => e.SposobPlatnosciRef);
			builder.Ignore(e => e.DeklaracjaVatRef);
			builder.Ignore(e => e.ZaliczkaPitRef);

			builder.HasOne(e => e.Sprzedawca).WithMany().HasForeignKey(e => e.SprzedawcaId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.Nabywca).WithMany().HasForeignKey(e => e.NabywcaId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.FakturaKorygowana).WithMany().HasForeignKey(e => e.FakturaKorygowanaId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.FakturaKorygujaca).WithMany().HasForeignKey(e => e.FakturaKorygujacaId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.Waluta).WithMany().HasForeignKey(e => e.WalutaId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.SposobPlatnosci).WithMany().HasForeignKey(e => e.SposobPlatnosciId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.DeklaracjaVat).WithMany(e => e.Faktury).HasForeignKey(e => e.DeklaracjaVatId).OnDelete(DeleteBehavior.SetNull);
			builder.HasOne(e => e.ZaliczkaPit).WithMany(e => e.Faktury).HasForeignKey(e => e.ZaliczkaPitId).OnDelete(DeleteBehavior.SetNull);
		}
	}
}
