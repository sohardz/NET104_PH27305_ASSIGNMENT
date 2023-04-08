using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IRoleServices, RoleServices>();
builder.Services.AddTransient<ICartServices, CartServices>();
builder.Services.AddTransient<ICartDetailServices, CartDetailServices>();
builder.Services.AddTransient<IBillServices, BillServices>();
builder.Services.AddTransient<IBillDetailServices, BillDetailServices>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
