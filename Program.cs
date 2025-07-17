using BoldChainBackendAPI.BoldChainConfiguration;
using BoldChainBackendAPI.BoldChainData;
using BoldChainBackendAPI.BoldChainInterface;
using BoldChainBackendAPI.BoldChainModel.BoldChainEntities;
using BoldChainBackendAPI.BoldChainService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<EthereumSetting>(builder.Configuration.GetSection("BlockChain"));
builder.Services.Configure<ContractAddress>(builder.Configuration.GetSection("ContractAddress"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<PinataSettings>(builder.Configuration.GetSection("PinataSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


builder.Services.AddDbContext<BoldChainContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<BoldChainContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))


    };
});
// Swagger config
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BoldChain API",
        Version = "v1",
        Description = "Secure Messaging and Identity System powered by Blockchain",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "BoldChain Dev Team",
            Email = "support@boldchain.io"
        }
    });

    // Add JWT Bearer auth to Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

//Services
builder.Services.AddScoped<IIdentityRegistryService,IdentityRegistryService>();
builder.Services.AddScoped<ITrustStampService, TrustStampService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMessageRegistryService, MessageRegistryService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDomainVerifierService, DomainVerifierService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPinataService, PinataService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IKeyService, KeyService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<ICanonicalizationService, CanonicalizationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBoldIdentity,BoldIdentity>();
builder.Services.AddScoped<IUserService, UserService>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "BoldChain API v1");
        options.RoutePrefix = string.Empty; // Swagger UI at root
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
