using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace DZ10
{
    internal class Program
    {
        // replace YOUR_BOT_TOKEN below, or set your BOT_TOKEN in Project Properties > Debug > Launch profiles UI > Environment variables
        private static string _botKey = Environment.GetEnvironmentVariable("BOT_TOKEN") ?? "YOUR_BOT_TOKEN";

        private static async Task Main()
        {
            using var cts = new CancellationTokenSource();
            var botClient = new TelegramBotClient(_botKey, cancellationToken: cts.Token);

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message],
                DropPendingUpdates = true
            };
            var handler = new UpdateHandler();            

            try
            {
                Console.WriteLine("Нажмите клавишу A для выхода");

                if (Console.ReadKey(true).Key == ConsoleKey.A)
                    cts.Cancel(); // stop the bot
                else
                {
                    var me = await botClient.GetMe(cts.Token);
                    Console.WriteLine($"{me.FirstName} запущен!");
                }                    

                botClient.StartReceiving(handler, receiverOptions);

                handler.OnHandleUpdateStarted += PrintConsoleMessageStarted;
                handler.OnHandleUpdateCompleted += PrintConsoleMessageCompleted;                

                await Task.Delay(-1, cts.Token); // We set an infinite delay so that the bot runs continuously with CancellationTokenSource
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
                handler.OnHandleUpdateStarted -= PrintConsoleMessageStarted;
                handler.OnHandleUpdateCompleted -= PrintConsoleMessageCompleted;
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
    }    
}
