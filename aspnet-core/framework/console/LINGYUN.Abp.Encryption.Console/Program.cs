using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp;
using Volo.Abp.Security.Encryption;
using static System.Console;

namespace LINGYUN.Abp.Encryption.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");

            var application = AbpApplicationFactory.Create<AbpEncryptionConsoleModule>();

            application.Initialize();

            while (true)
            {
                WriteLine("D: Decrypt E: Encrypt Q: Exit");

                var opt = ReadLine();
                var en = false;

                if (opt == "Q")
                {
                    break;
                }

                if ("E".Equals(opt, StringComparison.InvariantCultureIgnoreCase))
                {
                    en = true;
                    WriteLine("Please enter the string that needs to be encrypted");
                }
                else
                {
                    WriteLine("Please enter the string to be decrypted");
                }

                var sourceChr = ReadLine();
                var encryptionService = application.ServiceProvider.GetRequiredService<IStringEncryptionService>();
                WriteLine(en ? encryptionService.Encrypt(sourceChr) : encryptionService.Decrypt(sourceChr));

                WriteLine("Press any key to continue");

                ReadKey();

                Clear();
            }

            application.Shutdown();
        }
    }
}
