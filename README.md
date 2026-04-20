# Restaurant Management System

A desktop application for managing restaurant staff, user authentication,
and role-based access control, built with C# .NET 8 and Windows Forms.

## Tech Stack
- **Language:** C# (.NET 8)
- **UI Framework:** Windows Forms (WinForms)
- **Database:** Microsoft SQL Server
- **Auth:** Salt + PBKDF2 password hashing
- **Architecture:** DAL pattern (Data Access Layer)

## Features
- Login / Registration with secure password hashing
- Role-based access control (Admin / User / Guest)
- Employee (staff) management with filtering
- Optimistic concurrency control (RowVersion)
- Restaurant deletion with database trigger

## Setup
1. Run the SQL scripts in the `database/` folder (or root) on a local SQL Server instance
2. Open `LAB4.csproj` in Visual Studio 2022
3. Build and run — connection uses `Data Source=.` (local SQL Server)
