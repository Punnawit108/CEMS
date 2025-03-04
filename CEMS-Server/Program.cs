using System.IO; // เพิ่ม namespace นี้
using System.Text;
using CEMS_Server.AppContext;
using CEMS_Server.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders; // เพิ่ม namespace นี้
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ตั้งค่าลิขสิทธิ์ด้วย LicenseType
QuestPDF.Settings.License = LicenseType.Community; // ใช้ LicenseType.Community แทน string

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "CEMS-WEBSITE",
            Version = "v1.0.0",
            Description =
                "An API for the CEMS Website, providing endpoints for managing content and services.",
        }
    );
});

// Add SignalR service
builder.Services.AddSignalR();
builder.Services.AddScoped<GetDataExport>();
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<PdfServiceProject>();
builder.Services.AddScoped<DetailService>();

// ตั้งค่า CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowSpecificOrigin",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") // กำหนด URL ที่อนุญาต
                .AllowAnyHeader() // อนุญาตทุก header
                .AllowAnyMethod() // อนุญาตทุก method (GET, POST, PUT, DELETE)
                .AllowCredentials();
        }
    );
});

// ตั้งค่า MySQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString != null)
{
    builder.Services.AddDbContext<CemsContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    );
}

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// ใช้ CORS ก่อนการตั้งค่าอื่น ๆ
app.UseCors("AllowSpecificOrigin"); // ใช้ policy ที่ได้ตั้งไว้

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ตั้งค่า Static Files Middleware สำหรับโฟลเดอร์ Assets/Upload
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Upload")
        ),
        RequestPath = "/Assets/Upload",
    }
);

app.UseAuthentication();
app.UseAuthorization();

// Map SignalR hub
app.MapHub<NotificationHub>("/notificationHub");

app.MapControllers();

app.Run();
