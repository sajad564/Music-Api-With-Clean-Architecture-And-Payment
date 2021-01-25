using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using music.Domain.Entities;

namespace music.Infrastructure.Data.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id) ; 

            builder
            .HasMany(c => c.Items)
            .WithOne(ci => ci.Cart)
            .IsRequired()
            .HasForeignKey(ci => ci.CartId);
        }
    }
}