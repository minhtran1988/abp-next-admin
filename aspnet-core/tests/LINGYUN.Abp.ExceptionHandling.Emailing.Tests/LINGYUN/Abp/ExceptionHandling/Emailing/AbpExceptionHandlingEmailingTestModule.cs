using LINGYUN.Abp.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Encryption;

namespace LINGYUN.Abp.ExceptionHandling.Emailing
{
    [DependsOn(
        typeof(AbpEmailingExceptionHandlingModule),
        typeof(AbpTestsBaseModule),
        typeof(AbpAutofacModule)
        )]
    public class AbpExceptionHandlingEmailingTestModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var configurationOptions = new AbpConfigurationBuilderOptions
            {
                BasePath = @"D:\Projects\Development\Abp\ExceptionHandling\Emailing",
                EnvironmentName = "Development"
            };

            context.Services.ReplaceConfiguration(ConfigurationHelper.BuildConfiguration(configurationOptions));
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Encryption and decryption
            Configure<AbpStringEncryptionOptions>(options =>
            {
                options.DefaultPassPhrase = "s46c5q55nxpeS8Ra";
                options.InitVectorBytes = Encoding.ASCII.GetBytes("s83ng0abvd02js84");
                options.DefaultSalt = Encoding.ASCII.GetBytes("sf&5)s3#");
            });

            // Customize the exceptions that need to be handled
            Configure<AbpExceptionHandlingOptions>(options =>
            {
                // Add the exception types that need to be handled
                options.Handlers.Add<AbpException>();
            });
            // Customize the exception types that need to send email notifications
            Configure<AbpEmailExceptionHandlingOptions>(options =>
            {
                // Whether to send stack information
                options.SendStackTrace = true;
                // Default received email if no exception recipient is specified
                options.DefaultReceiveEmail = "colin.in@foxmail.com";
                // Specify which email to send certain exceptions to
                options.HandReceivedException<AbpException>("colin.in@foxmail.com");
                options.HandReceivedException<TestSendEmailException>("colin.in@foxmail.com");
            });
        }
    }
}
