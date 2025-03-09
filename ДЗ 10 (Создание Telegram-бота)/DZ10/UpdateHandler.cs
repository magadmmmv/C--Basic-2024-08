using System.Net.Http.Json;
using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DZ10
{
    public class UpdateHandler : IUpdateHandler
    {
        public delegate void MessageHandler(Message message);
        public event MessageHandler? OnHandleUpdateStarted;
        public event MessageHandler? OnHandleUpdateCompleted;

        record CatFactDto(string Fact, int length);
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
            await Task.FromException(exception);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        await BotOnMessageReceived(botClient, update.Message!, cancellationToken);
                        break;
                }
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, HandleErrorSource.PollingError, cancellationToken);
            }
        }

        public async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            if (message.Type != MessageType.Text)
                return;

            OnHandleUpdateStarted?.Invoke(message);

            var action = message.Text!.Split(' ')[0];
            switch (action)
            {
                case "/cat":
                    await SendMessageCatFact(botClient, message, cancellationToken);
                    break;
                default:
                    await botClient.SendMessage(chatId: message.Chat.Id, "Сообщение успешно принято", cancellationToken: cancellationToken);
                    break;
            }

            OnHandleUpdateCompleted?.Invoke(message);
        }

        public async Task SendMessageCatFact(ITelegramBotClient botClient, Message message, CancellationToken token)
        {
            using var client = new HttpClient();
            var catFact = await client.GetFromJsonAsync<CatFactDto>("https://catfact.ninja/fact", token);
            await botClient.SendMessage(chatId: message.Chat.Id, text: catFact!.Fact, cancellationToken: token);
        }
    }
}
