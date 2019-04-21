using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace UsersManagement
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
            services.AddHttpClient();
            services.AddScoped<UserService>();
            services.AddScoped<LoadData>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
                   {
                       c.SwaggerDoc("v1", new Info
                       { Title = "User-Management Core 2.2 API's", Description = "This is to expost this API's using Swagger" });
                       c.AddSecurityDefinition("basic", new BasicAuthScheme { Type = "basic" });   
                   }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env,LoadData loadData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User-Management Core 2.2 API's");
            });
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();
            await loadData.OnGet();
        }
    }
}
