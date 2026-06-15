# 📚 LibraryApp

A desktop Library Management System built with C#, .NET 8, WPF, Entity Framework Core, and SQL Server.

---

## Table of Contents

- Overview
- Features
- Architecture
- Technologies
- Project Structure
- Getting Started
- Future Enhancements
- Author

---

## Overview

LibraryApp is a desktop application that helps manage library operations through a layered architecture based on MVVM principles.

The system is designed to handle:

- User Authentication
- Book Management
- User Management
- Book Reservations
- Password Management

---

## Features

### Authentication
- User Login
- Password Validation
- Access Control

### Book Management
- View Books
- Manage Book Information
- Track Availability

### Reservation Management
- Reserve Books
- Borrow Books
- Return Books
- Reservation Tracking

### User Management
- Manage Users
- Change Passwords

---

## Architecture

The application follows a layered architecture to ensure maintainability and scalability.

```text
Presentation Layer
        │
        ▼
ViewModels Layer
        │
        ▼
Business Logic Layer
        │
        ▼
Data Access Layer
        │
        ▼
SQL Server Database
```

### Layers

| Layer | Responsibility |
|---------|---------------|
| DesktopApp | User Interface |
| ViewModels | Presentation Logic |
| BusinessLogic | Business Rules |
| DataAccess | Database Operations |

---

## Technologies

| Technology | Description |
|------------|-------------|
| C# | Programming Language |
| .NET 8 | Application Framework |
| WPF | Desktop UI Framework |
| SQL Server | Database |
| Entity Framework Core | ORM |
| MVVM | Architectural Pattern |
| Dependency Injection | Service Management |

---

## Project Structure

```text
LibraryApp
│
├── LibraryApp.DesktopApp
├── LibraryApp.ViewModels
├── LibraryApp.BusinessLogic
├── LibraryApp.DataAccess
└── DT
```

---

## Getting Started

### Requirements

- Visual Studio 2022
- .NET 8 SDK
- SQL Server

### Installation

1. Clone the repository

```bash
git clone https://github.com/KareemAboElshekh/LibraryApp.git
```

2. Open the solution in Visual Studio.

3. Restore NuGet packages.

4. Configure the SQL Server connection string.

5. Build and run the application.

---

## Future Enhancements

- Role-Based Authorization
- Advanced Search
- Dashboard & Statistics
- Audit Logging
- Unit Testing
- CI/CD Pipeline
- Improved UI/UX

---

## Author

### Kareem Abdullah

ASP.NET Backend Developer

GitHub:
https://github.com/KareemAboElshekh

---

## License

This project is intended for educational and portfolio purposes.
