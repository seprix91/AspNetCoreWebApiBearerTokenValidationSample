using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
namespace AspNetCoreWebApiBearerTokenValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            
                WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>().UseUrls("http://localhost:9000")
                    .Build()
                    .Run();
            
        }
    }
}
