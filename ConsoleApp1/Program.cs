using ConsoleApp1;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<Pdf, Pdf>()
                .AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()))
                .BuildServiceProvider();

            //configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);

            Console.WriteLine("Hello, World!");
            var pdf = serviceProvider.GetService<Pdf>(); ;
            string html = File.ReadAllText("test.html");
            var result = pdf.ConvertPdf(html);
            string pathDir = Path.Combine(Directory.GetCurrentDirectory(),"pdf");
            string path = Path.Combine(pathDir, $"invoice_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            File.WriteAllBytes(path, result);
            Console.WriteLine("Add background");
            new BackgroundPdf().AddBackground1(path);
            Console.WriteLine(path);
            //Thread.Sleep(600000);
        }
        
    }
}
