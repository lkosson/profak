using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class StawkaVatBuilder
	{
		public static void Configure(EntityTypeBuilder<StawkaVat> builder)
		{
			builder.ToTable(nameof(StawkaVat));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Skrot).HasDefaultValue("").IsRequired();
			builder.Property(e => e.Wartosc).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CzyDomyslna).HasDefaultValue(false).IsRequired();
		}
	}
}
