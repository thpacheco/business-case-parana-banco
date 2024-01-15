using Business.Case.ParanaBanco.API.Domain.Interfaces;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Business.Case.ParanaBanco.API.Infra.Context;
using Business.Case.ParanaBanco.API.Domain.Static;

namespace Business.Case.ParanaBanco.API.Infra.DependencyInjection
{
    public static class DependencyInjectionExtension
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var implementationsType = typeof(DependencyInjectionExtension).Assembly.GetTypes()
              .Where(t => typeof(IService).IsAssignableFrom(t) &&
                     t.BaseType != null);

            foreach (var item in implementationsType)
                services.AddScoped(item);

            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddScoped<dbBusinessCaseContext_>();

            services.AddScoped(sp =>
            {
                return new dbBusinessCaseContext(new SqlConnection(RuntimeConfig.ConnectStringSqlServer));
            });

            return services;
        }
        private static string ToBase64(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }
    }
}
