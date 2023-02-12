using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainer.Domain.Entities.Examination;

namespace Trainer.Persistence.Configurations.Examiantion
{
    public class ExaminationConfiguration : IEntityTypeConfiguration<Examination>
    {
        public void Configure(EntityTypeBuilder<Examination> builder)
        {
            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Examinations)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
