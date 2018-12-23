using System;
using System.Collections.Generic;

namespace ConsoleTwitch
{
    internal class BotCheck
    {
        private List<string> list = new List<string>();

        public bool CheckBots(string kanal)
        {
            list.Add(kanal);

            Console.WriteLine("SPRAWDZAM: " + list.Count);

            if (list.Count == 9)
            {
                list.Clear();

                TwitchAddons twitchAddons = new TwitchAddons();

                twitchAddons.MonitorAsync();
            }

            return true;
        }
    }
}