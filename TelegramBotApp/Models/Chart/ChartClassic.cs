using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TelegramBotApp.Models.Chart
{
    public class ChartClassic
    {
        readonly Bitmap field;
        public ChartClassic()
        {
            field = new Bitmap(800, 600);
        }

        public static List<PointF> Parse(string points)
        {
            var result = new List<PointF>();
            var arr = points.Split(new char[] { '(', ';', ' ', ')', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => int.Parse(e))
                .ToList();
            if (arr.Count % 2 == 0)
            {
                for (var i = 0; i < arr.Count - 1; i += 2)
                    result.Add(new Point(arr[i], arr[i + 1]));
                return result;
            }
            throw new ArgumentException("Точки заданы неверно!");
        }

        public void AddCurve(string label, Color color, string points)
        {
            var result = Parse(points);
            using (var g = Graphics.FromImage(field))
            {
                var pen = new Pen(color, 5);
                g.DrawCurve(pen, result.ToArray());
            }
        }

        public Stream Save()
        {
            var image = new Bitmap(1000, 1000);
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            stream.Position = 0;
            return stream;
        }
    }
}
