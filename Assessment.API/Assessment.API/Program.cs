using Assessment.DataAccess;
using Assessment.DataAccess.ApiKey;
using Assessment.DataAccess.ApiKey.IApiKey;
using Assessment.DataAccess.ApiKey.IApiKey.IApiKey;
using Assessment.DataAccess.Data.Repository;
using Assessment.DataAccess.Data.Repository.IRepository;
using TerevintoSoftware.AspNetCore.Authentication.ApiKeys;
using TerevintoSoftware.AspNetCore.Authentication.ApiKeys.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Assessment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
          
            builder.Services.AddSwaggerGen(setup =>
            {
                setup.AddApiKeySupport();
            });
            builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services
        .AddDefaultApiKeyGenerator(new ApiKeyGenerationOptions
        {
            KeyPrefix = "Assessment-",
            GenerateUrlSafeKeys = true,
            LengthOfKey = 36
        })
        .AddDefaultClaimsPrincipalFactory()
        .AddApiKeys(options => { options.InvalidApiKeyLog = (LogLevel.Warning, "Someone attempted to use an invalid API Key: {ApiKey}"); }, true)
        .AddSingleton<IClientsService, InMemoryClientsService>()
        .AddMemoryCache()
        .AddSingleton<IApiKeysCacheService, CacheService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

             app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
