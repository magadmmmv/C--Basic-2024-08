using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using SummaryProjectBotPlanner.Models;
using SummaryProjectBotPlanner.Repositories;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text;

namespace SummaryProjectBotPlanner
{
    public class UpdateHandler : IUpdateHandler
    {
        private const string AddStep = "add_step";
        private const string ReplaceStep = "replace_step";
        private const string GreetingMessage = "Добро пожаловать в планировщик задач!\n\n" +
            "Я здесь, чтобы помочь вам эффективно организовать свои дела и не упустить ничего важного. " +
            "С моей помощью вы сможете создавать и редактировать задачи.\n\n" +
            "Давайте вместе сделаем вашу жизнь более организованной и продуктивной.\n\nВыберите категорию задач:";

        public delegate void MessageHandler(Message message);
        public delegate void CallbackQueryHandler(CallbackQuery callbackQuery);

        public event MessageHandler? OnHandleMessageStarted;
        public event MessageHandler? OnHandleMessageCompleted;
        public event CallbackQueryHandler? OnHandleCallbackQueryStarted;
        public event CallbackQueryHandler? OnHandleCallbackQueryCompleted;
        private TelegramBotClient BotClient { get; }
        private LinqRepository LinqRepo { get; }
        private Category CurrentCategory { get; set; }
        private TaskNote? CurrentTaskNote { get; set; }
        private Category? TargetCategory { get; set; }
        private string? PreviousAction { get; set; }

        private InlineKeyboardMarkup actionInlineKeyboard = new(
                        [
                            [
                                InlineKeyboardButton.WithCallbackData("Добавить задачу", AddStep),
                                InlineKeyboardButton.WithCallbackData("Перенести задачу", ReplaceStep),
                            ]
                        ]
                 );

