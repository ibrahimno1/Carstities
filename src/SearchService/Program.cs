var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

await DB.initAsync("SearchDB", MongoClientSettings
    .FromConnectionString(builder.Configuration.GetConnectionString("MongoDBConnection")));

await DB.Index<Item>()
    .key(x => x.Make, KeyType.Text)
    .key(x => x.Model, KeyType.Text)
    .key(x => x.Color, KeyType.Text)
    .CreateAsync();


app.Run();
