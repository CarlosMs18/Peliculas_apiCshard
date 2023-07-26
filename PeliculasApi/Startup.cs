using Microsoft.EntityFrameworkCore;
using PeliculasApi.Servicios;

namespace PeliculasApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));


            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            services.AddHttpContextAccessor();


            services.AddDbContext<ApplicationDbContext>(options => options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers()
                .AddNewtonsoftJson();
          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();    
            }

         

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
