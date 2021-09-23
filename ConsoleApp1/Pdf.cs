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

                    Margins = { Top = 10, Bottom = 10, Left = 20, Right = 20, Unit = DinkToPdf.Unit.Millimeters },
                    //Out = Path.Combine(Directory.GetCurrentDirectory(), "assets", "test.pdf")
                },
            };
            var page = new ObjectSettings()
            {
                PagesCount = true,
                WebSettings = { DefaultEncoding = "utf-8" },
                HtmlContent = htmlContent
            };
            pdf.Objects.Add(page);
            return _converter.Convert(pdf);
            
        }
    }
}
