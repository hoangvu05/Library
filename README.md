LIBRARY MANAGEMENT SYSTEM
=========================

A comprehensive Library Management System built with C# and .NET, designed to
streamline the management of books, members, borrowing transactions, and
library operations. The project follows a clean layered architecture,
separating the application logic from data models for better maintainability
and scalability.

Repository: https://github.com/hoangvu05/Library
Author: hoangvu05
Last Updated: August 30, 2024


TABLE OF CONTENTS
-----------------

1. Overview
2. Features
3. Project Structure
4. Architecture
5. Technologies Used
6. Prerequisites
7. Installation
8. Configuration
9. Usage
10. Screenshots
11. API Reference
12. Database Schema
13. Testing
14. Roadmap
15. Contributing
16. Code of Conduct
17. FAQ
18. Troubleshooting
19. License
20. Acknowledgements
21. Contact


1. OVERVIEW
-----------

The Library Management System is a desktop/web application developed in C#
using the .NET framework. It provides an all-in-one solution for librarians
and administrators to efficiently manage their library's daily operations.

This system was created to solve common problems faced by small to medium-
sized libraries, such as:

  - Manual tracking of books and members
  - Difficulty in managing borrow/return transactions
  - Lack of real-time availability information
  - Inefficient search and cataloging processes

With this application, librarians can easily catalog books, register members,
track borrowed items, calculate fines, and generate reports - all from a
single, intuitive interface.

Note: This README is based on the repository's file structure. For specific
features and functionality, please refer to the source code or open an issue.


2. FEATURES
-----------

CORE FEATURES

  Book Management
    - Add, edit, delete, and view books
    - Categorize books by genre, author, and publisher
    - Track ISBN, publication year, and stock quantity
    - Upload and display book cover images

  Member Management
    - Register new library members
    - Update member information
    - View borrowing history per member
    - Manage member status (active/inactive/suspended)

  Borrow & Return System
    - Issue books to members
    - Record return dates and calculate fines
    - Track overdue books
    - Renewal support

  Search & Filter
    - Search books by title, author, ISBN, or category
    - Filter members by name, ID, or status
    - Advanced search with multiple criteria

  Reports & Analytics
    - Most borrowed books report
    - Active members report
    - Overdue items report
    - Monthly/Yearly statistics

ADDITIONAL FEATURES

  - User authentication and authorization
  - User-friendly interface
  - Data persistence with a relational database
  - Responsive design (if web-based)
  - Multi-language support (optional)
  - Notifications for overdue books


3. PROJECT STRUCTURE
--------------------

Library/
|
+-- Library/                        # Main application project
|   +-- Controllers/                # API/UI controllers
|   +-- Views/                      # UI views (if MVC/MVVM)
|   +-- Services/                   # Business logic layer
|   +-- Repositories/               # Data access layer
|   +-- Utilities/                  # Helper classes and extensions
|   +-- Resources/                  # Images, icons, localization files
|   +-- App.config                  # Application configuration
|   +-- Program.cs                  # Application entry point
|
+-- LibraryModel/                   # Data model project
|   +-- Entities/                   # Entity classes (Book, Member, Loan, etc.)
|   +-- DTOs/                       # Data Transfer Objects
|   +-- Enums/                      # Enumerations
|   +-- Interfaces/                 # Model interfaces
|
+-- .gitignore                      # Git ignore rules
+-- Library.sln                     # Visual Studio solution file
+-- README.md                       # Project documentation

The Library/ folder contains the core application logic, user interface, and
business rules. The LibraryModel/ folder contains model classes representing
the data entities used throughout the application. The Library.sln file is
the Visual Studio solution that ties the projects together.


4. ARCHITECTURE
---------------

This project follows a layered architecture pattern:

  +-----------------------------------------+
  |         Presentation Layer              |
  |      (UI / Controllers / Views)         |
  +-----------------------------------------+
  |         Business Logic Layer            |
  |            (Services)                   |
  +-----------------------------------------+
  |         Data Access Layer               |
  |          (Repositories)                 |
  +-----------------------------------------+
  |           Model Layer                   |
  |    (Entities / DTOs / Interfaces)       |
  +-----------------------------------------+
  |              Database                   |
  |      (SQL Server / SQLite / etc.)       |
  +-----------------------------------------+

Benefits:
  - Separation of concerns
  - Easier unit testing
  - Maintainable and scalable codebase
  - Reusable components


5. TECHNOLOGIES USED
--------------------

  Language         : C#
  Framework        : .NET (version TBD)
  IDE              : Visual Studio 2022 / VS Code
  Database         : SQL Server / SQLite (TBD)
  ORM              : Entity Framework (optional)
  UI Framework     : WinForms / WPF / ASP.NET (TBD)
  Testing          : xUnit / NUnit / MSTest
  Version Control  : Git & GitHub


6. PREREQUISITES
----------------

Before running this project, ensure you have the following installed:

  - .NET SDK (version 6.0 or later recommended)
    https://dotnet.microsoft.com/download
  - Visual Studio 2022 (Community Edition is free)
    https://visualstudio.microsoft.com/
  - Git
    https://git-scm.com/downloads
  - A supported database (SQL Server, SQLite, or as configured in App.config)


7. INSTALLATION
---------------

Step 1: Clone the Repository

  git clone https://github.com/hoangvu05/Library.git
  cd Library

