using System;
using WeatherAlerts.EventHandler;

namespace WeatherAlerts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------- Delegates Example ------\n");
            DelegateExample.Test();
            Console.WriteLine("\n------- Events Example ------");
            EventExample.Test();
            Console.WriteLine("\n------- Event Handlers Example ------");
            EventHandlerExample.Test();
            Console.ReadLine();
        }
    }
}
