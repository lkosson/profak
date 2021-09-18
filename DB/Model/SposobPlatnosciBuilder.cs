using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class SposobPlatnosciBuilder
	{
		public static void Configure(EntityTypeBuilder<SposobPlatnosci> builder)
		{
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).HasConversion(v => v.Id, v => v);
			builder.Property(e => e.Nazwa).IsRequired();
			builder.Property(e => e.LiczbaDni).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CzyDomyslny).HasDefaultValue(false).IsRequired();
		}
	}
}
