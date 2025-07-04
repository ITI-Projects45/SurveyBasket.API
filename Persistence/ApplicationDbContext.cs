



namespace SurveyBasket.API.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Poll> Polls { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