        public UpdateHandler(TelegramBotClient bot, CancellationToken cancellationToken)
        {
            BotClient = bot;
            LinqRepo = new LinqRepository();
        }

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
                    case UpdateType.CallbackQuery:
                        await BotOnCallbackReceived(botClient, update.CallbackQuery, cancellationToken);
                        break;
                }
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, HandleErrorSource.PollingError, cancellationToken);
            }
        }

        private async Task BotOnCallbackReceived(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
        {
            OnHandleCallbackQueryStarted?.Invoke(callbackQuery!);

            switch (callbackQuery!.Data)
            {
                case AddStep:
                    await BotClient.SendMessage(callbackQuery.Message!.Chat, $"Введите описание задачи:");
                    PreviousAction = AddStep;
                    break;
                case ReplaceStep:
                    InlineKeyboardMarkup taskInlineKeyboard = new(  
                        GetInlineTaskNoteButtons(CurrentCategory)
                    );
                    await BotClient.SendMessage(callbackQuery.Message!.Chat, $"Выберите ниже какую задачу перенести:", replyMarkup: taskInlineKeyboard);
                    PreviousAction = ReplaceStep;
                    break;
            }

            if (PreviousAction == ReplaceStep && CurrentTaskNote == null && callbackQuery!.Data != ReplaceStep)
            {
                var taskNotes = LinqRepo.GetTaskNotesByCategory(CurrentCategory);

                InlineKeyboardMarkup replaceInlineKeyboard = new(
                        [
                            GetInlineOtherCategories(CurrentCategory)
                        ]
                 );
                foreach (var taskNote in taskNotes)
                {
                    if (taskNote.ID.ToString() == callbackQuery!.Data)
                    {
                        CurrentTaskNote = taskNote;
                        await BotClient.SendMessage(callbackQuery.Message!.Chat, $"В какой раздел перенести?", replyMarkup: replaceInlineKeyboard);
                    }
                }
            }

            if (PreviousAction == ReplaceStep && CurrentTaskNote != null && callbackQuery.Data != CurrentTaskNote.ID.ToString() && callbackQuery.Data != null)
            {
                TargetCategory = (Category)Enum.Parse(typeof(Category), callbackQuery.Data);
                if (TargetCategory != null)
                {
                    await UpdateTaskInDatabase(CurrentTaskNote.ID, TargetCategory);
                    await botClient.SendMessage(callbackQuery.Message!.Chat, $"Выбранная задача перенесена в раздел *{TargetCategory}*. Текущий список задач:", parseMode: ParseMode.Markdown);

                    await GetTaskNotesByCategory(callbackQuery.Message!.Chat, CurrentCategory);

                    TargetCategory = null;
                    CurrentTaskNote = null;
                    PreviousAction = null;
                }
            }

            OnHandleCallbackQueryCompleted?.Invoke(callbackQuery!);
        }

        public async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            OnHandleMessageStarted?.Invoke(message);

            if (message.Type != MessageType.Text)
                return;

            if (PreviousAction == AddStep && message != null && !string.IsNullOrEmpty(message.Text))
            {
                await InsertTaskToDatabase(message.Text, CurrentCategory);
                await botClient.SendMessage(chatId: message.Chat.Id, $"Ваша задача сохранена в раздел *{CurrentCategory}*. Текущий список задач:", parseMode: ParseMode.Markdown);
                await GetTaskNotesByCategory(message.Chat, CurrentCategory);

                PreviousAction = null;
                return;
            }

            var action = message!.Text!.Split(' ')[0];

            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                if (action == category.ToString())
                {
                    CurrentCategory = category;
                    await BotClient.SendMessage(message.Chat, $"Cписок задач раздела *{CurrentCategory}*:", parseMode: ParseMode.Markdown);
                    await GetTaskNotesByCategory(message.Chat, CurrentCategory);
                    return;
                }
            }

            if (action == "/start")
            {
                PreviousAction = null;
                CurrentTaskNote = null;
                TargetCategory = null;
                ReplyKeyboardMarkup keyboard = new(
                    [
                        GetKeyboardButtonCategories()
                    ]
                )
                { ResizeKeyboard = true };
                await BotClient.SendMessage(message.Chat, GreetingMessage, replyMarkup: keyboard);
            }
            else
                await botClient.SendMessage(message.Chat, "Выберите раздел и необходимое действие!");

            OnHandleMessageCompleted?.Invoke(message);
        }

        private async Task GetTaskNotesByCategory(Chat chat, Category? category)
        {
            var categoryTaskNotes = LinqRepo.GetTaskDescriptionsByCategory(category);
            await BotClient.SendMessage(chat, CreateTaskNotes(categoryTaskNotes));
            await BotClient.SendMessage(chat, "*Выберите действие:*", replyMarkup: actionInlineKeyboard, parseMode: ParseMode.Markdown);
        }

        private async Task InsertTaskToDatabase(string text, Category category)
        {
            await LinqRepo.InsertTaskNote(text, category);
        }

        private async Task UpdateTaskInDatabase(int taskId, Category? toCategory)
        {
            await LinqRepo.UpdateTaskNote(taskId, toCategory);
        }

        internal string CreateTaskNotes(List<string> taskNotes)
        {
            var r = new StringBuilder();

            if (taskNotes.Count > 0)
            {
                foreach (var taskNote in taskNotes)
                {
                    r.Append("🔸 " + taskNote);
                    r.AppendLine();
                }

                return r.ToString();
            }
            else
                return "Список задач пуст";
        }

        private List<List<InlineKeyboardButton>> GetInlineTaskNoteButtons(Category category)
        {
            var taskNotes = LinqRepo.GetTaskNotesByCategory(category);
            var res = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < taskNotes.Count; i++)
            {
                var oneLineKeyboard = new List<InlineKeyboardButton>
                {
                    new InlineKeyboardButton
                    {
                        Text = taskNotes[i].Description!,
                        CallbackData = taskNotes[i].ID.ToString()
                    }
                };

                res.Add(oneLineKeyboard);
            }

            return res;
        }

        private List<InlineKeyboardButton> GetInlineOtherCategories(Category currentCategory)
        {
            var res = new List<InlineKeyboardButton>();
            foreach (Category otherCategory in Enum.GetValues(typeof(Category)))
            {
                if (otherCategory != currentCategory)
                {
                    var oneKeyboardButton = new InlineKeyboardButton
                    {
                        Text = otherCategory.ToString(),
                        CallbackData = otherCategory.ToString(),
                    };

                    res.Add(oneKeyboardButton);
                }
            }

            return res;
        }

        private List<KeyboardButton> GetKeyboardButtonCategories()
        {
            var res = new List<KeyboardButton>();
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                var oneKeyboardButton = new KeyboardButton
                {
                    Text = category.ToString()
                };

                res.Add(oneKeyboardButton);
            }

            return res;
        }
    }
}
