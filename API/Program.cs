using API.Services;
using Infrastructure.Helper;
using Repository.IRepository;
using Repository.Repository;

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


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});


var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
