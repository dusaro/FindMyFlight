using System;
using System.Collections.Generic;
using System.Linq;
using TravelRepublic.FlightCodingTest.API;
using TravelRepublic.FlightCodingTest.Model;

namespace TravelRepublic.FlightCodingTest.Service
{
    public class FlightService // The interface here is useless for the pupouse of this exercises.
    {
        #region PRIVATE FIELDS
        private readonly IFlightBuilder _flightBuilder;
        private readonly IList<Flight> _flights;
        #endregion

        #region CTOR
        public FlightService(IFlightBuilder flightBuilder)
        {
            _flightBuilder = flightBuilder;
            _flights = _flightBuilder.GetFlights()?
                                     .EnforceSegmentsOrderByDeparture()
                                     .RemoveEmptyFligths()
                                     .ToList();
        }
        #endregion

        #region PROPERTIES
        public IList<Flight> Fligths
        {
            get
            {
                return _flights?.ToList();
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Depart before the current date/time.
        /// </summary>
        public IList<Flight> FindValidFlightsWithDepartureBeforeDate(DateTime date)
        {
            return _flights?.GetValidFlights()
                            .GetFlightsBeforeDate(date)
                            .ToList();
        }

        /// <summary>
        /// Spend more than "gap" hours on the ground. i.e those with a total gap of over "gap" 
        /// hours between the arrival date of one segment and the departure date of the next.
        /// </summary>
        public IList<Flight> FindValidFlightsWithWaitingTimeGraterThan(TimeSpan gap)
        {
            return _flights?.GetValidFlights()
                            .GetFlightsWaitingTimeGraterThan(gap)
                            .ToList();
        }

        /// <summary>
        /// Have a segment with an arrival date before the departure date.
        /// </summary>
        public IList<Flight> FindFlightsWithArrivalBeforeDeparture()
        {
            return _flights?.GetFlightsArrivalBeforeDeparture()
                            .ToList();
        }

        #region REQUIREMENTS EXTENDED 
        /// <summary>
        /// All fligths with no overlap on segments and right arrival and departure time..
        /// </summary>
        public IList<Flight> FindAllValidFlights()
        {
            return _flights?.GetValidFlights()
                            .ToList();
        }
        #endregion

        #endregion
    }
}
