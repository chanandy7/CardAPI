//using Cards.Core;
using Cards.DB;


using Cards.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_CONNECTION_STRING")));






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//conficting name of Card- soln
builder.Services.AddSwaggerGen(Options =>
{
    Options.CustomSchemaIds(x => x.FullName);
});


builder.Services.AddHttpClient();



builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<ICardsServices, CardsServices>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();


//post-postman run 1
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();



var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret))
        };
    });




var app = builder.Build();



//for deployment
//get the appDbContext instance using DI
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    dbContext.Database.Migrate();
}






// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();



app.UseAuthentication();
app.UseAuthorization();

//app.UseCors(policy =>
//{

//    policy.WithOrigins("http://localhost:3000")
//    .AllowCredentials()
//    .AllowAnyHeader()
//    .AllowAnyMethod();
//}
//    );

app.UseCors(policy => {
    policy.SetIsOriginAllowed(_ => true)
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
});




app.MapControllers();

app.Run();
