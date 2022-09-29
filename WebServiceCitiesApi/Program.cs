using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebServiceCitiesApi;
using WebServiceCitiesApi.DBContext;
using WebServiceCitiesApi.Services;
using WebServiceCitiesApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//using Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("filenamn", rollingInterval: RollingInterval.Day)
    .CreateLogger();    

//creates automatically a set of logging providers
var builder = WebApplication.CreateBuilder(args);

//once per request
//builder.Services.AddScoped()  

//Created first time they are requested
//builder.Services.AddSingleton();

//for weightless stateless services
// from now this service can be injected
//this is my created service
//can be grey if in DEBUG mode
#if DEBUG
builder.Services.AddTransient < IMailService, MailService>();
#else
builder.Services.AddTransient < IMailService, CloudMailService>();  
#endif

//så här kan man man göra med static datakällor och sedan på samma sätt som ovan med constructor injection i controller klasserna
builder.Services.AddSingleton<CityDatastore>();

//scoped lifetime
//under utveckling spara connstring i appsettings
//production i lagra i System properties -> Environment variables->System variables
//miljön läser alltid från Systemvariables först, inte appsettingsfilerna
//Azure keyvault är dock säkrare
builder.Services.AddDbContext<LocalSQLContex>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["key in settingsfile"]));


//builder.Logging.ClearProviders();   
//builder.Logging.AddConsole();   like 

//attribute-based mapping is used in WEB api

// Add services to the container.

//sufficient if only building a dataAPI. AddMvc is when building sites
builder.Services.AddControllers(options => { 
    //options.InputFormatters NOTE! first added formatter in list will be the default
    options.ReturnHttpNotAcceptable = true; })
    .AddNewtonsoftJson()   //, replaces the default json serializer, must import Nuget packages first
    .AddXmlDataContractSerializerFormatters();// returns 406 Not Acceptable if requesting fx application/xml is in
                                               // header and the service does not support that outputformat.
                                               // also adding support for in/output xml

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();  //built in service for managing, fx returning a pdf so the client can open it
                                                                    //injects a singelton lifetime provider, just imported once, singleton lifetime
                                                                    //must be injected in FileController
builder.Services.AddScoped<ICityInfoRepository,CityRepository>();// scoped is once per request

//setting up the bearertoken authentication
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))

    };

}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();   //enables endpointrouting

//add this so the authenticationcontroller is available in the request pipeline
//viktigt att denna kommer före nästa rad, först authenticera, sedan auktorisera
app.UseAuthentication();


//nedan kan även implementera AddPolicy
app.UseAuthorization();

//app.MapControllers(); //do not use when using endpoint, use below instead
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

//last in pipeline
app.Run(async (contex) =>
{
    await contex.Response.WriteAsync("Henriks första wepAPI");
});

app.Run();
