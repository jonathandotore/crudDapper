using CrudDapper.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//This service indicates to the IUserInterface that the implementation is contained on UserService class
builder.Services.AddScoped<IUserInterface, UserService>();

//Adding Mapper dependency injection in a whole assembly.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("UsersApp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("UsersApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
