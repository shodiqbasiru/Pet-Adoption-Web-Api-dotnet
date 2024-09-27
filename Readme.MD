# PetAdoption API

PetAdoption is an online shop API for pets. This project is built using C# and ASP.NET Core, with Entity Framework Core for database operations.This api provides features to manage accounts, customers, stores, products, orders, and reviews. Users are divided into 3 roles, namely Admin, Customer, and Seller. Admin has full access to all features, Customer can only view and create orders, and Seller can only view and manage products and reviews.

This project is built using the principles of Clean Architecture to ensure maintainability, scalability, and testability. The architecture separates concerns into different layers, such as Presentation (API), Application, Domain (Core), and Infrastructure, allowing for independent development and testing of each layer.

## Features

- **Account management** : User can register, login, and update their account.
- **Store management** : Seller can manage their store information.
- **Product management** : Seller can manage their products, and Customer can view and order products.
- **Order management** : Customer can view and create orders.
- **Review management** : Customer can view and create reviews.

## Technologies Used

- C#
- ASP.NET Core 6.0
- Entity Framework Core
- SQL Server
- JWT

## Getting Started

### Prerequisites

- .NET SDK
- SQL Server

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/shodiqbasiru/PetAdoption.git
   cd PetAdoption
   ```

2. Set up the database:

   - Update the connection string in `PetAdoption.API/appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;User=sa;Password=YourPassword;Database=db_pet_adoption;TrustServerCertificate=True"
     }
     ```

3. Apply migrations:

   ```sh
   dotnet ef database update --project PetAdoption.API
   ```

4. Run the application:
   ```sh
   dotnet run --project PetAdoption.API
   ```

## Usage

### Endpoints

- **Auth**

  - `POST /api/accounts/register` - Register new account customer
  - `POST /api/accounts/register-seller` - Register new account seller
  - `POST /api/accounts/login` - Login

- **Customers**

  - `GET /api/customers` - Get all customers
  - `GET /api/customers/{id}` - Get customer by ID
  - `PUT /api/customers` - Update customer
  - `DELETE /api/customers/{id}` - Delete customer

- **Stores**

  - `GET /api/stores` - Get all stores
  - `GET /api/stores/{id}` - Get store by ID
  - `PUT /api/stores` - Update store
  - `DELETE /api/stores/{id}` - Delete store

- **Products**

  - `GET /api/products` - Get all products
  - `GET /api/products/{id}` - Get product by ID
  - `POST /api/products` - Create new product
  - `PUT /api/products` - Update product
  - `DELETE /api/products/{id}` - Delete product

- **Categories**

  - `GET /api/categories` - Get all categories
  - `GET /api/categories/{id}` - Get category by ID
  - `POST /api/categories` - Create new category
  - `PUT /api/categories` - Update category
  - `DELETE /api/categories/{id}` - Delete category

- **Orders**

  - `GET /api/orders` - Get all orders
  - `POST /api/orders` - Create new order

- **Reviews**
  - `GET /api/reviews` - Get all reviews
  - `POST /api/reviews` - Create new review
  - `PUT /api/reviews` - Update review

## Configuration

### Database
Update the connection string in `PetAdoption.API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;User=sa;Password=YourPassword;Database=db_pet_adoption;TrustServerCertificate=True"
}
```

### JWT

Update the JWT settings in `PetAdoption.API/appsettings.json`:

```json
"Jwt": {
  "Key": "your_jwt_secret_key",
  "Issuer": "your_jwt_issuer"
}
```
