

namespace SurveyBasket.API.Persistence.EntitiesConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
       
        builder.Property(U => U.FirstName)
            .HasMaxLength(30);
        builder.Property(U => U.LastName)
            .HasMaxLength(30);
    }
}
