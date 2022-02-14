using iText.IO.Image;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    public class BackgroundPdf
    {
        public void AddBackground()
        {  
            
            string[] filenames = { "Volvo", "BMW", "Ford", "Mazda" };              
            string outputPath = "pdf/test.pdf";
            Document doc = new Document();  
            PdfCopy writer = new PdfCopy(doc, new FileStream(outputPath, FileMode.Create));  
            if(writer==null)  
            {  
                return;  
            }  
            doc.Open();  
            foreach(string filename in filenames)  
            {  
                PdfReader reader = new PdfReader(filename);  
                reader.ConsolidateNamedDestinations();  
                for(int i=1;i<=reader.NumberOfPages;i++)  
                {  
                    PdfImportedPage page = writer.GetImportedPage(reader, i);  
                    writer.AddPage(page);  
                }                 
                reader.Close();  
            }  
            writer.Close();  
            doc.Close();  
        }

        public void AddBackground1(string file)
        {
            string WatermarkLocation = "bg.png";

            PdfReader pdfReader = new PdfReader(file);
            PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(file.Replace(".pdf", "[temp][file].pdf"), FileMode.Create));
            //optional: if image is wider than the page, scale down the image to fit the page
            var sizeWithRotation = pdfReader.GetPageSizeWithRotation(1);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(WatermarkLocation);
            if (img.Width > sizeWithRotation.Width)
                img.ScalePercent(sizeWithRotation.Width / img.Width * 100);

            //set image position in top left corner
            //in pdf files, cooridinates start in the left bottom corner
            img.SetAbsolutePosition(0, sizeWithRotation.Height - img.ScaledHeight);
            // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
            Console.WriteLine("pdfReader.NumberOfPages " + pdfReader.NumberOfPages);
            PdfContentByte waterMark;
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                Console.WriteLine("Set bg page " + page);
                waterMark = stamp.GetUnderContent(page);
                waterMark.AddImage(img);
            }
            stamp.FormFlattening = true;
            stamp.Close();

            // now delete the original file and rename the temp file to the original file
            File.Delete(file);
            File.Move(file.Replace(".pdf", "[temp][file].pdf"), file);
        }
        
        //private Image GetBackground()
        //{
        //    Image img = new Image(ImageDataFactory.create("test.png"))
        //    .scaleToFit(1700, 1000)
        //    .setFixedPosition(0, 0);
        //    return img;
        //}
    }
}
