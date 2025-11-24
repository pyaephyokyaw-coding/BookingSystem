# Backend Code Test

## Overview
This project is a **Backend Code Test** implemented using **.NET 8**, **MVC**, **Entity Framework**, **MSSQL**, and **Redis caching**, with background job scheduling using **Hangfire**. The project also implements **JWT authentication** with **Basic Authorization** for pre-login and **Bearer Tokens** for post-login.

---

## Features

- **Framework & Architecture**
  - ASP.NET Core MVC
  - Layered architecture
  - Entity Framework Core (Code First approach)
- **Database**
  - MSSQL
  - Database design with relationships (see diagram below)
- **Caching**
  - Redis distributed cache for high-performance data retrieval
- **Background Jobs**
  - Hangfire schedule support for recurring/background tasks
- **Authentication**
  - Basic Authorization (Before login)
  - JWT Bearer Token (After login)
- **API Documentation**
  - Swagger UI integrated

---

## Architecture Diagram

![Database Design](./Db/BookingSystem_diagram.jpg)
![Database Script](./Db/BCT_BookingSystem_DB.sql)

> The database is designed for optimal relational integrity and performance.

---

## Setup Instructions

### Prerequisites
- .NET 8 SDK
- MSSQL Server
- Redis Server

---

### References
- JwtService = https://youtu.be/oqpNQxEfz_Y
- EntityFramework = https://youtu.be/e1hpjbClopA
- Cache (Redis) = https://thecodebuzz.com/redis-distributed-cache-asp-net-core-csharp-redis-examples/
- Cache (Redis) = https://youtu.be/Tt5zIKVMMbs
- Hangfire = https://www.telerik.com/blogs/creating-background-routines-hangfire-aspnet-core

---

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/pyaephyokyaw-coding/BookingSystem.git backend-code-test
   cd backend-code-test
