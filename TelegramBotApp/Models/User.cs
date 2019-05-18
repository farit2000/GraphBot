using System;
namespace TelegramBotApp.Models
{
    public class User
    {
        public Telegram.Bot.TelegramBotClient Client { get; set; }
        public bool IsSendFile { get; set; }

        public User(Telegram.Bot.TelegramBotClient Client, bool IsSendFile)
        {
            this.Client = Client;
            this.IsSendFile = IsSendFile;
        }
    }
}
