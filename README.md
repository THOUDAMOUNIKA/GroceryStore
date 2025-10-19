# Grocery Store Application

A complete grocery delivery application built with Angular frontend and ASP.NET Core Web API backend.

## Features

- User registration and authentication
- Browse grocery items with search and category filtering
- Shopping cart functionality
- Delivery slot selection
- Multiple payment methods
- Order management and tracking
- Responsive design with Bootstrap

## Project Structure

```
GroceryStore/
├── BackEnd/           # ASP.NET Core Web API
│   ├── Controllers/   # API Controllers
│   ├── Models/        # Data Models
│   ├── Services/      # Business Logic
│   ├── Data/          # Entity Framework Context
│   └── DTOs/          # Data Transfer Objects
└── FrontEnd/          # Angular Application
    └── src/
        └── app/
            ├── components/    # Angular Components
            ├── services/      # Angular Services
            ├── models/        # TypeScript Models
            ├── guards/        # Route Guards
            └── interceptors/  # HTTP Interceptors
```

## Setup Instructions

### Backend Setup

1. Navigate to the BackEnd directory:
   ```bash
   cd BackEnd
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Update the connection string in `appsettings.json` if needed

4. Run the application:
   ```bash
   dotnet run
   ```

The API will be available at `http://localhost:5000`

### Frontend Setup

1. Navigate to the FrontEnd directory:
   ```bash
   cd FrontEnd
   ```

2. Install npm packages:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   ng serve
   ```

The application will be available at `http://localhost:4200`

## Default Data

The application comes with pre-seeded grocery items including:
- Fresh fruits (Apples, Bananas)
- Dairy products (Milk, Eggs)
- Bakery items (Bread)
- Meat products (Chicken Breast)
- Grains (Rice)
- Vegetables (Tomatoes)

## Technologies Used

### Backend
- ASP.NET Core 8.0
- Entity Framework Core
- JWT Authentication
- BCrypt for password hashing
- SQL Server LocalDB

### Frontend
- Angular 17
- Bootstrap 5
- Font Awesome
- RxJS
- TypeScript

## API Endpoints

- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `GET /api/groceryitems` - Get grocery items (with search/filter)
- `GET /api/groceryitems/categories` - Get categories
- `POST /api/orders` - Create order
- `GET /api/orders` - Get user orders
- `GET /api/orders/delivery-slots` - Get available delivery slots