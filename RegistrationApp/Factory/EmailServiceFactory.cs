﻿using RegistrationApp.Services.EmailServices;

namespace RegistrationApp.Factory
{
    public class EmailServiceFactory
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _provider;

        public EmailServiceFactory(IConfiguration config, IServiceProvider provider)
        {
            _config = config;
            _provider = provider;
        }

        public IEmailService GetEmailService()
        {
            var providerType = _config["EmailProvider"];

            return providerType switch
            {
                "Gmail" => _provider.GetRequiredService<LegacyGmailEmailService>(),
                _ => throw new Exception("Invalid Email Provider")
            };
        }
    }
}
