# 📚 Library Management Test – Full Solution

This repository contains the complete implementation for **Part 2** (C# API + System Health Monitor)  
and **Part 4** (React Frontend) of the Software Engineer Test.

---

## 🏗️ Project Structure

AddplayApp/

├── AddplayApp.Api/ → .NET 8 Web API (SQLite + EF Core + Bogus)

├── AddplayApp.HealthMonitor/ → .NET 8 Console App (Serilog)

└── my-react-app/ → React Frontend (Vite + Axios)

## ⚙️ Environment

| Tool | Version | Purpose |
|------|----------|----------|
| **.NET SDK** | 8.x | Backend API & console apps |
| **Node.js** | ≥ 18 | React frontend |
| **SQLite** | Built-in | Lightweight database |
| **Vite** | 5 or 6 | React dev server / bundler |

## 🧩 1️⃣ Library.Api (.NET 8 Web API)

### **Endpoints**

| Method | Route | Description |
|---------|--------|-------------|
| `POST` | `/api/users/create-user` | Creates a single user |
| `POST` | `/api/users/create-bulk-users` | Inserts 10 000 random users using Bogus |
| `GET`  | `/api/users/fetch-users` | Returns all users (cached 5 min) |

### **Technologies**
- **Entity Framework Core + SQLite** for persistence  
- **Bogus** for data generation  
- **IMemoryCache** for lightweight caching  
- **Swagger UI** for API exploration  

### **Run locally**

```bash
cd AddplayApp.Api
dotnet restore
dotnet run
```

The API will start (typically) at https://localhost:7176
and automatically create users.db.

Enable CORS
CORS is configured to allow the React frontend:
```
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", p =>
        p.WithOrigins("http://localhost:5175")
         .AllowAnyHeader()
         .AllowAnyMethod());
});
app.UseCors("AllowFrontend");
```

## 🧠 2️⃣ AddplayApp.HealthMonitor (.NET 8 Console App)

Logs CPU and memory usage every 10 seconds.

Run
```
cd AddplayApp.HealthMonitor
dotnet restore
dotnet run
```

Output

A rolling log file testlog-20251108.log is created in the project directory:
```
2025-11-08 22:30:10 [INF] CPU = 15.4 %, FreeRAM = 6721 MB
```

## ⚛️ 3️⃣ my-react-app (Vite + React + Axios)
Features

Displays all users in a table

Provides a form to create new users

Connects to the .NET API via Axios

Folder Layout
```
src/
├── api/
│   └── userApi.js
├── components/
│   ├── CreateUserForm.jsx
│   └── UserTable.jsx
├── App.jsx
└── main.jsx
```
Setup & Run
```
cd my-react-app
npm install
npm run dev
```

#✅ How to Run Everything Together

1. Start the API
```
cd AddplayApp.Api
dotnet run
```
2.Start the React App
```
cd my-react-app
npm run dev
```
Open http://localhost:5173

You’ll see:

a user creation form

a table listing all users from SQLite

