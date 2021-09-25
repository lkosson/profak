using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class WalutaBuilder
	{
		public static void Configure(EntityTypeBuilder<Waluta> builder)
		{
			builder.ToTable(nameof(Waluta));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Skrot).IsRequired();
			builder.Property(e => e.Nazwa).IsRequired();
			builder.Property(e => e.CzyDomyslna).HasDefaultValue(false).IsRequired();
		}
	}
}
