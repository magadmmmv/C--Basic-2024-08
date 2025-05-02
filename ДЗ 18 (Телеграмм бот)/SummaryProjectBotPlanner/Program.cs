using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SummaryProjectBotPlanner
{
    internal class Program
    {        
        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            var botClient = new TelegramBotClient(Config._botKey, cancellationToken: cts.Token);

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery],
                DropPendingUpdates = true
            };
            var handler = new UpdateHandler(botClient, cts.Token);

            try
            {
                Console.WriteLine("Нажмите клавишу A для выхода");

                if (Console.ReadKey(true).Key == ConsoleKey.A)
                    cts.Cancel();
                else
                {
                    var me = await botClient.GetMe(cts.Token);
                    Console.WriteLine($"{me.FirstName} запущен!");
                }

                botClient.StartReceiving(handler, receiverOptions, cts.Token);

                handler.OnHandleMessageStarted += PrintConsoleMessageStarted;
                handler.OnHandleMessageCompleted += PrintConsoleMessageCompleted;
                handler.OnHandleCallbackQueryStarted += PrintConsoleCallbackQueryStarted;
                handler.OnHandleCallbackQueryCompleted += PrintConsoleCallbackQueryCompleted;

                await Task.Delay(-1, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Async operation was canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                handler.OnHandleMessageStarted -= PrintConsoleMessageStarted;
                handler.OnHandleMessageCompleted -= PrintConsoleMessageCompleted;
                handler.OnHandleCallbackQueryStarted -= PrintConsoleCallbackQueryStarted;
                handler.OnHandleCallbackQueryCompleted -= PrintConsoleCallbackQueryCompleted;
            }
        }

        public static void PrintConsoleMessageStarted(Message message)
        {
            Console.WriteLine($"Началась обработка сообщения '{message.Text ?? message.Type.ToString()}'");
        }
        public static void PrintConsoleMessageCompleted(Message message)
        {
            Console.WriteLine($"Закончилась обработка сообщения '{message.Text ?? message.Type.ToString()}'");
        }
        public static void PrintConsoleCallbackQueryStarted(CallbackQuery callbackQuery)
        {
            Console.WriteLine($"Началась обработка действия '{callbackQuery.Data}'");
        }
        public static void PrintConsoleCallbackQueryCompleted(CallbackQuery callbackQuery)
        {
            Console.WriteLine($"Закончилась обработка действия '{callbackQuery.Data}'");
        }
    }
}
