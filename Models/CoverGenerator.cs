using System;
using System.IO;
using System.Threading.Tasks;
using BookShelfAPI.Storage;
using ceTe.DynamicPDF.Rasterizer;
using Microsoft.AspNetCore.Http;

namespace BookShelfAPI.Models
{
      public class CoverGenerator
    {
        public Task<Stream> GenerateCover(Stream file)
        {
            if(file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            Stream imageStream = null;
            var task = Task.Factory.StartNew(() => 
            {
                InputPdf input= new InputPdf(file); 
                PdfRasterizer pdf = new PdfRasterizer(input);   
                byte[] image = pdf.Pages[0].Draw(ImageFormat.Jpeg, ImageSize.Dpi72);
                imageStream = new MemoryStream(image);
            });
            return Task.WhenAll(task).ContinueWith(t =>
            {
                return imageStream;
            });
        }
    }
}