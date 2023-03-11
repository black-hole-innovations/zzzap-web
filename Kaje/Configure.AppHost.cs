using Funq;
using ServiceStack;
using Kaje.ServiceInterface;
using Kaje.ServiceModel;

[assembly: HostingStartup(typeof(Kaje.AppHost))]

namespace Kaje;

public class AppHost : AppHostBase, IHostingStartup
{
    public AppHost() : base("Kaje", typeof(MyServices).Assembly) { }

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
        });

        Plugins.Add(new CorsFeature(allowedHeaders: "Content-Type,Authorization",
            allowOriginWhitelist: new[]{
            "http://localhost:5000",
            "https://localhost:5001",
            "https://" + Environment.GetEnvironmentVariable("DEPLOY_CDN")
        }, allowCredentials: true));

        KajeClient kj = new KajeClient();
        //var response = kj.DataPush<KajeItem>("@sbworld", "address", new KajeItem() { Data = new Item() { City = "test", State = "TX", Address = "abc", PostalCode = "75001" } }).Result;
        //var response2 = kj.DataFind<KajeItem>("@sbworld").Result;

    }

    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => 
            services.ConfigureNonBreakingSameSiteCookies(context.HostingEnvironment));
}
