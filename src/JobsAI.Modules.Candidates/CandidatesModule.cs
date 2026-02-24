using JobsAI.Modules.Candidates.Application.Abstractions;
using JobsAI.Modules.Candidates.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobsAI.Modules.Candidates;

public static class CandidatesModule
{
    public static IServiceCollection AddCandidatesModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CandidatesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("JobsAIDb")));

        services.AddScoped<ICandidateRepository, CandidateRepository>();

        return services;
    }
}
