# ASP.NET MVC Session & Cookie Demo

An ASP.NET Core MVC application demonstrating two common state-management techniques in web apps: **server-side session state** for authentication and **client-side cookies** for persisting a shopping cart.

## Overview

The app has two independent feature areas:

1. **Account module** — a login/logout flow that uses **ASP.NET Core Session** to track an authenticated user.
2. **Shop module** — a product catalog with an "add to cart" feature that persists cart contents in a **browser cookie** (serialized as JSON).

## Features

### Account (Session-based)
- **Login** (`GET`/`POST`) — accepts email & password; on success, stores them in `HttpContext.Session` and redirects to the home page
- **Home** — a protected page that reads from session; redirects back to `Login` if no active session exists
- **Logout** — clears the session and redirects to `Login`

### Shop (Cookie-based)
- **Products** — displays a list of products (T-shirt, Shoes, Bag)
- **AddToCart** — reads the existing `UserCart` cookie (if any), deserializes it, appends the new product, and re-serializes it into a cookie that expires in 2 days
- **Cart** — reads and displays the current cart from the cookie
- **ClearCart** — deletes the `UserCart` cookie

## Project Structure

```
.
├── Controllers
│   ├── AccountController.cs   # Login / Logout / Home — Session-based auth
│   ├── HomeController.cs      # Default MVC scaffold (Index, Privacy, Error)
│   └── ShopController.cs      # Products / Cart — Cookie-based cart
├── Models
│   ├── Product.cs             # Name, Price
│   └── ErrorViewModel.cs      # Default error page model
├── Views
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Home.cshtml
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   ├── Shop/
│   │   ├── Products.cshtml
│   │   └── Cart.cshtml
│   └── Shared/
└── Properties/
```

## Key Concepts Demonstrated

| Concept | Where | Notes |
|---|---|---|
| **Session state** | `AccountController` | Requires `AddSession()` and `UseSession()` configured in `Program.cs`, plus a backing distributed cache (in-memory by default) |
| **Cookies** | `ShopController` | Manual cookie read/write via `Request.Cookies` / `Response.Cookies.Append`, with a 2-day expiry and JSON serialization of the cart list |
| **Model binding** | `ShopController.AddToCart` | Binds `name` and `price` from query string / form |
| **Redirect patterns** | All controllers | `RedirectToAction` used to enforce login flow and post-action navigation |

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version matching the project's target framework)

## Getting Started

```bash
# Clone the repository
git clone https://github.com/<your-username>/aspnet-mvc-session-cookie-demo.git
cd aspnet-mvc-session-cookie-demo

# Restore dependencies
dotnet restore

# Run the app
dotnet run
```

Then open the URL shown in the console (e.g. `https://localhost:5001`) in your browser.

### Try it out

1. Go to `/Account/Login` and log in with any email/password — you'll be redirected to the home page, which displays your session-stored email.
2. Go to `/Shop/Products` and add a few items to your cart — visit `/Shop/Cart` to see them persisted via cookie, even after refreshing.
3. Click "Logout" to clear your session, or visit `/Shop/ClearCart` to clear the cookie cart.

## Notes / Limitations

- Login does not validate credentials against a real user store — any non-empty email and password are accepted (for demonstration purposes only).
- Credentials are stored in plain text in session — **not** suitable for production use.
- No anti-forgery token validation is shown on the login form in this excerpt; add `[ValidateAntiForgeryToken]` and proper CSRF protections for any real-world deployment.
