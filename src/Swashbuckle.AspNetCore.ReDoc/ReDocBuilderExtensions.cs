﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.ReDoc;

namespace Microsoft.AspNetCore.Builder;

public static class ReDocBuilderExtensions
{
    /// <summary>
    /// Register the Redoc middleware with provided options
    /// </summary>
    public static IApplicationBuilder UseReDoc(this IApplicationBuilder app, ReDocOptions options)
    {
        return app.UseMiddleware<ReDocMiddleware>(options);
    }

    /// <summary>
    /// Register the Redoc middleware with optional setup action for DI-injected options
    /// </summary>
    public static IApplicationBuilder UseReDoc(
        this IApplicationBuilder app,
        Action<ReDocOptions> setupAction = null)
    {
        ReDocOptions options;
        using (var scope = app.ApplicationServices.CreateScope())
        {
            options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<ReDocOptions>>().Value;
            setupAction?.Invoke(options);
        }

        // To simplify the common case, use a default that will work with the SwaggerMiddleware defaults
        options.SpecUrl ??= "../swagger/v1/swagger.json";

        return app.UseReDoc(options);
    }
}
