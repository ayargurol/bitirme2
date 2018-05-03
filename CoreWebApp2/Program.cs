using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CoreWebApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args: args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args: args)
                .UseStartup<Startup>()
                .Build();
    }
}
