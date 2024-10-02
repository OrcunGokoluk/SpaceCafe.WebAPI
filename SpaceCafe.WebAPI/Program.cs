using SpaceCafe.Application;
using SpaceCafe.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddProblemDetails(options =>
    {
        options.CustomizeProblemDetails = context =>
        {
            context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
            context.ProblemDetails.Extensions["asdasdsa"] = "gdfafsa";
        };
    });



    builder.Services.AddApplication();


    builder.Services.AddInfrastructure(builder.Configuration);


    builder.Services.AddControllers();


    //builder.Services.AddSingleton<ProblemDetailsFactory, SpaceCafeProblemDetailsFactory>();

}


var app = builder.Build();
{
    //Error Section
    app.UseExceptionHandler("/error");
    //Section
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();

}
