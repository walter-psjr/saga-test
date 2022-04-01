using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Secondary.Api.Mappings;
using Secondary.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISessionFactory>(serviceProvider =>
{
    var configuration = new Configuration();
    configuration.DataBaseIntegration(db =>
    {
        db.ConnectionString = "Data Source=SecondaryApi.db";
        db.Driver<SQLite20Driver>();
        db.Dialect<SQLiteDialect>();
        db.LogSqlInConsole = true;
        db.LogFormattedSql = true;
    });

    var modelMapper = new ModelMapper();
    modelMapper.AddMapping<PersonMapping>();

    var mappings = modelMapper.CompileMappingForAllExplicitlyAddedEntities();

    configuration.AddMapping(mappings);

    var sessionFactory = configuration.BuildSessionFactory();

    var schemaUpdate = new SchemaUpdate(configuration);
    schemaUpdate.Execute(true, true);

    return sessionFactory;
});

builder.Services.AddScoped<NHibernate.ISession>(serviceProvider =>
{
    var sessionFactory = serviceProvider.GetService<ISessionFactory>();
    var session = sessionFactory.OpenSession();

    return session;
});

builder.Services.AddScoped<PersonService>();

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
