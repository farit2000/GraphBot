using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ZedGraph;

namespace TelegramBotApp.Models.Chart
{
    public class ZedChart
    {
        ZedGraphControl graph;
        public ZedChart()
        {
            graph = new ZedGraphControl();
        }

        private List<Point> Parse(string points)
        {
            var result = new List<Point>();
            var arr = points.Split(new char[] { '(', ';', ' ', ')' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => int.Parse(e))
                .ToList();
            if (arr.Count % 2 == 0)
            {
                for (var i = 0; i < arr.Count - 1; i++)
                    result.Add(new Point(arr[i], arr[i + 1]));
                return result;
            }
            throw new ArgumentException("Точки заданы неверно!");
        }

        public void AddCurve(string points)
        {
            var result = Parse(points);
            var pairOfPoint = new PointPairList();
            foreach(var point in result)
            {
                pairOfPoint.Add(point.X, point.Y);
            }
            graph.GraphPane.AddCurve("тест", pairOfPoint, Color.Red);
        }

        public Stream Save()
        {
            var image = graph.MasterPane.GetImage();
            Stream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return stream;
        }
    }
}
