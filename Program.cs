using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces;
using WarehouseAPI.Domain.Interfaces.Repositories;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Infra;
using WarehouseAPI.Infra.Repositories;
using WarehouseAPI.Services;
using WarehouseAPI.Services.AppServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Cors

var sourceAccessAllowed = "_SourceAccessAllowed";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: sourceAccessAllowed,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WarehouseApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Jwt
builder.Services.AddAuthorization();

// Identity DB
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

string NgSqlConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("String of connection not found!");

builder.Services.AddDbContext<AppDbContext>(options=>
{
    options.UseNpgsql(NgSqlConnection);
});

// Configuração do jwt

var secreyKey = builder.Configuration["JWT:SecretKey"]
    ?? throw new ArgumentException("Invalid secret key!");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreyKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));


    //options.AddPolicy("AdminOnly", policy => policy.RequireRole("User"));

    //options.AddPolicy("SuperAdminOnly", policy =>
    //                            policy.RequireRole("Admin").RequireClaim("id", "octaciano"));
    
    //options.AddPolicy("ExclusivePolicyOnly", policy =>
    //    policy.RequireAssertion(context =>
    //    context.User.HasClaim(claim =>
    //                    claim.Type == "id" && claim.Value == "octaciano")
    //                    || context.User.IsInRole("SuperAdmin")));
});


// Camada de serviço (Em breve)
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IAssetTypeService, AssetTypeService>();
builder.Services.AddScoped<IInstitutionalAssetService, InstitutionalAssetService>();
builder.Services.AddScoped<ILocationTypeService, LocationTypeService>();

// Camada de repository
builder.Services.AddTransient<IRepository<Company>, Repository<Company>>();
builder.Services.AddTransient<ILocationRepository, LocationRepository>();
builder.Services.AddTransient<IAssetTypeRepository, AssetTypeRepository>();
builder.Services.AddTransient<IInstitutionalAssetRepository, InstitutionalAssetRepository>();
builder.Services.AddTransient<IRepository<LocationType>, Repository<LocationType>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
app.UseAuthorization();

app.UseCors(sourceAccessAllowed);

app.MapControllers();
app.Run();
