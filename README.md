# 🎓 Student Management System (.NET 8 Web API & Console Client)

A robust full-stack backend and client simulation project built with **.NET 8**, featuring a complete RESTful Web API service and a C# Console Client application that performs full CRUD operations and advanced data filtering.

---

## 🚀 Project Architecture
The project is organized into two main parts:
1. **ServiceSide (`StudentsAPI`):** An ASP.NET Core Web API built with controller-based architecture, in-memory data simulation (`clsStudentsDataSumelation`), proper status code handling, and routing.
2. **ClientSideAPI (`ClientSideCodeAPI`):** A C# Console Application acting as a client that consumes the API using `HttpClient` to fetch, create, update, and delete student records.

---

## 🛠️ Technologies Used
* **C# 12 / .NET 8**
* **ASP.NET Core Web API** (`[ApiController]`, `ControllerBase`, `ActionResult`)
* **LINQ** (Filtering, Aggregations like `.Average()`, `.Where()`, `.FirstOrDefault()`)
* **HttpClient & System.Net.Http.Json** for asynchronous REST communication
* **RESTful Status Codes & Routing** (`CreatedAtRoute`, `BadRequest`, `NotFound`, `Ok`)

---

## 📡 API Endpoints Reference (`/api/StudentsAPI`)

| Method | Route | Description | Response Status Codes |
| :--- | :--- | :--- | :--- |
| **GET** | `/api/StudentsAPI/All` | Retrieves all student records. | `200 OK` |
| **GET** | `/api/StudentsAPI/Pass` | Retrieves students with a passing grade ($\ge 50$). | `200 OK` |
| **GET** | `/api/StudentsAPI/AVG` | Calculates and returns the average grade of all students. | `200 OK`, `404 Not Found` |
| **GET** | `/api/StudentsAPI/{id}` | Finds and returns a specific student by ID. | `200 OK`, `400 Bad Request`, `404 Not Found` |
| **POST** | `/api/StudentsAPI` | Adds a new student and returns the created resource location. | `201 Created`, `400 Bad Request` |
| **PUT** | `/api/StudentsAPI/{id}` | Updates an existing student's details by ID. | `200 OK`, `400 Bad Request`, `404 Not Found` |
| **DELETE** | `/api/StudentsAPI/{id}` | Deletes a student record by ID. | `200 OK`, `400 Bad Request`, `404 Not Found` |

---

## 💻 Client Implementation Highlights
The Console Client (`Program.cs`) executes an automated sequence of requests:
* Fetches the full student list.
* Filters passing students and calculates the average grade.
* Tests successful lookups as well as edge cases (such as invalid IDs like `30` to trigger `404 Not Found`).
* Performs asynchronous **POST**, **PUT**, and **DELETE** operations seamlessly using JSON serialization extensions.

---

## ⚙️ How to Run
1. Clone or download this repository.
2. Open the solution in **Visual Studio**.
3. Set **ServiceSide** as the startup project and run it to launch the Web API (note the localhost HTTPS port, e.g., `https://localhost:7002`).
4. Ensure the `HttpClient.BaseAddress` in the Console Client matches your API's running port.
5. Run the **ClientSideAPI** project to observe real-time console logs interacting with the API endpoints.
