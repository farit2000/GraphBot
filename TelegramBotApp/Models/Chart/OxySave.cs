using System;
using System.IO;
using OxyPlot;
using OxyPlot.WindowsForms;
//save graphics
namespace TelegramBotApp.Models.Chart
{
    public static class OxySaveExtension
    {
        public static Stream Save(this PlotModel plot)
        {
            var stream = new MemoryStream();
            var pngExporter = new PngExporter
            {
                Width = 800,
                Height = 800,
                Background = OxyColors.White
            };
            pngExporter.Export(plot, stream);
            stream.Position = 0;
            return stream;
        }
    }
}