using System.ComponentModel;
using System.Net;
using Application.Models.ComponentModel;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

// https://nblumhardt.com/2020/10/bootstrap-logger/#introducing-createbootstraplogger
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information($"Application starting at {DateTime.Now:o} ...");
Log.Information($"Serilog-logger-type: {Log.Logger.GetType()}. Remove this log in the real world."); // Remove this row. Just for this lab.

try
{
	var builder = WebApplication.CreateBuilder(args);

	// https://nblumhardt.com/2020/10/bootstrap-logger/#introducing-createbootstraplogger
	builder.Host.UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) =>
	{
		loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
		loggerConfiguration.ReadFrom.Services(serviceProvider);
	});

	TypeDescriptor.AddAttributes(typeof(IPAddress), new TypeConverterAttribute(typeof(IpAddressTypeConverter)));
	TypeDescriptor.AddAttributes(typeof(IPNetwork), new TypeConverterAttribute(typeof(IpNetworkTypeConverter)));

	builder.Services.Configure<ForwardedHeadersOptions>(options =>
	{
		options.AllowedHosts.Clear();
		options.KnownNetworks.Clear();
		options.KnownProxies.Clear();
	});

	var forwardedHeadersSection = builder.Configuration.GetSection("ForwardedHeaders");
	builder.Services.Configure<ForwardedHeadersOptions>(forwardedHeadersSection);

	builder.Services.AddControllersWithViews();

	var application = builder.Build();

	application
		.UseDeveloperExceptionPage()
		.UseForwardedHeaders()
		.UseStaticFiles()
		.UseRouting()
		.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapDefaultControllerRoute());

	//throw new InvalidOperationException("Test");

	application.Run();
}
catch(Exception exception)
{
	Log.Fatal(exception, "Unhandled exception during application startup.");
	Log.Information($"Serilog-logger-type: {Log.Logger.GetType()}. Remove this log in the real world."); // Remove this row. Just for this lab.
}
finally
{
	Log.Information($"Application stopping at {DateTime.Now:o} ...");
	Log.Information($"Serilog-logger-type: {Log.Logger.GetType()}. Remove this log in the real world."); // Remove this row. Just for this lab.
	Log.CloseAndFlush();
}