using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreWebApiBearerTokenValidation
{
    public class Startup
    {
        private readonly string x509PublicCert =
            @"MIIFPDCCBCSgAwIBAgIQfSccbZe9B94AAAAAUNwFODANBgkqhkiG9w0BAQsFADCBXXXXXXXXXXXXXXXXXXXXXXXXXX";
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // var certString = File.ReadAllText("PublicKey.pem"); //public key X509 certificate "-----BEGIN CERTIFICATE----- your cert -----END CERTIFICATE-----"
            var byteCert = Convert.FromBase64String(x509PublicCert);
            var x509Cert = new X509Certificate2(byteCert);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = Configuration["Audience"]; //"http://localhost:9000/";
                    options.Authority = Configuration["Authority"]; // "https://idp.yourcompany.com:5000/";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validate the JWT Audience
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new X509SecurityKey(x509Cert),
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Issuer"], //idp.yourcompany.com
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        // If you want to allow a certain amount of clock drift, set that here:
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}