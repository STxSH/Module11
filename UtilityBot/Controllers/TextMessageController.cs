using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"1" , $"symbolCount"),
                        InlineKeyboardButton.WithCallbackData($"2" , $"numCount")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Бот имеет дфе функции:</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}1. Подсчет количества символов в тексте{Environment.NewLine}" +
                        $"{Environment.NewLine}2. Вычисление суммы чисел{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;

                default:

                    switch (_memoryStorage.GetSession(message.Chat.Id).ChoosenFunction)
                    {
                        case "symbolCount":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Символов в сообщении: {message.Text.Length}", cancellationToken: ct);
                            break;
                        case "numCount":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {Utilities.NumSum.Sum(message.Text).ToString()}", cancellationToken: ct);
                            break;
                    }
                    break;
            }
        }
    }
}
