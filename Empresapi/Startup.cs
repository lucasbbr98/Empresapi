using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Empresapi
{
    using Authentication;
    using Services;
    using Services.CVM;
    using Services.CVM.ITR;
    using Services.CVM.DFP;

    using Services.CVM.FCA;
    using Services.Interfaces;
    using Services.CVM.FRE;

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
            services.AddCors();
            services.AddControllersWithViews();

            // Add Response compression services
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            // JWT
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = JwtSecurityKey.Create(Configuration["JWT:SecretKey"])
                    };
                });
            services.AddMvcCore().AddAuthorization();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IHttpClientService, HttpClientService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICVMSourceService, CVMSourceService>();
            services.AddTransient<IITRDividendService, ITRDividendService>();
            services.AddTransient<IITRShareCapitalService, ITRShareCapitalService>();
            services.AddTransient<IITRFinancialReportService, ITRFinancialReportService>();
            services.AddTransient<IDFPFinancialReportService, DFPFinancialReportService>();
            services.AddTransient<IFCACompanyIssuerService, FCACompanyIssuerService>();
            services.AddTransient<IFCACompanySecurityService, FCACompanySecurityService>();
            services.AddTransient<IFCACompanyIssuerService, FCACompanyIssuerService>();
            services.AddTransient<IFRECompanyIntangibleService, FRECompanyIntangibleService>();
            services.AddTransient<IFRECompanyOwnershipService, FRECompanyOwnershipService>();
            services.AddTransient<IFRECompanyFixedAssetService, FRECompanyFixedAssetService>();
            services.AddTransient<IFRECompanyAuditorService, FRECompanyAuditorService>();
            services.AddTransient<IFRECompanyDebtService, FRECompanyDebtService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