Step 2: Open the Solution

  Open Library.sln in Visual Studio:

    start Library.sln

  Or via command line:

    dotnet open Library.sln

Step 3: Restore Dependencies

    dotnet restore

Step 4: Build the Solution

    dotnet build

Step 5: Run the Application

    dotnet run --project Library


8. CONFIGURATION
----------------

Update the connection string in App.config or appsettings.json:

  <connectionStrings>
    <add name="LibraryDB"
         connectionString="Server=localhost;Database=LibraryDB;Trusted_Connection=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>


9. USAGE
--------

FOR LIBRARIANS

  1. Login with your credentials.
  2. Add Books via the Books menu -> "Add New Book".
  3. Register Members via the Members menu.
  4. Issue Books by selecting a member and a book.
  5. Return Books and automatically calculate fines for overdue items.
  6. Generate Reports from the Reports section.

FOR MEMBERS (if applicable)

  1. Login to the member portal.
  2. Browse the catalog.
  3. View your borrowed books.
  4. Check due dates and fines.

SAMPLE COMMANDS (CLI)

  # Add a new book
  dotnet run --project Library -- add-book --title "Clean Code" --author "Robert C. Martin"

  # List all books
  dotnet run --project Library -- list-books

  # Borrow a book
  dotnet run --project Library -- borrow --memberId 1 --bookId 42


10. SCREENSHOTS
---------------

Add screenshots of your application here.

  Dashboard          : docs/screenshots/dashboard.png
  Book Management    : docs/screenshots/books.png
  Member Management  : docs/screenshots/members.png
  Reports            : docs/screenshots/reports.png


11. API REFERENCE
-----------------

If the project exposes an API, document endpoints here:

  Method | Endpoint             | Description
  -------|----------------------|--------------------------
  GET    | /api/books           | Get all books
  GET    | /api/books/{id}      | Get book by ID
  POST   | /api/books           | Add a new book
  PUT    | /api/books/{id}      | Update a book
  DELETE | /api/books/{id}      | Delete a book
  GET    | /api/members         | Get all members
  POST   | /api/loans           | Create a new loan


12. DATABASE SCHEMA
-------------------

The database consists of three main tables.

  BOOKS TABLE
    Id (PK), Title, Author, ISBN, Category, Quantity

  LOANS TABLE
    BookId (FK), MemberId (FK), BorrowDate, DueDate, ReturnDate, Fine

  MEMBERS TABLE
    Id (PK), Name, Email, Phone, Address, Status

The Books and Loans tables are related through the BookId foreign key, and
the Loans and Members tables are related through the MemberId foreign key,
creating a many-to-many relationship between Books and Members through the
Loans junction table.


13. TESTING
-----------

Run unit tests using:

  dotnet test

To add tests:

  dotnet new xunit -n Library.Tests
  dotnet add Library.Tests reference Library


14. ROADMAP
-----------

  [x] Initial project setup
  [x] Book management module
  [x] Member management module
  [x] Borrow/Return system
  [ ] Online reservation system
  [ ] Email notifications for overdue books
  [ ] Barcode/QR code scanning
  [ ] Mobile app version
  [ ] Cloud deployment (Azure/AWS)
  [ ] Multi-language support


15. CONTRIBUTING
----------------

Contributions make the open-source community an amazing place to learn,
inspire, and create. Any contributions you make are greatly appreciated.

To contribute:

  1. Fork the project
  2. Create your feature branch:
       git checkout -b feature/AmazingFeature
  3. Commit your changes:
       git commit -m 'Add some AmazingFeature'
  4. Push to the branch:
       git push origin feature/AmazingFeature
  5. Open a Pull Request

When contributing, please follow the existing code style, write clear commit
messages, add tests for new features, update documentation as needed, and be
respectful to other contributors.


16. CODE OF CONDUCT
-------------------

Please read our Code of Conduct (CODE_OF_CONDUCT.md) before contributing.
We are committed to providing a welcoming and inclusive environment for
everyone.


17. FAQ
-------

Q: What .NET version is required?
A: We recommend .NET 6.0 or later.

Q: Can I use SQLite instead of SQL Server?
A: Yes, update the connection string and provider in App.config.

Q: How do I reset the database?
A: Delete the database file/instance and re-run the migration scripts.

Q: Is there a web version?
A: Currently a desktop application; a web version may be added in the future.


18. TROUBLESHOOTING
-------------------

  Issue                       | Solution
  ----------------------------|------------------------------------------
  Build errors                | Run "dotnet restore" and "dotnet clean"
  Database connection failed  | Verify connection string in App.config
  Missing packages            | Run "dotnet restore"
  Port already in use         | Change the port in launch settings


19. LICENSE
-----------

This project does not currently specify a license. If you intend to use,
modify, or distribute this code, please contact the repository owner for
permission.


20. ACKNOWLEDGEMENTS
--------------------

  - hoangvu05 - Project author
  - All contributors who have helped improve this project
  - The open-source community for tools and inspiration
  - Microsoft for the .NET ecosystem


21. CONTACT
-----------

Author: hoangvu05

  GitHub     : https://github.com/hoangvu05
  Repository : https://github.com/hoangvu05/Library

Feel free to reach out for questions, suggestions, or collaboration!


SHOW YOUR SUPPORT
-----------------

If you find this project helpful, please give it a star on GitHub! It helps
others discover the project and motivates further development.

Made with love by hoangvu05
