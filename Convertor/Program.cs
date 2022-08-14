using Convertor;
using Microsoft.EntityFrameworkCore;
using Convertor.Models;
using Convertor.Services;
using Convertor.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ConvertorContext>(opt =>
    opt.UseInMemoryDatabase("ConvertorDocuments"));
builder.Services.AddTransient<JsonConvertorService>();
builder.Services.AddTransient<XmlConvertorService>();
builder.Services.AddSingleton<IConvertorFactory>(s =>
{
    var convertors = new Dictionary<Type, Func<ConvertorService>>()
    {
        [typeof(XmlDoc)] = () => s.GetService<XmlConvertorService>(),
        [typeof(JsonDoc)] = () => s.GetService<JsonConvertorService>(),
    };

    return new ConvertorFactory(convertors);
});
builder.Services.AddScoped<IDocumentService,DocumentService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MailSettings>(
    builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

var app = builder.Build();

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
