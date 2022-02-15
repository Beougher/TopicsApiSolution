var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuration of the backing services...
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ILookupOnCallDevelopers, FakeDeveloperLookup>();

builder.Services.AddScoped<IProvideTopicsData, EfSqlTopicsData>();
// The TopicsDataContext is set up as a Scoped service.  You can inject it into your controllers, services and stuff.
builder.Services.AddDbContext<TopicsDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("topics"));
});

// Building the actual application
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
