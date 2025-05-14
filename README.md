# Agri-Energy Connect – Prototype Web Application  
**Module:** PROG7311  
**Student:** ZINEDEAN SAAIMAN  
**Student No:** ST10348753  
**Date:** 15 May 2025

---

## Overview

Agri-Energy Connect is a prototype web application developed in ASP.NET Core MVC for connecting South African farmers with sustainable, green energy solutions. The platform supports two roles: **Farmers** and **Employees**, allowing them to manage and view agricultural products and farmer profiles respectively.

---

## Technologies Used

- ASP.NET Core MVC (.NET 6)
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap 5 + Custom CSS
- ASP.NET Identity with Role-based Authentication

---

## User Roles & Functionalities

### Farmer Role
- Login via secure Identity portal
- Add new product:
  - Name
  - Category
  - Production date
- View list of all products they’ve created

### Employee Role
- Login via secure Identity portal
- Add new farmer profile:
  - Name
  - Email
  - Location
- View all farmers' products
- Filter by category and date range

---

## Setup Instructions

### 1. Requirements
- Visual Studio 2022 or newer
- .NET 6.0 SDK
- SQL Server Express / LocalDB

---

### 2. Clone & Build the Project

# Clone the repo
git clone https://github.com/ST10348753-ZSAAIMAN/AgriEnergyConnect.git
cd AgriEnergyConnect 

# Or open directly in Visual Studio

### ▶Then:

- **Build the solution**: `Ctrl + Shift + B`
- **Restore NuGet packages** if prompted

---

### 3. Database Setup

Open **Package Manager Console** in Visual Studio and run:

Add-Migration Init
Update-Database

This creates the database and tables using Entity Framework Core.

---

### 4. Run the App

Press `F5` to run the application.

Then:

Go to `/Account/Register` to register as:

- **Farmer** — to add/view products  
- **Employee** — to add farmers & filter products

---

### Pages and Views

| **Page**              | **Role**    | **Functionality**                              |
|-----------------------|-------------|------------------------------------------------|
| `/Account/Login`      | All         | Login interface                                |
| `/Account/Register`   | Employee    | Register new users (with role selection)       |
| `/Product/Create`     | Farmer      | Add new product                                |
| `/Product/MyProducts` | Farmer      | View their products                            |
| `/Farmer/Create`      | Employee    | Add new farmer profile                         |
| `/Product/ViewAll`    | Employee    | View & filter all farmers’ products            |
| `/Home/Index`         | Public      | Landing page with login/register buttons       |

---

### Design & UI

- Custom **hero sections** using farm-themed images  
- Responsive layout using **Bootstrap 5**  
- **Form validation**, flash messages, and clean navigation  
- **Role-based visibility** for links and pages  

---

### Known Limitations

- No edit or delete functionality is implemented  
- ✉Email confirmation flow is scaffolded but not required to use  

---

### Support

For setup issues, please contact:  
`your.email@student.domain`  

Or refer to:  
[Microsoft ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
