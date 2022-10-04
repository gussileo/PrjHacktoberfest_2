using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Usuario.Model;

namespace Usuario.Data.EntityTypeConfiguration
{
	public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.ToTable("Usuario");

			builder
				.Property(x => x.Id)
				.IsRequired();

			builder
				.Property(x => x.Name)
				.IsRequired();

			builder
				.Property(x => x.DataNascimento)
				.IsRequired();

			builder
				.Property(x => x.CPF)
				.IsRequired();

			builder
				.Property(x => x.Endereco)
				.IsRequired();

			builder
				.HasKey(x => x.Id)
				.HasName("PK_Usuario");
		}
	}
}
