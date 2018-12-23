using System;
using TwitchLib.Api;
using LiveStreams = TwitchLib.Api.V5.Models.Streams.LiveStreams;

namespace ConsoleTwitch
{
    internal class TwitchAddons
    {
        private TwitchAPI API;

        public async void MonitorAsync()
        {
            API = new TwitchAPI();

            API.Settings.ClientId = "0xw8roszr2iuy665w9xn78g38yjuuy";
            API.Settings.AccessToken = "oauth:qdftj9ur5lrsbm42zsjl07aeoo1plx";

            LiveStreams streams = new LiveStreams();

            var kanaly = await API.V5.Streams.GetLiveStreamsAsync(null, null, null, null, 10, null);
            int licznik = 0;
            string[] dobota = new string[10];

            foreach (var kanal in kanaly.Streams)
            {
                Console.WriteLine("TOP#" + licznik + 1);
                Console.WriteLine("");
                Console.WriteLine("Nazwa: " + kanal.Channel.DisplayName + "      " + "Widzowie: " + kanal.Viewers);
                Console.WriteLine("GRA: " + kanal.Channel.Game);
                Console.WriteLine("Tytul: " + kanal.Channel.Status);

                dobota[licznik] = kanal.Channel.DisplayName;

                licznik++;
            }

            foreach (string d in dobota)
            {
                Console.WriteLine(d);

                if (!String.IsNullOrEmpty(d))
                {
                    TwitchBOT bOT = new TwitchBOT();

                    bOT.Bot(d);
                }
                else
                {
                }
            }
        }
    }
}