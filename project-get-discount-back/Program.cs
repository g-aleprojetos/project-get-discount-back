using project_get_discount_back._1_Domain.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigDatabase(builder.Configuration);
builder.Services.ConfigMediatR();
builder.Services.ConfigEndPonts();
builder.Services.ConfigControlles();
builder.Services.ConfigSwagger();
builder.Services.ConfigServices();
builder.Services.ConfigAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
