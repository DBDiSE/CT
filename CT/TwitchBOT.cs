using MySql.Data.MySqlClient;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace ConsoleTwitch
{
    internal class TwitchBOT
    {
        private TwitchClient client;
        private int licz;
        private int licz2 = 0;
        private int botlicz = 0;
        private static int instances = 0;

        private DateTime dnow = DateTime.UtcNow;
        private DateTime dnow2 = DateTime.UtcNow.AddMinutes(5);

        private BotCheck check = new BotCheck();

        //private static string connStr = "server=51.254.246.195;user=root;database=Twitch;port=3306;password=wiselka1";
        //private static string connStr = "server=188.165.26.194;user=root;database=Twitch;port=3306;password=wiselka1";

        private static string connStr = "server=127.0.0.1;user=root;database=Twitch;port=3306;password=wiselka1";

        public void Bot(string kanal)
        {
            instances++;

            string kanal2 = kanal;

            Console.WriteLine("");
            Console.WriteLine("Bot uruchamiany na kanale: " + kanal2);

            ConnectionCredentials credentials = new ConnectionCredentials("pomagam_dzbanom", "oauth:qdftj9ur5lrsbm42zsjl07aeoo1plx");

            client = new TwitchClient();
            client.Initialize(credentials, kanal);
            client.Connect();

            Console.WriteLine("Bot polaczony na kanale: " + kanal2);

            client.OnMessageReceived += Client_OnMessageReceived;
        }

        public static int GetActiveInstances()
        {
            return instances;
        }

        public void Zapisz(string czas, string nick, string wiadomosc, string kanal)
        {
            licz = licz + 1;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO Chat (Kanal, Czas, Nick, Wiadomosc) VALUES('" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(kanal) + "', '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(czas) + "', '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(nick) + "', '" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(wiadomosc) + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error in adding mysql row. Error: " + ex.Message);
                }
            }
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            var wiadomosc = e.ChatMessage.Message;
            var user = e.ChatMessage.Username;
            var kanal = e.ChatMessage.Channel;

            if (DateTime.UtcNow <= dnow2)
            {
                if (e.ChatMessage.Message.Contains("usuni"))
                {
                    wiadomosc = "x";
                    user = "x";
                    Console.WriteLine("WIADOMOSC USUNIETA");
                }
                else
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "   " + kanal + "    " + user + "    " + wiadomosc);
                    Zapisz(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), user, wiadomosc, kanal);
                }
            }
            else
            {
                Console.WriteLine("KONCZE BOTA: " + e.ChatMessage.Channel);
                client.OnMessageReceived -= Client_OnMessageReceived;
                botlicz++;
                Check(botlicz);
                instances--;
                Console.WriteLine("AKTYWNYCH BOTOW:         " + GetActiveInstances());

                return;
            }
        }

        public void Check(int licz)
        {
            if (instances <= 1)
            {
                TwitchAddons twitchAddons = new TwitchAddons();

                twitchAddons.MonitorAsync();
            }
            else
            {
            }
        }
    }
}