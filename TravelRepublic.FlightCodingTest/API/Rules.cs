using System;
using System.Collections.Generic;
using System.Linq;
using TravelRepublic.FlightCodingTest.Model;

namespace TravelRepublic.FlightCodingTest.API
{
    /// <summary>
    /// Very simple implementation of a fluent API.
    /// A better implementation is to implement interfaces
    /// that make possible to define a way to allow only
    /// a subset of methods after a method has been applied.
    /// In this example this is not necessary and has been
    /// chosen to take it simple using extension methods.
    /// </summary>
    public static class Rules
    {
        #region PUBLIC METHODS
        public static IEnumerable<Flight> EnforceSegmentsOrderByDeparture(this IEnumerable<Flight> flights)
        {
            return flights.Select(f => new Flight { Segments = f.Segments?.OrderBy(fs => fs.DepartureDate).ToList() });
        }

        public static IEnumerable<Flight> RemoveEmptyFligths(this IEnumerable<Flight> flights)
        {
            return flights.Where(f => f.Segments != null && f.Segments.Count() > 0);
        }
        public static IEnumerable<Flight> GetFlightsBeforeDate(this IEnumerable<Flight> flights, DateTime date)
        {
            return flights?.Where(f => f.Segments.Any(fs => fs.DepartureDate <= date));
        }

        public static IEnumerable<Flight> GetFlightsArrivalBeforeDeparture(this IEnumerable<Flight> flights)
        {
            return flights.Where(f => f.Segments.Any(fs => fs.ArrivalDate <= fs.DepartureDate));
        }

        public static IEnumerable<Flight> GetFlightsWaitingTimeGraterThan(this IEnumerable<Flight> flights, TimeSpan wait)
        {
            return flights.Where(f => f.Segments.Exists((curr, next) => next.DepartureDate - curr.ArrivalDate >= wait));
        }

        #region REQUIREMENTS EXTENDED 
        public static IEnumerable<Flight> GetFlightsOverlap(this IEnumerable<Flight> flights)
        {
            return flights.Where(f => f.Segments.Exists((curr, next) => next.DepartureDate <= curr.ArrivalDate));
        }

        public static IEnumerable<Flight> GetFlightsNoOverlap(this IEnumerable<Flight> flights)
        {
            return flights.Where(f => f.Segments.All((curr, next) => next.DepartureDate > curr.ArrivalDate) || f.Segments.Count() == 1);
        }

        public static IEnumerable<Flight> GetValidFlights(this IEnumerable<Flight> flights)
        {
            return flights.Where(f => f.Segments.Any(fs => fs.ArrivalDate > fs.DepartureDate))
                          .GetFlightsNoOverlap();
        }
        #endregion

        #endregion

        private static bool Exists<T>(this IEnumerable<T> source, Func<T, T, bool> action)
        {
            var s = source.ToList();
            var result = false;
            var i = 0;

            while (i <= s.Count() - 2 && !result)
            {
                if (action(s[i], s[i + 1]))
                {
                    result = true;
                }
                i++;
            }

            return result;
        }

        private static bool All<T>(this IEnumerable<T> source, Func<T, T, bool> action)
        {
            var s = source.ToList();
            var result = true;
            var i = 0;

            while (i <= s.Count() - 2 && result)
            {
                if (!action(s[i], s[i + 1]))
                {
                    result = false;
                }
                i++;
            }

            return result;
        }
    }
}
