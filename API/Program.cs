using API.Middleware;
using Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepository;
using Repository.Repository;
using Service.BackgroundServices;
using Service.Implementations;
using Service.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobDescriptionRepository, JobDescriptionRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IMedicineDepartmentRepository, MedicineDepartmentRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IAssignUsersToRoleRepository, AssignUsersToRoleRepository>();
builder.Services.AddScoped<IModuleRoleRepository, ModuleRoleRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<IInvoiceMasterRepository, InvoiceMasterRepository>();
builder.Services.AddScoped<IInvoiceDetailsRepository, InvoiceDetailsRepository>();
builder.Services.AddScoped<IErrorLogService, ErrorLogService>();

builder.Services.AddHostedService<SessionCleanupService>();
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<IJwtService>(provider => new JwtService("ASP.NET_CORE(MVC&API)PharmacyManagementSystem"));


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {   
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5039",
            ValidAudience = "http://localhost:5130",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASP.NET_CORE(MVC&API)PharmacyManagementSystem"))
        };
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});


var app = builder.Build();

app.UseCors("AllowAll");

app.UseMiddleware<SessionValidationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
