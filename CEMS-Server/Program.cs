using QuestPDF.Infrastructure;
using CEMS_Server.AppContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ตั้งค่าลิขสิทธิ์ด้วย LicenseType
QuestPDF.Settings.License = LicenseType.Community; // ใช้ LicenseType.Community แทน string

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<GetDataExport>();
builder.Services.AddScoped<PdfService>();

// ตั้งค่า CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // กำหนด URL ที่อนุญาต
              .AllowAnyHeader() // อนุญาตทุก header
              .AllowAnyMethod(); // อนุญาตทุก method (GET, POST, PUT, DELETE)
    });
});

// ตั้งค่า MySQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString != null)
{
    builder.Services.AddDbContext<CemsContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

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

app.UseAuthorization();

app.MapControllers();

app.Run();
