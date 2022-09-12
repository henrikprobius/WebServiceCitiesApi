using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

//attribute-based mapping is used in WEB api

// Add services to the container.

//sufficient if only building a dataAPI. AddMvc is when building sites
builder.Services.AddControllers(options => { 
    //options.InputFormatters NOTE! first added formatter in list will be the default
    options.ReturnHttpNotAcceptable = true; }).AddXmlDataContractSerializerFormatters();// returns 406 Not Acceptable if requesting fx application/xml is in
                                               // header and the service does not support that outputformat.
                                               // also adding support for in/output xml

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();  //built in service for managing, fx returning a pdf so the client can open it
                                                                    //injects a singelton lifetime provider, just imported once, singleton lifetime
                                                                    //must be injected in FileController

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();   //enables endpointrouting

app.UseAuthorization();

//app.MapControllers(); //do not use when using endpoint, use below instead
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

//last in pipeline
app.Run(async (contex) =>
{
    await contex.Response.WriteAsync("Henriks första wepAPI");
});

app.Run();
