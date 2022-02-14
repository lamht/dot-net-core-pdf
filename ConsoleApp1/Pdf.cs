using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Pdf
    {
        IConverter _converter;
        public Pdf(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] ConvertPdf(string htmlContent)
        {
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = { Top = 70, Bottom = 25, Left = 20, Right = 20, Unit = DinkToPdf.Unit.Millimeters }
                },
            };

            string headerPath = GetHeaderPath();
            string footerPath = GetFooterPath();
            var page = new ObjectSettings()
            {
                PagesCount = true,
                WebSettings = { DefaultEncoding = "utf-8" },
                HtmlContent = htmlContent,
                //HeaderSettings = { HtmUrl = headerPath },
                FooterSettings = { HtmUrl = footerPath }
            };
            pdf.Objects.Add(page);
            byte[] data = _converter.Convert(pdf);
            File.Delete(headerPath);
            File.Delete(footerPath);
            return data;
        }

        public string GetHeaderPath()
        {
            string html = File.ReadAllText("header.html");
            //html.Replace
            string pathDir = Path.Combine(Directory.GetCurrentDirectory(), "html");
            string path = Path.Combine(pathDir, $"header_{DateTime.UtcNow:yyyyMMddHHmmss}.html");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            File.WriteAllText(path, html);

            return path;
        }

        public string GetFooterPath()
        {
            string html = File.ReadAllText("footer.html");
            string pathDir = Path.Combine(Directory.GetCurrentDirectory(), "html");
            string path = Path.Combine(pathDir, $"footer_{DateTime.UtcNow:yyyyMMddHHmmss}.html");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            File.WriteAllText(path, html);

            return path;
        }
    }
}
