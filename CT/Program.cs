using System;

namespace ConsoleTwitch
{
    public class Program
    {
        public int n = 0;

        private static void Main(string[] args)
        {
            TwitchAddons twitchAddons = new TwitchAddons();

            twitchAddons.MonitorAsync();

            Console.ReadLine();
        }
    }
}