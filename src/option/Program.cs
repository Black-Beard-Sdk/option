using System;
using System.IO;
using System.Linq;
using Bb.Extensions.Configuration;
using Bb.Option;
using Microsoft.Extensions.Configuration;

//[Assembly:Guid("C768F5EC-1A46-42DB-A6B5-7430820300A4")]
namespace option
{
    class Program
    {
        
        static void Main(string[] args)
        {

            //var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            //var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
            ////Determines the working environment as IHostingEnvironment is unavailable in a console app

            //string environment = "";
            //string configurationDirectory = Directory.GetCurrentDirectory();

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(configurationDirectory)
            //    //.AddEnvironmentVariables()

            //    //.AddJsonFile(@"D:\src\option\src\outTests\schemas\Test.json", optional: false, reloadOnChange: true)
            //    //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    //.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)

            //    .AddSecureStoreFile(@"D:\src\option\src\outTests\securevault.json", "mypwd")
            //    //.BuilTypes()
            //   .Build();

            //if (isDevelopment) //only add secrets in development
            //{
            //    //builder.AddUserSecrets<YourClassName>();
            //}

            //var item = builder.GetSection("")
            //                  .Get<object>();

            //var items = builder
            //    .GetChildren()
            //    .ToList();

            Bb.CommandLine.Run<CommandLines, CommandLineOption>(args);

        }

        public static IConfigurationRoot Configuration { get; set; }

    }
}
