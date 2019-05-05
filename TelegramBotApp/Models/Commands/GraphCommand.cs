using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp.Models.Commands
{
    public class GraphCommand : Command
    {
        public override string Name => @"/plot";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId,
                 "Отправьте точки для построения в формате (x, y) в текстовом файле, где каждая точка начинается с новой строки. ФАЙЛ ДОЛЖЕН БЫТЬ В ФОРМАТЕ .TXT ))) ",
                  parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}