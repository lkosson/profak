using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class StanMenuBuilder
	{
		public static void Configure(EntityTypeBuilder<StanMenu> builder)
		{
			builder.ToTable(nameof(StanMenu));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Pozycja).IsRequired();
			builder.Property(e => e.CzyZwinieta).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyUkryta).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.CzyAktywna).HasDefaultValue(false).IsRequired();
		}
	}
}
