using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelRepublic.FlightCodingTest.Model;
using TravelRepublic.FlightCodingTest.Service;

namespace TravelRepublic.FlightCodingTest.Test
{
    [TestClass]
    public class FligthServiceTest
    {
        [TestMethod]
        public void FlightService_FlightsIsNull_GetAnyQueryReturnsEmpty()
        {
            // Arrange
            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns<IList<Flight>>(null);
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 0;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.Fligths;

            // Assert
            Assert.IsTrue((currentFlights?.Count() ?? 0) == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsIsEmpty_GetAnyQueryReturnsEmpty()
        {
            // Arrange
            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(new List<Flight>());
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 0;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.Fligths;

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithNullSegments_RemovedAsSoonAsServiceCreated()
        {
            // Arrange
            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(new List<Flight>() { new Flight { Segments = null } });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 0;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.Fligths;

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithEmptySegments_RemovedAsSoonAsServiceCreated()
        {
            // Arrange
            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(new List<Flight>() { new Flight { Segments = new List<Segment>() } });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 0;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.Fligths;

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithNoDateBeforeGivenOne_GetBeforeReturnsEmpty()
        {
            // Arrange
            var beforeDate = new DateTime(2018, 04, 30, 23, 59, 59);

            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 09, 30, 00), ArrivalDate = new DateTime(2018, 05, 01, 13, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 0;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindValidFlightsWithDepartureBeforeDate(beforeDate);

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithDateBeforeGivenOne_GetBeforeReturnsSubset()
        {
            // Arrange
            var beforeDate = new DateTime(2018, 05, 10, 23, 59, 59);

            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 09, 30, 00), ArrivalDate = new DateTime(2018, 05, 01, 13, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 2;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindValidFlightsWithDepartureBeforeDate(beforeDate);

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithSomeRequiredGap_GetValidReturnsSubset()
        {
            // Arrange
            var gap = TimeSpan.FromHours(2);

            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 09, 30, 00), ArrivalDate = new DateTime(2018, 05, 01, 13, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 1;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindValidFlightsWithWaitingTimeGraterThan(gap);

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithoutAnyRequiredGap_GetValidReturnsEmpty()
        {
            // Arrange
            var gap = TimeSpan.FromHours(5);

            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 09, 30, 00), ArrivalDate = new DateTime(2018, 05, 01, 13, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 0;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindValidFlightsWithWaitingTimeGraterThan(gap);

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithArrivalBeforeDeparture_GetValidReturnsSubset()
        {
            // Arrange
            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 09, 30, 00), ArrivalDate = new DateTime(2018, 05, 01, 09, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 1;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindFlightsWithArrivalBeforeDeparture();

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithoutAnyArrivalBeforeDeparture_GetValidReturnsEmpty()
        {
            // Arrange
            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 09, 30, 00), ArrivalDate = new DateTime(2018, 05, 01, 13, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 0;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindFlightsWithArrivalBeforeDeparture();

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        #region Extensions
        [TestMethod]
        public void FlightService_FlightsWithSomeInvalidSegments_GetValidReturnsSubset()
        {
            // Arrange
            var beforeDate = new DateTime(2018, 05, 10, 23, 59, 59);

            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 08, 21, 00), ArrivalDate = new DateTime(2018, 05, 01, 13, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 2;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindAllValidFlights();

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }

        [TestMethod]
        public void FlightService_FlightsWithNoInvalidSegments_GetValidReturnsAll()
        {
            // Arrange
            var flightBuilderMock = new Mock<IFlightBuilder>();
            flightBuilderMock.Setup(fb => fb.GetFlights()).Returns(() =>
            {
                return new List<Flight>
                {
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 07, 01, 10, 00, 00), ArrivalDate = new DateTime(2018, 07, 01, 12, 00, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 06, 15, 00), ArrivalDate = new DateTime(2018, 05, 01, 08, 22, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 09, 30, 00), ArrivalDate = new DateTime(2018, 05, 01, 13, 10, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 01, 15, 00, 00), ArrivalDate = new DateTime(2018, 05, 01, 18, 50, 00) }
                        }
                    },
                    new Flight
                    {
                        Segments = new List<Segment>
                        {
                            new Segment { DepartureDate = new DateTime(2018, 05, 10, 23, 10, 00), ArrivalDate = new DateTime(2018, 05, 11, 03, 45, 00) },
                            new Segment { DepartureDate = new DateTime(2018, 05, 11, 06, 00, 00), ArrivalDate = new DateTime(2018, 05, 11, 08, 30, 00) }
                        }
                    }
                };
            });
            var flightBuilder = flightBuilderMock.Object;

            // Act
            var expectedFlightsCount = 3;

            var service = new FlightService(flightBuilder);
            var currentFlights = service.FindAllValidFlights();

            // Assert
            Assert.IsTrue(currentFlights.Count() == expectedFlightsCount);
        }
        #endregion
    }
}
