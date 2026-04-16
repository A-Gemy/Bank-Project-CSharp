# Bank-Project-CSharp

A console-based Bank Management System built with C# and Object-Oriented Programming principles.  
The project manages clients, users, transactions, login sessions, permissions, and transfer logging using text files for persistent storage.

## Features

### Client Management
- Show client list
- Add new client
- Delete client
- Update client information
- Find client by account number

### User Management
- Show users list
- Add new user
- Delete user
- Update user
- Find user
- Permissions-based access control

### Transactions
- Deposit
- Withdraw
- Show total balances
- Transfer between clients
- Transfer log screen

### Authentication & Access
- Login screen
- Logout flow
- Lock system after 3 failed login attempts
- Current logged-in user displayed in screen headers
- Current date displayed in screen headers
- Login register logging
- Access denied handling for unauthorized users

## Technologies Used
- C#
- .NET Console Application
- Object-Oriented Programming (OOP)
- File Handling
- Text Files for Data Persistence

## Project Structure

```text
Bank-Project-CSharp
│── Program.cs
│── Global.cs
│── Bank-Project-CSharp.csproj
│
├── Core
│   │── clsPerson.cs
│   │── clsBankClient.cs
│   │── clsUser.cs
│   │── Clients.txt
│   │── Users.txt
│   │── LoginRegister.txt
│   │── TransferLog.txt
│
├── Screens
│   │── clsScreen.cs
│   │── clsMainScreen.cs
│   │── clsLoginScreen.cs
│   │
│   ├── Clients
│   │   │── clsShowClientsListScreen.cs
│   │   │── clsAddNewClientScreen.cs
│   │   │── clsDeleteClientScreen.cs
│   │   │── clsUpdateClientScreen.cs
│   │   │── clsFindClientScreen.cs
│   │
│   ├── Transactions
│   │   │── clsTransactionsScreen.cs
│   │   │── clsDepositScreen.cs
│   │   │── clsWithdrawScreen.cs
│   │   │── clsTotalBalancesScreen.cs
│   │   │── clsTransferScreen.cs
│   │   │── clsTransferLogScreen.cs
│   │
│   ├── Users
│       │── clsManageUsersScreen.cs
│       │── clsShowUsersListScreen.cs
│       │── clsAddNewUserScreen.cs
│       │── clsDeleteUserScreen.cs
│       │── clsUpdateUserScreen.cs
│       │── clsFindUserScreen.cs
```

## Data Storage

The project currently uses text files instead of a database.

### Clients File

**Path:**

`Core/Clients.txt`

**Format:**

`FirstName#//#LastName#//#Email#//#Phone#//#AccountNumber#//#PinCode#//#Balance`

**Example:**

`Ahmed#//#Gamal#//#ahmed@email.com#//#0100000000#//#A100#//#1234#//#5000`

### Users File

**Path:**

`Core/Users.txt`

**Format:**

`FirstName#//#LastName#//#Email#//#Phone#//#UserName#//#Password#//#Permissions`

### Login Register File

**Path:**

`Core/LoginRegister.txt`

**Format:**

`DateTime#//#UserName#//#Permissions`

### Transfer Log File

**Path:**

`Core/TransferLog.txt`

**Format:**

`DateTime#//#SourceAccount#//#DestinationAccount#//#Amount#//#SourceBalance#//#DestinationBalance#//#UserName`

## Permissions

The system supports permissions using integer flags.

**Current permissions:**
- `pListClients = 1`
- `pAddNewClient = 2`
- `pDeleteClient = 4`
- `pUpdateClient = 8`
- `pFindClient = 16`
- `pTransactions = 32`
- `pManageUsers = 64`
- `pShowLoginRegister = 128`
- `pAll = -1`

## How to Run

1. Open the project in Visual Studio.
2. Build the solution.
3. Run the application.
4. Log in using a valid user from `Core/Users.txt`.
5. Use the menu system to navigate through clients, users, and transactions.

## Notes

- The project uses `AppContext.BaseDirectory`, so data files must exist under the runtime `Core` folder beside the executable.
- The application is file-based and does not require SQL Server or any external database.
- Transfer operations are logged to `TransferLog.txt`.
- Successful logins are logged to `LoginRegister.txt`.
- The system locks after 3 failed login attempts.

## Version

Current milestone: **v1.0**