using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Progra1Bot.Clases
{
    public class clsEjemplo2
    {
        private static TelegramBotClient Bot;
        private static object chatAction;

        public async Task IniciarTelegram()
        {
            Bot = new TelegramBotClient("1799683591:AAEKwF_fksF54uNl8Reg7uPvQT_mII8o6jc");

            var me = await Bot.GetMeAsync();
            Console.Title = me.Username;

            Bot.OnMessage += BotCuandoRecibeMensajes;
            Bot.OnMessageEdited += BotCuandoRecibeMensajes;
            Bot.OnReceiveError += BotOnReceiveError;

            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"escuchando solicitudes del BOT @{me.Username}");



            Console.ReadLine();
            Bot.StopReceiving();
        }

        // cuando recibe mensajes
        private static async void BotCuandoRecibeMensajes(object sender, MessageEventArgs messageEventArgumentos)
        {

            //    var message = messageEventArgumentos.Message;
            //    if (message == null || message.Type != MessageType.Text)

            //        return;
            //    switch (message.Text.Split(' ').First())
            //    {
            //        case "/preguntame":
            //            await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            //            await Task.Delay(50);

            //            var hola = new InlineKeyboardMarkup(new[]
            //            {
            //            new[]
            //            {
            //                InlineKeyboardButton.WithCallbackData(
            //                    text:"imagen",
            //                    callbackData: "imagen"),
            //                InlineKeyboardButton.WithCallbackData(
            //                    text: "ubicacion",
            //                    callbackData: "ubucacion"),

            //            },
            //            new []
            //            {
            //                InlineKeyboardButton.WithCallbackData(
            //                    text:"acerca de nosotros",
            //                    callbackData: "acerca de nosotros"),
            //                InlineKeyboardButton.WithCallbackData(
            //                    text: "telefono",
            //                    callbackData: "telefono")

            //            },
            //            new[]
            //            {
            //                    InlineKeyboardButton.WithCallbackData(
            //                    text:"imagen",
            //                    callbackData: "imagen"),
            //                InlineKeyboardButton.WithCallbackData(
            //                    text: "ubicacion",
            //                    callbackData: "ubucacion"),

            //            },
            //            new []
            //            {
            //                InlineKeyboardButton.WithCallbackData(
            //                    text:"accesorios",
            //                    callbackData: "acesorios"),
            //                InlineKeyboardButton.WithCallbackData(
            //                    text: "vehiculos",
            //                    callbackData: "vehuiculos"),

            //            }
            //            });
            //            await Bot.SendTextMessageAsync(
            //                message.Chat.Id,
            //                "Elija una opcion",
            //                replyMarkup: hola);
            //            break;
            //    }
            //}
            //private static async void BotMensajes(object sender, MessageEventArgs messageEventArgumentos)
            //{
            //    var hola = CallbackQueryEventArgs.Empty;
            //    switch (hola.Data)
            //    {
            //        case "hola":
            //            ReplyKeyboardMarkup
            //    }

            //}

            var ObjetoMensajeTelegram = messageEventArgumentos;
            var mensajes = ObjetoMensajeTelegram.Message;

            string mensajeEntrante = mensajes.Text;


            string respuesta = "No te entiendo";
            if (mensajes == null || mensajes.Type != MessageType.Text)
                return;

            Console.WriteLine($"Recibiendo Mensaje del chat {ObjetoMensajeTelegram.Message.Chat.Id}.");
            Console.WriteLine($"Dice {ObjetoMensajeTelegram.Message.Text}.");


            if (mensajes.Text.Contains("hola"))
            { 
                respuesta = "Hola me da mucho gusto de Saludarte deseas algun producto de nuestra tienda" + "\uD83D\uDCDE" + " te ofrecemos repuestos y accesorios para moto y vehiculo" + "🔧";
            }
            if (mensajes.Text.Contains("para moto"))
            {
                respuesta = " DEspliegue la base de datos de los productos ";
            }
                if (!String.IsNullOrEmpty(respuesta))
                {
                    await Bot.SendTextMessageAsync(
                        chatId: ObjetoMensajeTelegram.Message.Chat,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        text: respuesta

                );
                }//fin del metodo de recepcion de mensajes
        }
        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
                Console.WriteLine("UPS!!! Recibo un error!!!: {0} — {1}",
                    receiveErrorEventArgs.ApiRequestException.ErrorCode,
                    receiveErrorEventArgs.ApiRequestException.Message
                );
        }








         } }
