using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class TowarBuilder
	{
		public static void Configure(EntityTypeBuilder<Towar> builder)
		{
			builder.ToTable(nameof(Towar));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Nazwa).HasDefaultValue("").IsRequired();
			builder.Property(e => e.Rodzaj).HasDefaultValue(RodzajTowaru.Towar).IsRequired();
			builder.Property(e => e.CenaNetto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CenaBrutto).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CzyWedlugCenBrutto).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyArchiwalny).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.GTU).HasDefaultValue(0).IsRequired();

			builder.Property(e => e.StawkaVatId);
			builder.Property(e => e.JednostkaMiaryId);

			builder.Ignore(e => e.StawkaVatRef);
			builder.Ignore(e => e.JednostkaMiaryRef);

			builder.HasOne(e => e.StawkaVat).WithMany().HasForeignKey(e => e.StawkaVatId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.JednostkaMiary).WithMany().HasForeignKey(e => e.JednostkaMiaryId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
