using Microsoft.IdentityModel.Tokens;

namespace Autorizacion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    string key = Environment.GetEnvironmentVariable("JWT_KEY");

                    options.TokenValidationParameters = new TokenValidationParameters();
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
