
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Carter is a library for mapping the objects
builder.Services.AddCarter();

// MeidatR is library that help us to implement CQRS design pattern
builder.Services.AddMediatR(
    config =>
    {
        config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

// This is for fluent validation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Marten is a library for converting Postgres from regular db to documentDb
builder.Services.AddMarten(
        opts =>
        {
            opts.Connection(builder.Configuration.GetConnectionString("Database")!);

        }
        ).UseLightweightSessions();


// Add exception handler from buildingblocks

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
