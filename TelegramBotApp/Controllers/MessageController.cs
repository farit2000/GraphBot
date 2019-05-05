using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotApp.Models;
using TelegramBotApp.Models.Chart;
using OxyPlot;
using OxyPlot.Series;

namespace TelegramBotApp.Controllers
{
    [Route("api/message/update")]
    public class MessageController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "Method GET unuvalable";
        }

        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();

            if (message.Type == MessageType.Document)
            {
                var file = botClient.GetFileAsync(message.Document.FileId);
                var result = file.Result;
                var path = result.FilePath;
                var url = $@"https://api.telegram.org/file/bot{AppSettings.Key}/{path}";
                var response = new WebClient().DownloadData(url);
                var points = Encoding.Default.GetString(response);

                var flag = "success";

                try
                {
                    var plot = new PlotModel();
                    var listOfPoints = ChartClassic.Parse(points);
                    var series = new LineSeries();
                    foreach (var point in listOfPoints)
                    {
                        series.Points.Add(new DataPoint(point.X, point.Y));
                    }
                    plot.Series.Add(series);

                    var stream_png = plot.Save();
                    await botClient.SendPhotoAsync(message.Chat.Id, stream_png);
                }
                catch(Exception e) { flag = e.Message; }

                await botClient.SendTextMessageAsync(message.Chat.Id, flag);
                return Ok();
            }

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    break;
                }
            }
            return Ok();
        }
    }
}