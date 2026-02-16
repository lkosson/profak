using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProFak.DB.Model
{
	class ZawartoscBuilder
	{
		public static void Configure(EntityTypeBuilder<Zawartosc> builder)
		{
			builder.ToTable(nameof(Zawartosc));

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
			builder.Property(e => e.Dane).IsRequired();
			builder.Property(e => e.PlikId);

			builder.Ignore(e => e.PlikRef);
		}
	}
}
