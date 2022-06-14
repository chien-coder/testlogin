using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using UserAPI_NetCore6.DataConfig;
using UserAPI_NetCore6.Models;
using UserAPI_NetCore6.Services;
using UserAPI_NetCore6.Validation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddTransient<IValidator<User>, UserValidation>();

builder.Services.AddControllers()
    .AddFluentValidation(c =>
    {
        //c.DisableDataAnnotationsValidation = true;

        c.ImplicitlyValidateChildProperties = true;
        c.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

    });
    //.AddJsonOptions(op =>
    //{
    //    op.JsonSerializerOptions.PropertyNamingPolicy = null;
    //}
    //);
    
//builder.Services.AddValidatorsFromAssemblyContaining<UserValidation>();
builder.Services.AddDbContext<MyDbContext>(op =>
{  
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IUserManager, UserManager>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://192.168.1.105:8080").AllowCredentials());
});

//c =>
//{
//    c.SwaggerDoc("V1", new OpenApiInfo { Title = "UserAPI_NetCore6", Version = "V1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Jwt Authorization",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] { }
//        }
//    });

//}

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
//{
//    op.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt: Issuer"],
//        ValidAudience = builder.Configuration["Jwt: Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt: Key"]))
//    };
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//c => c.SwaggerEndpoint("./swagger/v1/swagger.json", "userAPI_NetCore6 v1")

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://192.168.1.105:8080").AllowCredentials());
app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
