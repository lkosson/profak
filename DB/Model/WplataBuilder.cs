using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	class WplataBuilder
	{
		public static void Configure(EntityTypeBuilder<Wplata> builder)
		{
			builder.ToTable(nameof(Wplata));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.FakturaId).IsRequired();
			builder.Property(e => e.Data).IsRequired();
			builder.Property(e => e.Kwota).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.Uwagi).HasDefaultValue("").IsRequired();

			builder.Ignore(e => e.FakturaRef);

			builder.HasOne(e => e.Faktura).WithMany(e => e.Wplaty).HasForeignKey(e => e.FakturaId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
