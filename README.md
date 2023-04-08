# ChessAspNet

ChessAspNet is a server-side application built using the ASP.NET Core web framework. It is a chess game that allows two players to play against each other in real-time, with the game state being stored in a SQL Server database using EF Core. The server code is responsible for managing the game state, enforcing the rules of chess, and facilitating real-time communication between players using SignalR.

## Features

- Allows two players to play chess against each other in real-time
- Game state and users stats is stored in a SQL Server database using EF Core
- Uses SignalR for real-time communication between players
- Uses cookie-based authentication to authenticate users.

## Requirements

- .NET Core SDK 3.1 or later
- SQL Server
- Visual Studio or Visual Studio Code (optional)

## Getting Started

1. Clone the repository: `git clone https://github.com/Taronn/ChessAspNet.git`
2. Open the solution in Visual Studio or Visual Studio Code
3. Configure the connection string for the database in the `appsettings.json` file
4. Run the following commands in the Package Manager Console to create the database and apply the migrations:

```
Add-Migration InitialCreate
Update-Database
```

5. Run the application and navigate to `https://localhost:5001`

## Technologies Used

- ASP.NET Core
- SignalR
- SQL Server
- EF Core
