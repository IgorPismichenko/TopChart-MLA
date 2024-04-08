using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TopChart_DLL.EF;

namespace TopChart_BLL.Infrastructure
{
    public static class TopChartContextExtensions
    {
        public static void AddTopChartMLAContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<TopChartDbMLAContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));
        }
    }
}
