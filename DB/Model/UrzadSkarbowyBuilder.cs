using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model
{
	class UrzadSkarbowyBuilder
	{
		public static void Configure(EntityTypeBuilder<UrzadSkarbowy> builder)
		{
			builder.ToTable(nameof(UrzadSkarbowy));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Kod).IsRequired();
			builder.Property(e => e.Nazwa).IsRequired();
		}
	}
}
