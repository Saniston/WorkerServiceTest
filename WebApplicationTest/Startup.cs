using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace WebApplicationTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddDirectoryBrowser();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseDefaultFiles();
                app.UseRouting();

                app.UseStaticFiles();

                app.UseFileServer();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
            catch (Exception)
            {

                throw;
            }


            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();
            //app.UseCors(c => { c.AllowAnyOrigin(); });
            //app.UseFileServer(enableDirectoryBrowsing: true);

            //app.UseDefaultFiles();
            //var location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //var folder = Path.Combine(location ?? "", "wwwroot");
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(folder),
            //    RequestPath = "",
            //    ServeUnknownFileTypes = true
            //});

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}