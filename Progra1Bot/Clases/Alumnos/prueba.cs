using Progra1Bot.Clases;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegrambotExamenFinal
{
    public class prueba
    {
        //Key del bot
        private static readonly TelegramBotClient Bot = new TelegramBotClient("1799683591:AAEKwF_fksF54uNl8Reg7uPvQT_mII8o6jc");
       

        static void Main(string[] args)
        {
            //Método que se ejecuta cuando se recibe un mensaje
            Bot.OnMessage += BotOnMessageReceived;

            //Método que se ejecuta cuando se recibe un callbackQuery
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            //Método que se ejecuta cuando se recibe un error
            Bot.OnReceiveError += BotOnReceiveError;

            //Inicia el bot
            Bot.StartReceiving();
            Console.WriteLine("Bot levantado!");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        internal Task IniciarTelegram()
        {
            throw new NotImplementedException();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text) return;

            switch (message.Text.Split(' ').First())
            {
                //Enviar un inline keyboard con callback
                case "/Vehiculos":

                    //Simula que el bot está escribiendo
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(50);

                    var keyboardEjemplo1 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"ubicacion de Nuestra empresa",
                            callbackData: "ubicacion de Nuestra empresa"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Contacto",
                            callbackData: "contacto"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text: "Listado de repuestos",
                            callbackData: "Listado de repuestos"),
                    }

                     });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Elija una opción",
                        replyMarkup: keyboardEjemplo1);
                    break;





                case "/Motocicletas":

                    var keyboardEjemplo2 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                        
                        InlineKeyboardButton.WithCallbackData(
                            text:"ubicacion de Nuestra empresa",
                            callbackData: "ubicacion de Nuestra empresa"),
                    },
                    new []
                    {
                        
                        InlineKeyboardButton.WithCallbackData(
                            text:"contacto",
                            callbackData: "contacto"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Listado de repuestos",
                            callbackData: "Listado de Accesorios"),
                    }
                });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Elija una opción",
                        replyMarkup: keyboardEjemplo2);
                    break;






                //Mensaje por default
                default:
                    const string usage = @"
                Comandos:
                /Vehiculos  - accesorios y Repuestos
                /Motocicletas- Accesorio y Repuestos";

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        text: usage,
                        replyMarkup: new ReplyKeyboardRemove());

                    break;
            }
        }


        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            switch (callbackQuery.Data)
            {


                case "ubicacion de Nuestra empresa":
                    await Bot.SendVenueAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        latitude: 9.932551f,
                        longitude: -84.031086f,
                        title: "Accesorios VyM",
                        address: "Jutiapa, calle 15 de sepiembre"
                        );
                    break;


                case "contacto":
                    await Bot.SendContactAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        phoneNumber: "+502 42357805",
                        firstName: "Gredy",
                        lastName: "Carrillo"
                        );
                    break;


                case "Listado de repuestos":
                    
                    SqlConnection conexion = new SqlConnection("Data Source = DESKTOP-03L4M4P\\SQLEXPRESS; Initial Catalog = Accesorios; Integrated Security = True");
                    SqlCommand comando = new SqlCommand("Insert into TbVehiculos values(7, 'llanta para camion', 2896)");
                    comando.Connection = conexion;
                    conexion.Open();

                    DataTable dt = new DataTable();
                    dt.Load(comando.ExecuteReader());
                    //string resultado = Convert.ToString(dt);

                    comando.ExecuteReader();
                    conexion.Close();

                    await Bot.SendTextMessageAsync(
                       chatId: callbackQuery.Message.Chat.Id,

                       text: "Comentario enviado");


                    break;
            }
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
    }
}