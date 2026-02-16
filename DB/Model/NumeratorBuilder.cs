using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model
{
	class NumeratorBuilder
	{
		public static void Configure(EntityTypeBuilder<Numerator> builder)
		{
			builder.ToTable(nameof(Numerator));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Przeznaczenie).IsRequired();
			builder.Property(e => e.Format).HasDefaultValue("").IsRequired();
		}
	}
}
