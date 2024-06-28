using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;
using OfficeManager.Domain.Services;
using OfficeManager.Infra.Context;
using OfficeManager.Infra.Repositories;

namespace OfficeManager.Api
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext, OfficeManagerContext>();

            //Services
            services.AddScoped<IFeeService, FeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IClientMeetingService, ClientMeetingService>();

            //Repositories
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IClientMeetingRepository, ClientMeetingRepository>();

            return services;
        }

    }
}
