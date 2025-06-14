using System.Text.Json;
using System.Text.Json.Nodes;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CustomUIConfig;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            // Core
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");

            // Display
            c.DefaultModelExpandDepth(2);
            c.DefaultModelRendering(ModelRendering.Model);
            c.DefaultModelsExpandDepth(-1);
            c.DisplayOperationId();
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.EnableFilter();
            c.ShowExtensions();

            // Network
            c.EnableValidator();
            c.SupportedSubmitMethods(SubmitMethod.Get);

            // Other
            c.DocumentTitle = "CustomUIConfig";
            c.StylesPath = "/ext/custom-stylesheet.css";
            c.InjectStylesheet("/ext/custom-stylesheet.css");
            c.ScriptBundlePath = "/ext/custom-javascript.js";
            c.ScriptPresetsPath = "/ext/custom-javascript.js";
            c.InjectJavascript("/ext/custom-javascript.js");
            c.InjectJavascript("/ext/custom-plugin1.js");
            c.InjectJavascript("/ext/custom-plugin2.js");
            c.UseRequestInterceptor("(req) => { req.headers['x-my-custom-header'] = 'MyCustomValue'; return req; }");
            c.UseResponseInterceptor("(res) => { console.log('Custom interceptor intercepted response from:', res.url); return res; }");
            c.EnablePersistAuthorization();

            c.ConfigObject.Plugins = ["customPlugin1", "customPlugin2"];

            c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
            c.ConfigObject.AdditionalItems.Add("charProperty", 'c');
            c.ConfigObject.AdditionalItems.Add("stringProperty", "value");
            c.ConfigObject.AdditionalItems.Add("byteProperty", (byte)1);
            c.ConfigObject.AdditionalItems.Add("signedByteProperty", (sbyte)1);
            c.ConfigObject.AdditionalItems.Add("int16Property", (short)1);
            c.ConfigObject.AdditionalItems.Add("uint16Property", (ushort)1);
            c.ConfigObject.AdditionalItems.Add("int32Property", 1);
            c.ConfigObject.AdditionalItems.Add("uint32Property", 1u);
            c.ConfigObject.AdditionalItems.Add("int64Property", 1L);
            c.ConfigObject.AdditionalItems.Add("uint64Property", 1uL);
            c.ConfigObject.AdditionalItems.Add("floatProperty", 1f);
            c.ConfigObject.AdditionalItems.Add("doubleProperty", 1d);
            c.ConfigObject.AdditionalItems.Add("decimalProperty", 1m);
            c.ConfigObject.AdditionalItems.Add("dateTimeProperty", DateTime.UtcNow);
            c.ConfigObject.AdditionalItems.Add("dateTimeOffsetProperty", DateTimeOffset.UtcNow);
            c.ConfigObject.AdditionalItems.Add("timeSpanProperty", new TimeSpan(12, 34, 56));
            c.ConfigObject.AdditionalItems.Add("jsonArray", new JsonArray() { "string" });
            c.ConfigObject.AdditionalItems.Add("jsonObject", new JsonObject() { ["foo"] = "bar" });
            c.ConfigObject.AdditionalItems.Add("jsonDocument", JsonDocument.Parse("""{ "foo": "bar" }"""));
            c.ConfigObject.AdditionalItems.Add("dateOnlyProperty", new DateOnly(1977, 05, 25));
            c.ConfigObject.AdditionalItems.Add("timeOnlyProperty", new TimeOnly(12, 34, 56));
            c.ConfigObject.AdditionalItems.Add("halfProperty", Half.CreateChecked(1));
            c.ConfigObject.AdditionalItems.Add("int128Property", Int128.CreateChecked(1));
            c.ConfigObject.AdditionalItems.Add("unt128Property", UInt128.CreateChecked(1));
        });
    }
}
