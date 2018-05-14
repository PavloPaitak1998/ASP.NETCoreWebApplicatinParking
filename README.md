# ASP.NETCoreWebApplicatinParking

## This is a Web Application  emulating the work of the parking.

Access to the entire functionality must be implemented using WebAPI and each method will return *JSON* objects. To test the application, you can use the following tools: [Postman](https://www.getpostman.com/) or [Fiddler](https://www.telerik.com/fiddler)

## The Solution must contain two projects:

* ASP.NET Core with open REST API

* Class Library which will contain all logic for work with parking

## The program has the following functionality:

### Parking
* Number of vacancies (GET)

* List of all machines (GET)

* Details on one machine (GET)

* Delete machine (DELETE)

* Add a car (POST)

* Number of occupied places (GET)

* General Balance (GET)

* Fill the machine balance (PUT)

### Transactions

* OutputTransactions.log (GET)

* Output last minute transaction (GET)

* Output a transaction for the last minute on one particular machine (GET)


## General logic:

There should be only 1 parking in the program. When you initialize the parking, you must use the Settings class. Parking keeps a list of machines / transactions.

You can add a car to the parking lot or pick up from the parking lot. Every N-second parking charges money from the car. If the car does not have enough money to pay parking, then write off the penalty (the penalty coefficient * the parking price). Also, we can not pick up the car until we replenish the balance and repeat the operation.

When debiting funds, parking keeps transactions for the last minute. At any time, we can turn to the parking lot and find out the current balance (earned money). We can apply to the parking lot and find out the amount of money earned in the last minute. We can apply to the parking lot and find out the number of free / occupied places in the parking lot. The cost of parking depends on the type of car.

## List of classes (without methods) that you can use in the program:

* ### Settings (read-only / static class):

  Property Timeout  (every N-second writes off the funds for the parking space) - the default is 3 seconds
  
  Dictionary - a dictionary for storing parking prices (for example: for trucks - 5, for cars - 3, for buses - 2, for motorcycles - 1)
  
  Property ParkingSpace - parking capacity (total number of seats)
  
  Property  Fine  is the penalty factor
  
* ### Parking - this class initializes using the settings described in the class Settings:

  List of machines
  
  Transaction List
  
  Property Balance (Earned Money)
  
* ### Car:

  Property identifier
  
  Property balance
  
  Property type of machine
  
* ### CarType

  Passenger
  
  Truck
  
  Bus
  
  Motorcycle
  
* ### Transaction

  Property Date / Time of the Transaction
  
  Property Machine ID
  
  Property Write-off
  
  ### Have fun :)
  _“The Way Get Started Is To Quit Talking And Begin Doing.” – Walt Disney_
