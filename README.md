# FutureOfWorkAPI (C# / .NET 8) – Global Solution 2025-2

API RESTful sobre **Requalificação Profissional** para o tema **O Futuro do Trabalho**.

## ✅ Requisitos Atendidos
- **Boas práticas REST (30 pts)**: Verbos corretos, status codes (`200/201/204/400/404`).  
- **Versionamento (10 pts)**: Rotas com prefixo `/api/v1/...`.  
- **Integração e Persistência (30 pts)**: EF Core + SQLite + Migrations (instruções abaixo).  
- **Documentação (30 pts)**: Swagger habilitado; diagrama arquitetural (Mermaid); roteiro de vídeo.

## 🏗️ Arquitetura (Mermaid)
```mermaid
flowchart LR
    Client[Cliente (Swagger/Postman)] -->|HTTP| Ctrl[Controllers]
    Ctrl --> Svc[Services]
    Svc --> Db[(SQLite via EF Core)]
```

## 🚀 Como Rodar
### Requisitos
- .NET SDK 8
- (Opcional) dotnet-ef:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Passos
```bash
dotnet restore
dotnet build
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
Swagger: `https://localhost:7286/swagger` (ou `http://localhost:5286/swagger`)

## 🔗 Endpoints
- `/api/v1/profissionais` (GET, POST)
- `/api/v1/profissionais/{id}` (GET, PUT, DELETE)
- `/api/v1/cursos` (GET, POST)
- `/api/v1/cursos/{id}` (GET, PUT, DELETE)

## 📦 Publicação (Opcional)
Deploy em Azure/Render/Railway. Alterar connection string se usar SQL Server.
