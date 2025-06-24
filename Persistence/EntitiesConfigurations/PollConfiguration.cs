using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.API.Persistence.EntitiesConfigurations;

public class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasIndex(p => p.Title).IsUnique();
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(p => p.Summary).HasMaxLength(1000);
    }
}
