# NbpApi
The NbpApi is a tool designed to provide users with exchange rates and historical data to monitor and analyze currency fluctuations. This project aims to deliver an intuitive and user-friendly platform for all.

## Getting Started

### Prerequisites

* .NET 8
* Node 18+
* Docker

### Tech Stack:

Frontend: React.js
Backend: .NET
Database: MSSQL

### Installation

1.Clone the ropository:
```
$ git clone https://github.com/danaj22/NbpApi.git
$ cd NbpApi
```
2. Install packages
```
npm install
```
3.Enter your backend url in `apiClient.tsx`
```
baseURL: "backendUrl"
```

## Deployment

To run with `dockercompose` you need to make sure that all the connections (frontend, backend, redis, mssql) are set correctly. 

## Roadmap

- [x] Add readme
- [x] ~~Add tests~~ (reverted)
- [x] ~~Add charts data for one currency~~ (reverted)
- [x] ~~Add charts data for selected currencies~~ (reverted)
- [x] ~~Add job updating NBP exchange rates every working day~~ (reverted)
- [ ] Extend tests
- [ ] Finish cleaning project structure in:
  - [ ] frontend
  - [ ] backend
- [ ] Add nation flags
- [ ] Multi-language Support

## Sources

* Developing project you can find here: https://github.com/danaj22/CurrencyApp
