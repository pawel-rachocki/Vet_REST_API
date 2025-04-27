# Vet_REST_API

A simple REST API for managing animals and their visits in a veterinary clinic shelter.  
The project is based on a static in-memory collection.

---

## Project Description

This application allows you to:
- Manage animals (add, view, edit, delete, search by name)
- Manage visits related to animals (view and add visits)

All data is stored in static collections within the application (no external database).

---

## Technologies

- .NET 8 / ASP.NET Core Web API
- C#
- Postman (for endpoint testing)

---

## Data Models

**Animal**
- `id` (int)
- `name` (string)
- `category` (string)
- `weight` (double)
- `coatColor` (string)

**Visit**
- `id` (int)
- `date` (DateTime)
- `animal` (Animal)
- `description` (string)
- `price` (double)

---

## API Endpoints

| Method | Endpoint                              | Description                               |
|--------|---------------------------------------|-------------------------------------------|
| GET    | `/api/animals`                        | Get all animals                           |
| GET    | `/api/animals/{id}`                   | Get animal by id                          |
| POST   | `/api/animals`                        | Add a new animal                          |
| PUT    | `/api/animals/{id}`                   | Edit animal data                          |
| DELETE | `/api/animals/{id}`                   | Delete animal                             |
| GET    | `/api/animals/search?search=name`     | Search animals by name                    |
| GET    | `/api/animals/{id}/visits`            | Get visits for a specific animal          |
| POST   | `/api/animals/{id}/visits`            | Add a visit for a specific animal         |

---
