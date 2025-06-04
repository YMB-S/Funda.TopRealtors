# Funda Top Realtors

This solution calculates the top realtors on Funda based on the number of objects for sale. The solution is split into:

- **Core logic**: Handles API interaction and realtor ranking.
- **Presentation layer**: A simple console application demonstrating functionality.

Funda’s API has a rate limit of approximately 100 requests per minute. To stay within these limits, the application makes 1 request per second. To improve user experience, the results are calculated and displayed progressively — so users aren't left staring at a blank screen.

## Testability

Unit tests are not yet implemented, but the architecture supports them. Interfaces are used where appropriate (e.g., for the `ApiClient`), enabling easy mocking and future test integration.

## Note on GenAI

I consulted two large language models (DeepSeek and ChatGPT) during design. They served as sounding boards for architectural decisions — similar to rubber duck debugging - but all decisions (DI setup, interface separation, core logic/presentation layer split, service setup) were ultimately my own.

## TODO

- [ ] Add unit tests
- [ ] Add configuration for request throttling
- [ ] Consider alternative presentation layers (e.g., web UI)
