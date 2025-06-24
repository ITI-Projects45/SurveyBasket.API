using SurveyBasket.API.Persistence;

namespace SurveyBasket.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddControllers();

        services
            .AddSwaggerServices()
            .AddMapsterConfig()
            .AddFluentValidationConfig();

        // Register Services
        services.RegisteredServices();

        services.AddDataBase(configuration);

        return services;
    }
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection String DefaultConnection not Exist");
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
        return services;
    }
    public static IServiceCollection RegisteredServices(this IServiceCollection services)
    {
        // Register services
        services.AddKeyedScoped<IPollService, PollService>("PollService");
        return services;
    }
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        // Add Swagger services
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        // Add Mapster
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }
    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        // Add Vaidation
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}
