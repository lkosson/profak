using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class JednostkaMiaryBuilder
	{
		public static void Configure(EntityTypeBuilder<JednostkaMiary> builder)
		{
			builder.ToTable(nameof(JednostkaMiary));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Skrot).IsRequired();
			builder.Property(e => e.Nazwa).IsRequired();
			builder.Property(e => e.CzyGlowna).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.LiczbaMiescPoPrzecinku).HasDefaultValue(0).IsRequired();
		}
	}
}
