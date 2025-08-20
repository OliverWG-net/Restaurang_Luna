
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resturang_luna.Data;
using Resturang_luna.ServiceInterface.Auth;

namespace Resturang_luna
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var hasher = new PasswordHasher();
            var hash = hasher.Hash("test123");
            Console.WriteLine(hash);

            bool ok = hasher.Verify(
    "G/ZurCUC4QMRDqXwZwQRsA==;ULAG2GRXijaJasIKRhSJUTZtZQGTsAm3UTjPE7xsEBY=",
    "test123");
            Console.WriteLine(ok); 


            // Add services to the container.

            builder.Services.AddDbContext<LunaDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}
