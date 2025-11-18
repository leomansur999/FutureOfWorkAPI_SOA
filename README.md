# FutureOfWorkAPI (C# / .NET 8) – SOA & WebServices 2025-2

API RESTful para **Capacitação Profissional** e **Recomendação de Cursos**, alinhada ao tema **O Futuro do Trabalho**.

## Integrantes do Grupo
- Leonardo Mansur – RM551659
- Gabriel Oliveira – RM98565
- Gabriel Riqueto – RM98685

## Objetivo do Projeto
A FutureOfWorkAPI simula um ecossistema de capacitação profissional utilizando arquitetura orientada a serviços (SOA).  
A solução permite cadastrar cursos, profissionais, realizar autenticação JWT, controlar permissões por perfil e recomendar cursos de acordo com a área de interesse do usuário.  
O sistema demonstra separação de responsabilidades, uso de DTOs, Value Objects, Middleware, padrão REST e integração via EF Core.

## Funcionamento
- Autenticação via JWT (`POST /api/v1/Auth/login`)
- Cursos e profissionais organizados em serviços independentes
- Regras de recomendação centralizadas no `RecomendacaoService`
- Tratamento global de exceções com `GlobalExceptionMiddleware`
- Respostas padronizadas via `ApiResponse<T>`
- Arquitetura totalmente stateless (JWT)
- Banco relacional SQLite controlado por EF Core e Migrations

## Requisitos Atendidos
- Arquitetura orientada a serviços (SOA): controllers finos, services independentes
- Value Object aplicado: `AreaInteresseVO` interpreta áreas de interesse
- Caso de uso implementado em serviço: recomendação de cursos
- Padrão de resposta (ResponseEntity)
- Autenticação JWT
- Autorização com Roles (Admin / User)
- Sessão stateless (JWT Bearer)
- Tratamento global de exceções (middleware)
- Documentação automática via Swagger

## Arquitetura (Mermaid)
```mermaid
flowchart LR
    Client["Cliente (Swagger ou Postman)"] -->|HTTP| Ctrl[Controllers]
    Ctrl --> Svc[Services]
    Svc --> VO["Value Object AreaInteresseVO"]
    Svc --> Db["SQLite via EF Core"]
    Ctrl --> Auth["Middleware JWT"]
