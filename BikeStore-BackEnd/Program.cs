
using BikeStore_BackEnd.Data;
using BikeStore_BackEnd.Iservices.ServicesImpl;
using BikeStore_BackEnd.Iservices;
using Microsoft.EntityFrameworkCore;
using BikeStore_BackEnd.Services;
using BikeStore_BackEnd.IServices;
using BikeStore_BackEnd.AutoMapperProfiles;

namespace BikeStore_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<BikeApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program)); // Assuming your profiles are in the same assembly
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(UserProfile)); // Register AutoMapper
            

            // Register services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();  

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
