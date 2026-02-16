using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model
{
	class StanNumeratoraBuilder
	{
		public static void Configure(EntityTypeBuilder<StanNumeratora> builder)
		{
			builder.ToTable(nameof(StanNumeratora));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.NumeratorId).IsRequired();
			builder.Property(e => e.Parametry).HasDefaultValue("").IsRequired();
			builder.Property(e => e.OstatniaWartosc).HasDefaultValue(0).IsRequired();

			builder.Ignore(e => e.NumeratorRef);

			builder.HasOne(e => e.Numerator).WithMany(e => e.Stany).HasForeignKey(e => e.NumeratorId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
