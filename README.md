# OutOfOffice

## About the Application

This is a web application developed using ASP.NET Core MVC with Identity, implementing the Onion architecture. The interface utilizes jQuery and Bootstrap libraries.

### OutOfOffice.Core

This project contains all the core entities of the application and interfaces that define contracts for data interaction.

### OutOfOffice.Persistence

This project handles database connectivity via the `OutOfOfficeDbContext` class. It includes table configuration classes, user role management, database migrations, and repositories that implement interfaces from `OutOfOffice.Core`. Also included is the `DbSeeder` class, designed to create roles and initial users upon application startup.

### OutOfOffice.Application

This project houses application services that utilize repository methods to execute business logic.

### OutOfOffice

The main project that integrates all the above components. Identity is implemented here, along with all application views and controllers.

## Database Architecture

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/37675830-709c-4649-8acf-94b7b70bff42)

### Changes in Database Structure

Based on the project specification, identified certain inconsistencies (in my opinion).

### Table Employees

Made the `PeoplePartnerId` field nullable because we need to create the first user, and this field cannot be mandatory.

### Table ApprovalRequests

Made the `ApproverId` field nullable because we cannot create a record in the `ApprovalRequests` table without first creating a record in the `LeaveRequests` table.

## Running the Application

To run the application, update the database connection string in `OutOfOffice --> appsettings.Development.json`.

To create the database, open `Package Manager Console` (`View --> Other Windows --> Package Manager Console`), select the `OutOfOffice.Persistence` project as the `Default project`, and run the `update-database` command. This will create a database with the name specified in the connection string.

Afterward, use the `Rebuild Solution` command.

## Roles and Users

### Administrator

Login: Administrator1

Password: Admin@123

### Project Manager

Login: ProjectManager1

Password: ProjMgr@123

### HR Manager

Login: HR_Manager1

Password: HRMgr@123

### Employee

Login: Employee1

Password: Emp@123

**All newly registered users receive the `Employee` role by default.**

## Application Workflow (as Administrator)

### Home Page

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/992db764-fa79-4f7e-ae4b-59edb838b375)

### Employees Section

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/d4694e5e-7808-4dfa-ae6e-ed0a7700aa1b)

### Projects Section

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/a8947e4f-e157-4fbb-a78e-297c5dc99496)

### LeaveRequests Section

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/138b8a9b-4ce2-4ecc-8015-4806cd907c1d)

### ApproveRequests Section

![image](https://github.com/Palazaram/OutOfOffice/assets/108758569/6cd0f479-9612-4118-a3b9-e6959ea6d305)
