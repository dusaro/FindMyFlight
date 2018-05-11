# Find my flight!

# Solution
The project has been kept as simple as possible, 
- the directories: *"API"*, *"Fakes"*, *"Model"*, *"Service"*, in a rial situatios would be four different projects;
- no needs for an service interface here (at the moment)
- fluent API implemented through extension methods, a better implementation is to implement interfaces that make possible to define a way to allow only a subset of methods after a method has been applied but no need of that here.
- console application exist only to see the project run but the test project is used to run the application.
- tests are basic, the assert has been done only on the number of flights returned instead of comparing the flights themselves but this can achieved through the us of an identifier for the flight.

# Instructions

*Invoke the GetFlights method and given the flights returned from the FlightBuilder class filter out those that:*

1. Depart before the current date/time.

2. Have a segment with an arrival date before the departure date.

3. Spend more than 2 hours on the ground. i.e those with a total gap of over two hours between the arrival date of one segment and the departure date of the next.

While solving the problem correctly is important this is an opportunity to show how you would go about structuring a solution to the problem.

Include as much or as little as you deem appropriate.

As a starting point it is highly likely we will want to adjust, add to or remove from these rules.
