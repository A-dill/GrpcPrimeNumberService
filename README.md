# PrimeNumberCheck gRPC Service

This repository contains a PrimeNumberCheck gRPC Service, consisting of a server and a client application.

## Getting Started

**Note**: The Makefile in this project is primarily intended for use on Windows systems. While it may work on other platforms, there is no guarantee of full compatibility. If you intend to use it, ensure that you run the client and server from different terminals.

To run the PrimeNumberCheck gRPC Service, follow these steps:

1. Clone the repository to your local machine.

2. Navigate to the root folder of the project.

3. Run the server using the following command:

   ```
   dotnet run --project GrpcPrimeNumberService/GrpcPrimeNumberService.csproj
   ```

4. Open another terminal or command prompt.

5. Navigate to the root folder of the project.

6. Run the client using the following command:

   ```
   dotnet run --project PrimeNumberClient/PrimeNumberClient.csproj
   ```

7. Observe the output on both console windows. The client application will interact with the GrpcPrimeNumberService and display the results accordingly.
8. Server will run on Port 4026.
## Scaling the Application

To scale the application to handle hundreds of servers, consider the following techniques:

1. Caching: Implement caching mechanisms to store frequently requested prime numbers, reducing the need for redundant calculations.

2. Load Balancing: Load balancers can be used to distribute incoming client requests among multiple server instances, ensuring an even distribution of workload.

3. Asynchronous Processing: Offload intensive or time-consuming tasks to background workers using queues or message brokers to enhance server responsiveness.

Remember that scaling an application involves a combination of architectural decisions, infrastructure choices, and optimizing the codebase. The exact approach will depend on the specific requirements, budget, and technical expertise available. With careful planning and the right technologies, you can scale the application to handle hundreds of servers efficiently.

## Server-Side Code

### GrpcPrimeNumberService

The `GrpcPrimeNumberService` handles incoming gRPC requests from clients. It checks if the input number is prime or not and sends the response back to the client.

## Integration Testing for Server-Side Code

### GrpcPrimeNumberService

- **Positive Test:** Check if the server correctly identifies prime numbers for valid inputs.

- **Negative Test:** Ensure that the server accurately identifies non-prime numbers for invalid inputs.

## Client-Side Code

### PrimeNumberClient

The `PrimeNumberClient` is a C# Console Application that utilizes gRPC to communicate with the `GrpcPrimeNumberService`. The client sends numbers to the server for prime number checks and receives the responses.

## Integration Testing for Client-Side Code

### PrimeNumberClient

- **Test Server Communication:** Verify that the client can communicate with the `GrpcPrimeNumberService` and receive responses.

## Test Environment Setup

- **Dependency Verification:** Ensure that all required components are present and properly configured.

- **Isolation Testing:** Create an isolated environment to avoid interference from other processes during testing.

## Test Data Preparation

- **Positive and Negative Cases:** Prepare test data with both prime and non-prime numbers for different test scenarios.

- **Edge Cases:** Include extreme inputs like large prime numbers and small non-prime numbers for testing application limits.

## Test Execution

- **Automated Test Execution:** Run the tests automatically to ensure consistent and repeatable results.

## Assertion and Validation

- **Response Validation:** Verify that the server and client responses match the expected results.

## Test Cleanup

- **State Reset:** Clean up any temporary data or changes made during testing to restore the environment to its original state.

## Test Coverage Analysis

- **Coverage Metrics:** Analyze test coverage reports to identify areas of the application that may need additional testing.

## Test Failure Analysis

- **Debugging and Issue Resolution:** Investigate test failures to identify potential bugs or issues. Address problems promptly and re-run tests to verify fixes.

---
Feel free to modify and expand on this `README.md` file according to the specific details of your PrimeNumberCheck gRPC Service.