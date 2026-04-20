# Restaurant Management System 🍽️

A desktop application for managing restaurant staff, user authentication, and role-based access control, built with C# .NET 8 and Windows Forms.

## 💻 Tech Stack
* **Language:** C# (.NET 8)
* **UI Framework:** Windows Forms (WinForms)
* **Database:** Microsoft SQL Server
* **Auth:** Salt + PBKDF2 password hashing
* **Architecture:** DAL pattern (Data Access Layer)

## ✨ Features
* Login / Registration with secure password hashing
* Role-based access control (Admin / User / Guest)
* Employee (staff) management with filtering
* Optimistic concurrency control using `RowVersion`
* Restaurant deletion utilizing database triggers

## 🚀 Setup Instructions
1. Run the `.sql` scripts located in the `database` folder on a local SQL Server instance in the following order:
   - `database_schema.sql`
   - `triggers.sql`
   - `users_setup.sql`
2. Open the `RestaurantManagement.csproj` file in Visual Studio 2022.
3. Build and run the project. *(Note: The connection string uses `Data Source=.` targeting the local SQL Server instance).*
