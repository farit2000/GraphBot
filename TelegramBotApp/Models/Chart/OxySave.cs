using System;
using System.IO;
using OxyPlot;
using OxyPlot.WindowsForms;

namespace TelegramBotApp.Models.Chart
{
    public static class OxySaveExtension
    {
        public static Stream Save(this PlotModel plot)
        {
            var stream = new MemoryStream();
            var pngExporter = new PngExporter
            {
                Width = 600,
                Height = 400,
                Background = OxyColors.White
            };
            pngExporter.Export(plot, stream);
            stream.Position = 0;
            return stream;
        }
    }
}