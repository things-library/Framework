// ================================================================================
// <copyright file="IServiceCollection.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
//    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ThingsLibrary.Schema.Library;
using ThingsLibrary.Services.Extensions;

namespace ThingsLibrary.Notification.Azure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private const string ADDRESS_FROM = "address_from";

        /// <summary>
        /// Register SMS Notification Services
        /// </summary>
        /// <param name="services"></param>
        public static void AddSmsAzureService(this IServiceCollection services, SmsServiceOptions options)
        {
            services.AddSingleton(options);

            services.AddScoped<SmsService>();
        }

        /// <summary>
        /// Register SMS Notification Services
        /// </summary>
        /// <param name="services"></param>
        public static void AddSmsAzureService(this IServiceCollection services, ItemDto options, IConfiguration configuration)// EmailServiceOptions options)
        {
            var emailConnectionString = configuration.TryGetConnectionString(options["connection_string_variable"] ?? string.Empty);
            
            var smsOptions = new SmsServiceOptions(emailConnectionString, options[ADDRESS_FROM] ?? string.Empty);

            services.AddSmsAzureService(smsOptions);            
        }


        /// <summary>
        /// Register Email Notification Services
        /// </summary>
        /// <param name="services"></param>
        public static void AddEmailAzureService(this IServiceCollection services, EmailServiceOptions options)
        {                        
            services.AddSingleton(options);

            services.AddScoped<EmailNotificationService>();
        }


        /// <summary>
        /// Register Email Notification Services
        /// </summary>
        /// <param name="services"></param>
        public static void AddEmailAzureService(this IServiceCollection services, ItemDto options, IConfiguration configuration)// EmailServiceOptions options)
        {
            var emailConnectionString = configuration.TryGetConnectionString(options["connection_string_variable"] ?? string.Empty);

            var emailOptions = new EmailServiceOptions(emailConnectionString, options[ADDRESS_FROM] ?? string.Empty);

            services.AddSingleton(emailOptions);

            services.AddScoped<EmailNotificationService>();
        }
    }
}
