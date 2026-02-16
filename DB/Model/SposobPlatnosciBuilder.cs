using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model
{
	class SposobPlatnosciBuilder
	{
		public static void Configure(EntityTypeBuilder<SposobPlatnosci> builder)
		{
			builder.ToTable(nameof(SposobPlatnosci));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Nazwa).HasDefaultValue("").IsRequired();
			builder.Property(e => e.LiczbaDni).HasDefaultValue(0).IsRequired();
			builder.Property(e => e.CzyDomyslny).HasDefaultValue(false).IsRequired();
		}
	}
}
