using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainer.Domain.Entities.Result;

namespace Trainer.Persistence.Configurations.Results
{
    public class ResultsConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasOne(x => x.Examination)
                .WithOne(x => x.Result)
                .HasForeignKey<Result>(x => x.ExaminationId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Results)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
