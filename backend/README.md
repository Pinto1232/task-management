# Task Management Backend (ASP.NET Core Web API)

This is a .NET 9 Minimal API scaffolded in the `backend` folder.

## Run (Development)

- Launch with HTTPS profile:

```bash
cd backend
dotnet run --launch-profile https
```

Listens on:
- HTTP: http://localhost:5164
- HTTPS: https://localhost:7195

If you see HTTPS trust issues locally, you may need to trust the dev cert once:

```bash
dotnet dev-certs https --trust
```

## Test

Open in a browser:
- http://localhost:5164/weatherforecast
- or https://localhost:7195/weatherforecast (ignore the cert warning if not trusted yet)

OpenAPI document (for tooling):
- http://localhost:5164/openapi/v1.json

## CORS

CORS is enabled for the Vite dev server origins:
- http://localhost:5173
- http://127.0.0.1:5173

If your frontend runs on a different port or host, update `Program.cs` CORS origins accordingly.

## Notes

- Target framework: net9.0
- Package: Microsoft.AspNetCore.OpenApi
