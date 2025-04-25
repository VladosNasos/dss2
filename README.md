
# 📝 Todo Notebook Application

![dotnet](https://img.shields.io/badge/.NET-6.0-purple?logo=.net)
![license](https://img.shields.io/badge/license-MIT-green)
![build](https://img.shields.io/badge/build-passing-brightgreen)

A **full‑stack demo** built during the *“Object‑Oriented Design & Web Development”* module.  
The finished app lets you **register / log in, create todo lists, add tasks, manage users (admin)**, and exposes an OpenAPI backend.

---

## 📐 Architecture

```
┌────────────┐      Refit + JSON     ┌──────────────┐
│  ASP.NET   │ ───────────────────▶ │ ASP.NET Web  │
│    MVC     │                      │   API 6.0    │
│   Razor    │ ◀─────────────────── │  Controllers │
└────────────┘      HTML / CSS       └──────┬───────┘
        ▲             ▲                      │
        │ Bootstrap 5 │                      │ EF Core 6
        │  + Icons    │             ┌────────▼────────┐
        └─────────────┘             │ SQLite database │
                                     └────────────────┘
```

* **Todo.Web** – MVC front‑end (Bootstrap 5 + Razor)
* **Todo.Web.Api** – REST back‑end (Swagger enabled)
* **Todo.Application / Domain / Infrastructure** – clean architecture with repositories, services & EF Core mappings

---

## 🚀 Quick Start (Local)

```bash
# clone & restore
git clone https://github.com/<your‑user>/todo‑notebook.git
cd todo‑notebook
dotnet restore

# dev certificate for https (optional)
dotnet dev-certs https --trust

# run API   (http://localhost:5000)
dotnet run --project Todo.Web.Api/Todo.Web.Api.csproj

# in NEW terminal – run MVC (http://localhost:5002)
set WEB_API_URL=http://localhost:5000/
dotnet run --project Todo.Web/Todo.Web.csproj
```

Open:

* Swagger UI&ensp;→ `http://localhost:5000/swagger`
* Web‑UI&ensp;→ `http://localhost:5002`

### Default accounts

| Role | User / Pass |
|------|-------------|
| Admin | **admin / admin123** |
| Demo  | **demo  / demo123** |

---

## 🧩 Main Features

| Feature | URL | Method |
|---------|-----|--------|
| **List CRUD** | `/api/todolist` | GET / POST / PUT / DELETE |
| **Task CRUD** | `/api/todotask` | GET / POST / PUT / DELETE |
| **User auth** | `/api/user/ValidatePassword` | POST |

Frontend: Home → *Todo Lists* → *Details* → inline **Add Task**, edit status, delete.

---

## ⚙️ Environment Variables

| Variable | Purpose | Example |
|----------|---------|---------|
| `WEB_API_URL` | Frontend → Backend base URL | `http://localhost:5000/` |
| `ASPNETCORE_ENVIRONMENT` | `Development` / `Production` | Development |

---

## 📦 Migrations

```bash
# add migration
dotnet ef migrations add <Name> --project Todo.Infrastructure/Todo.Infrastructure.csproj
# update db
dotnet ef database update --project Todo.Infrastructure/Todo.Infrastructure.csproj
```

SQLite file is created automatically on first run inside *Todo.Web.Api/bin/…*.

---

## ☁️ Deploy

> **Azure App Service (free tier)**

1. Create two Web Apps: **todo-api** & **todo-web**  
2. Set *todo-web* configuration → `WEB_API_URL=https://todo-api.azurewebsites.net/`  
3. `git push azure main` (or deploy from GitHub Actions).  
4. Done – live demo ready for evaluation 🚀

---

## 👏 Credits

Code skeleton by **@YourTeacher** during class. Todo‑Lists & Todo‑Tasks modules – implemented by **@<your‑name>** (2025).

Licensed under the MIT License.
