# Notes
## Time Taken
Approx. 2.5 hours
## Assumptions
* That a house number can’t not be anything other than an “Int”, such as 11a or “Station House”.
* That file encoding and path structure is local.
* That there are no flats or shared physical locations.
* That street layout is simple.
* That a street has normal amount of houses, to large and LINQ might not perform
## If more time
* Refactor out the static methods to specialised collection of int, which would encapsulate the logic and follow domain driven principles.
* Implement a concreate console proxy and integrate with a simple console application.
* Performance test LINQ statements to compare to other methods and check for duplicate IEnumerable evals.
* Revisit TestCase to introduce text base error message to aid in unit test failures.
* Create factory interface or provider for differant input types
* Place Validation in child object of the int collection
