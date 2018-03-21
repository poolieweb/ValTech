# Notes
## Time Taken
Approx. 2.5 hours
## Assumptions
* That a house number can’t not be anything other than an “Int”, such as 11a or “Station House”.
* That file encoding and path structure is local.
* That there are no flats or shared physical locations.
* That street layout is simple.
## If more time
* Refactor out the static methods to specialised collection of int, which would encapsulate the logic and follow domain driven principles.
* Implement a concreate console proxy and integrate with a simple console application.
* Performance test LINQ statements to compare to other methods and check for duplicate IEnumerable evals.
* Revisit TestCase to introduce text base error message to aid in unit test failures.
