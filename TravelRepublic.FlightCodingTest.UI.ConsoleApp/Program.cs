using System;
using System.Collections.Generic;
using TravelRepublic.FlightCodingTest.Fakes;
using TravelRepublic.FlightCodingTest.Model;
using TravelRepublic.FlightCodingTest.Service;

namespace TravelRepublic.FlightCodingTest.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new FlightBuilder();

            var service = new FlightService(builder);

            IList<Flight> res = new List<Flight>();

            res = service.FindValidFlightsWithDepartureBeforeDate(DateTime.Now.AddDays(4));
            Console.WriteLine("\nService.Find(Before) runs.");

            res = service.FindValidFlightsWithWaitingTimeGraterThan(TimeSpan.FromHours(2));
            Console.WriteLine("Service.Find(Waiting) runs.");

            res = service.FindFlightsWithArrivalBeforeDeparture();
            Console.WriteLine("Service.Find(Arrival before Departure) runs.");

            Console.WriteLine("\nAndrea - It runs!");
            Console.WriteLine("The Runner - Ok, that's amazing, but... what happened here?");
            Console.WriteLine("Andrea - Uhmm... I'd suggest to use the test project...");
            Console.WriteLine("The Runner - What?");
            Console.WriteLine("Andrea - I'm still working on the client app.");
            Console.WriteLine("The Runner - What?");
            Console.WriteLine("Andrea - It runs... maybe... I promise I'll complete it...");
            Console.WriteLine("The Runner - #%*'@+! - said darkly, in inscrutable language.");
            Console.WriteLine("\nPress any key to hit the programmer.");
            Console.ReadKey();
        }
    }
}
