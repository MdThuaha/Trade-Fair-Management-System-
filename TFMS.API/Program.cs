using Microsoft.EntityFrameworkCore;

using TFMS.Lib.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FairDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db"), b=>b.MigrationsAssembly("TFMS.API")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCors",
                          policy =>
                          {
                              policy.WithOrigins("http://127.0.0.1:4200",
                                                  "http://localhost:4200",
                                                  "http://localhost:5173")
                              .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowAnyOrigin();
                          });
});
builder.Services.AddControllers().AddNewtonsoftJson(
    op =>
    {
        op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
        op.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
    }
    );

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("EnableCors");
app.MapDefaultControllerRoute();
app.Run();