## About the Project

This C# application leverages CSV files as a database alternative, providing four key endpoints for managing and retrieving data:

![Endpoints](https://github.com/MVAPereira/API_CSV/blob/main/imgs/endpoints.png)

- **`GET /persons`**: Retrieves all persons from the CSV file, acting as a complete list of all entries stored in the system.
- **`GET /persons/{personId}`**: Fetches a specific person based on their unique ID, allowing targeted retrieval of individual records.
- **`GET /persons/color/{color}`**: Returns all persons who share a common color attribute, useful for filtering data by specific criteria.
- **`POST /persons/create`**: Allows the creation of new person entries by adding data directly to the CSV file, simulating database insertion.

## Unit Testing

The project includes robust unit tests to ensure all endpoints and data operations work correctly. These tests cover scenarios like retrieving, filtering, and creating records, ensuring the solution handles various edge cases and data validation effectively.

![Testing](https://github.com/MVAPereira/API_CSV/blob/main/imgs/testing.png)




