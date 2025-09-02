using Microsoft.EntityFrameworkCore;
using Restaurang_luna.Data;
using Restaurang_luna.Extensions;
using Restaurang_luna.Models;
using Restaurang_luna.ServiceInterface.Auth;
using Restaurang_luna.ServiceInterface.Customers;
using Restaurang_luna.ServiceInterface.Resturant;
using Restaurang_luna.DTOs.Booking.Other;
using System.Text.Json;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.JSInterop;
using Restaurang_luna.Extensions.Mappers;

namespace Restaurang_luna
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //        var hasher = new PasswordHasher();
            //        var hash = hasher.Hash("test123");
            //        Console.WriteLine(hash);

            //        bool ok = hasher.Verify(
            //"G/ZurCUC4QMRDqXwZwQRsA==;ULAG2GRXijaJasIKRhSJUTZtZQGTsAm3UTjPE7xsEBY=",
            //"test123");
            //        Console.WriteLine(ok);


            // Add services to the container.

            builder.Services.AddDbContext<LunaDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var cfg = builder.Configuration;

            builder.Services.AddJwtAuthentication(builder.Configuration);


            builder.Services.AddAuthorization();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    
                    options.JsonSerializerOptions.Converters.Add(
                        new System.Text.Json.Serialization.JsonStringEnumConverter(
                        System.Text.Json.JsonNamingPolicy.CamelCase,
                         allowIntegerValues: false));

                    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverterExtension());
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.MapType<BookingBucket>(() =>
                {
                    var names = Enum.GetNames(typeof(BookingBucket))
                    .Select(n => (IOpenApiAny)new OpenApiString(n))
                    .ToList();

                    return new OpenApiSchema
                    {
                        Type = "string",
                        Enum = names
                    };
                });
                c.MapType<DateTimeOffset>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date-time",
                    Example = new OpenApiString("2025-08-27 16:00")
                });

                c.MapType<DateTimeOffset?>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date-time",
                    Nullable = true,
                    Example = new OpenApiString("2025-08-27 16:00")
                });
            });


            //Setting scrope for service/interface
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
            builder.Services.AddSingleton<IBookingPolicy, BookingPolicy>();
            builder.Services.AddSingleton<IMapper<Booking, BookingListDto>, BookingMapper>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseCors("AllowReactApp");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
