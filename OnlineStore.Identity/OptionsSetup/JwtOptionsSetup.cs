using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OnlineStore.Identity.Models;

namespace OnlineStore.Identity.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private const string SECTION_NAME = "JwtOptions"; 
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SECTION_NAME).Bind(options);
        }
    }
}
