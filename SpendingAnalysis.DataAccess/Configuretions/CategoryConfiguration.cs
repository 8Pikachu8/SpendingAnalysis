using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpendingAnalysis.DataAccess.Entities;

namespace SpendingAnalysis.DataAccess.Configuretions
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(c => c.OperationType)
                .WithMany(o => o.Categories)
                .HasForeignKey(c => c.OperationTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
