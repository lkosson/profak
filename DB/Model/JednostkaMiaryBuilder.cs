using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model
{
	class JednostkaMiaryBuilder
	{
		public static void Configure(EntityTypeBuilder<JednostkaMiary> builder)
		{
			builder.ToTable(nameof(JednostkaMiary));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Skrot).HasDefaultValue("").IsRequired();
			builder.Property(e => e.Nazwa).HasDefaultValue("").IsRequired();
			builder.Property(e => e.CzyDomyslna).HasDefaultValue(false).IsRequired();
			builder.Property(e => e.LiczbaMiescPoPrzecinku).HasDefaultValue(0).IsRequired();
		}
	}
}
