using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendingAnalysis.DataAccess.Entities;

namespace SpendingAnalysis.DataAccess.Configuretions
{
    public class SpendingConfiguration : IEntityTypeConfiguration<SpendingEntity>
    {
        public void Configure(EntityTypeBuilder<SpendingEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.Amount)
                .IsRequired();

            builder.Property(s => s.Date)
                .IsRequired();
        }
    }
}
