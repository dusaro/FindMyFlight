using System.Collections.Generic;

namespace TravelRepublic.FlightCodingTest.Model
{
    public interface IFlightBuilder
    {
        IList<Flight> GetFlights();
    }
}