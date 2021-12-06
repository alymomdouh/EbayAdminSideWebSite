using EbayAdminDbContext;
using EbayAdminModels.Category;
using EbayAdminModels.SubCategory;
using EbayAdminRepository.Brands;
using EbayAdminRepository.Category;
using EbayAdminRepository.Comments;
using EbayAdminRepository.Offers;
using EbayAdminRepository.Orders;
using EbayAdminRepository.Products;
using EbayAdminRepository.Rates;
using EbayAdminRepository.Shippers;
using EbayAdminRepository.Stocks;
using EbayAdminRepository.SubCategory;
using EbayAdminRepository.WatchLists;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;

namespace EbayView
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IWatchListRepository, WatchListRepository>();
            services.AddTransient<IRateRepository, RateRepository>();
            services.AddTransient<IShipperRepository, ShipperRepository>();
            services.AddTransient<IOfferRepository, OfferRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();


            services.AddControllersWithViews();
            services.AddDbContext<myDbContext>
                (options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // Products   Home  Categories
            });
        }

    }
}
