using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServicesContainer
{
    public static IServiceCollection ApplicationServices(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
