# Registration App

Welcome to the Registration App! This application provides user registration functionality with email notifications.

## Getting Started

To set up and use the application, please follow these steps:

### 1. Database Configuration

Set up your database connection string in the application configuration:

-   Navigate to the `appsettings.json` file in your project root
-   Locate the `"DefaultConnection"` tag under the `ConnectionStrings` section
-   Update the connection string with your database details

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Your_Database_Connection_String_Here"
    }
}
```

### 2. Email Service Configuration

Configure the email service using environment variables:

1. Open **Environment Variables** on your system:

    - Windows: Search for "Environment Variables" in the Start menu
    - Or go to: System Properties → Advanced → Environment Variables

2. Under **User Variables**, create the following variables:

    - **Variable Name**: `app_email`
    - **Variable Value**: Your email address for sending notifications

    - **Variable Name**: `app_password`
    - **Variable Value**: Your email app password

> **Note**: Make sure to restart your development environment after setting environment variables.

## Prerequisites

-   .NET Core SDK
-   SQL Server
-   Valid email account with app password enabled

## Support

If you encounter any issues during setup, please check your configuration settings or contact the development team.

---

RegistrationApp Team
