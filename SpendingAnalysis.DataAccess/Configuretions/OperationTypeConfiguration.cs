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
    public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationTypeEntity>
    {
        public void Configure(EntityTypeBuilder<OperationTypeEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasData(
                new OperationTypeEntity { Id = 0, Name = "Расход"},
                new OperationTypeEntity { Id = 1, Name = "Доход"},
                new OperationTypeEntity { Id = 2, Name = "Перевод"},
                new OperationTypeEntity { Id = 3, Name = "Долг"}
            );
        }
    }
}
