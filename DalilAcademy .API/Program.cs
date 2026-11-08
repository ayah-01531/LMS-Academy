using DalilAcademy.API.Middleware;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Security;
using DalilAcademy.Core.Service;
using DalilAcademy.Core.Validators;
using DalilAcademy.Infra.Data;
using DalilAcademy.Infra.Repository;
using DalilAcademy.Infra.Security;
using DalilAcademy.Infra.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); // Important for minimal APIs + Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Dalil Academy LMS API",
        Version = "v1",
        Description = "Learning Management System API built with ASP.NET Core 10"
    });
});
// Repositories


builder.Services.AddScoped<ICourseRepository, CourseRepositry>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



// Services

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IAuthServices, AuthServices>();



// FluentValidation — one line registers every validator in Core
builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseDtoValidator>();//Registers every validater

//  global error handling with ProblemDetails
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();



builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//JWT Security

var jwt = builder.Configuration.GetSection("Jwt").Get<JWTsetting>();//بتم عملية الدمج مع ال key
builder.Services.Configure<JWTsetting>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // [Authorize] uses JWT by default
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,//هل السيرفر انشاها
        ValidateAudience = true,//هل اصدرها لهاد التطبيق
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key)),//بحول من string ->byte لان ال hashing ما بشتغل على string 
        ClockSkew = TimeSpan.FromMinutes(1) // default is 5 minutes!
    };
});
builder.Services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();//لان ما عندي شي بتغير (نسخة وحودة بتكفي)

builder.Services.AddSingleton<IToken_Service, JwtTokenService>();//لان ما عندي شي بتغير (نسخة وحودة بتكفي)





//register for serveses
//-------------------------------------------------
var app = builder.Build();
//------------------------------------------------------------
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dalil Academy API v1");
        options.RoutePrefix = string.Empty; // Swagger at root
    });
}
//لما ال requestيوصل شو اعمل
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//-------------Correct Order-------------------------
//بحول اي http request ->https
app.UseHttpsRedirection();//middleware

app.UseAuthentication();
app.UseAuthorization();
//بوصل ال request للcontrillersالمناسب
app.MapControllers();

app.Run();
