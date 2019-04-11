using Gif.Service.Authentications;
using Gif.Service.Contracts;
using Gif.Service.Crm;
using Gif.Service.Filters;
using Gif.Service.Interfaces;
using Gif.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Net;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace Gif.Service
{
  /// <summary>
  /// Startup
  /// </summary>
  public class Startup
  {
    private readonly IHostingEnvironment _hostingEnv;

    private IServiceProvider ServiceProvider { get; set; }
    private IConfiguration Configuration { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="env"></param>
    public Startup(IHostingEnvironment env)
    {
      _hostingEnv = env;

      var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile("hosting.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>();

      Configuration = builder.Build();

      // database connection string for nLog
      GlobalDiagnosticsContext.Set("LOG_CONNECTIONSTRING", Settings.LOG_CONNECTIONSTRING(Configuration));

      DumpSettings();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(sp => Configuration);
      services.AddSingleton<IBasicAuthentication, BasicAuthentication>();
      services.AddSingleton<IRepository, Repository>();
      services.AddSingleton<ICapabilityDatastore, CapabilitiesService>();
      services.AddSingleton<ICapabilitiesImplementedDatastore, CapabilitiesImplementedService>();
      services.AddSingleton<ICapabilitiesImplementedEvidenceDatastore, CapabilitiesImplementedEvidenceService>();
      services.AddSingleton<ICapabilitiesImplementedReviewsDatastore, CapabilitiesImplementedReviewsService>();
      services.AddSingleton<ICapabilityStandardDatastore, CapabilityStandardService>();
      services.AddSingleton<IContactsDatastore, ContactsService>();
      services.AddSingleton<IFrameworksDatastore, FrameworksService>();
      services.AddSingleton<ILinkManagerDatastore, LinkManagerService>();
      services.AddSingleton<IOrganisationsDatastore, OrganisationsService>();
      services.AddSingleton<ISolutionsDatastore, SolutionsService>();
      services.AddSingleton<ISolutionsExDatastore, SolutionExService>();
      services.AddSingleton<IStandardsDatastore, StandardsService>();
      services.AddSingleton<IStandardsApplicableDatastore, StandardsApplicableService>();
      services.AddSingleton<IStandardsApplicableEvidenceDatastore, StandardsApplicableEvidenceService>();
      services.AddSingleton<IStandardsApplicableReviewsDatastore, StandardsApplicableReviewsService>();
      services.AddSingleton<ITechnicalContactsDatastore, TechnicalContactService>();

      // Add framework services.
      services
        .AddMvc()
        // Add controllers as services so they'll be resolved.
        .AddControllersAsServices()
        .AddJsonOptions(opts =>
        {
          opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
          opts.SerializerSettings.Converters.Add(new StringEnumConverter
          {
            CamelCaseText = false
          });
        });


      if (_hostingEnv.IsDevelopment())
      {
        services
          .AddSwaggerGen(c =>
          {
            c.SwaggerDoc("v1.0", new Info
            {
              Version = "v1.0",
              Title = "Buying Catalog API",
              Description = "Buying Catalog API (ASP.NET Core 2.0)",
              Contact = new Contact()
              {
                Name = "Swagger Codegen Contributors",
                Url = "https://github.com/swagger-api/swagger-codegen",
                Email = ""
              },
              TermsOfService = ""
            });
            c.CustomSchemaIds(type => type.FriendlyId(true));
            c.DescribeAllEnumsAsStrings();
            c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
            // Sets the basePath property in the Swagger document generated
            c.DocumentFilter<BasePathFilter>("/sie");

            // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
            // Use [ValidateModelState] on Actions to actually validate it in C# as well!
            c.OperationFilter<GeneratePathParamsValidationFilter>();
          });
      }

      services.AddIdentityServer()
        // TODO : Depending on hosting this may need replacing with a cert (also probably better practice to handle instance restarts)
        // https://stackoverflow.com/questions/41572900/addtemporarysigningcredential-vs-addsigningcredential-in-identityserver4
        .AddDeveloperSigningCredential()
        .AddInMemoryApiResources(AuthConfig.GetApiResources())
        .AddInMemoryClients(AuthConfig.GetClients(Configuration));

      services.AddMvcCore()
        .AddAuthorization()
        .AddJsonFormatters();

      services
        .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
        .AddBasicAuthentication(
          options =>
          {
            options.Realm = "GIFBuyingCatalogue";
            options.Events = new BasicAuthenticationEvents
            {
              OnValidatePrincipal = context =>
              {
                var auth = ServiceProvider.GetService<IBasicAuthentication>();
                return auth.Authenticate(context);
              }
            };
          });

      services.AddAuthentication("Bearer")
        .AddIdentityServerAuthentication(options =>
        {
          options.Authority = Settings.GIF_AUTHORITY_URI(Configuration);
          options.RequireHttpsMetadata = false;

          options.ApiName = "GIFBuyingCatalogue";
        });
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="loggerFactory"></param>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      ServiceProvider = app.ApplicationServices;

      app.UseExceptionHandler(options =>
      {
        options.Run(async context =>
        {
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          context.Response.ContentType = "text/html";
          var ex = context.Features.Get<IExceptionHandlerFeature>();
          if (ex != null)
          {
            var logger = ServiceProvider.GetService<ILogger<Startup>>();
            var err = $"Error: {ex.Error.Message}{Environment.NewLine}{ex.Error.StackTrace }";
            logger.LogError(err);
            if (env.IsDevelopment())
            {
              await context.Response.WriteAsync(err).ConfigureAwait(false);
            }
          }
        });
      });

      app.UseIdentityServer();
      app.UseAuthentication();

      app
        .UseStaticFiles()
        .UseMvc()
        .UseDefaultFiles();


      if (env.IsDevelopment())
      {
        app
        .UseSwagger()
        .UseSwaggerUI(c =>
        {
          //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
          c.SwaggerEndpoint("/spec.json", "Buying Catalog API");

          //TODO: Or alternatively use the original Swagger contract that's included in the static files
          // c.SwaggerEndpoint("/swagger-original.json", "Buying Catalog API Original");

          c.DocExpansion(DocExpansion.None);
        })
        .UseDeveloperExceptionPage();
      }
    }

    private void DumpSettings()
    {
      Console.WriteLine("Settings:");
      Console.WriteLine($"  GIF:");
      Console.WriteLine($"    GIF_AUTHORITY_URI           : {Settings.GIF_AUTHORITY_URI(Configuration)}");
      Console.WriteLine($"    GIF_CRM_AUTHORITY           : {Settings.GIF_CRM_AUTHORITY(Configuration)}");
      Console.WriteLine($"    GIF_CRM_URL                 : {Settings.GIF_CRM_URL(Configuration)}");
      Console.WriteLine($"    GIF_AZURE_CLIENT_ID         : {Settings.GIF_AZURE_CLIENT_ID(Configuration)}");
      Console.WriteLine($"    GIF_ENCRYPTED_CLIENT_SECRET : {Settings.GIF_ENCRYPTED_CLIENT_SECRET(Configuration)}");

      Console.WriteLine($"  LOG:");
      Console.WriteLine($"    LOG_CONNECTIONSTRING : {Settings.LOG_CONNECTIONSTRING(Configuration)}");
      Console.WriteLine($"    LOG_CRM              : {Settings.LOG_CRM(Configuration)}");

      Console.WriteLine($"  CRM:");
      Console.WriteLine($"    CRM_CLIENTID            : {Settings.CRM_CLIENTID(Configuration)}");
      Console.WriteLine($"    CRM_CLIENTSECRET        : {Settings.CRM_CLIENTSECRET(Configuration)}");
    }
  }
}
