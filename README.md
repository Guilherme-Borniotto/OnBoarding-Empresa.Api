# Onboarding SIG DB1 API

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework](https://img.shields.io/badge/EF%20Core-512BD4?style=for-the-badge&logo=nuget&logoColor=white)
![Status](https://img.shields.io/badge/Status-%20Concluido-green?style=for-the-badge)

Uma API RESTful completa desenvolvida com **.NET 8**, seguindo princípios de **DDD (Domain-Driven Design)**, **Clean Architecture** e boas práticas de mercado.
O projeto foi construído como parte de um desafio técnico com foco em organização, regras de negócio e arquitetura escalável.

---

# 🚀 Stack Tecnológico

## Backend

| Tecnologia            | Propósito              |
| --------------------- | ---------------------- |
| .NET 8 Web API        | API RESTful            |
| Entity Framework Core | ORM (Code First)       |
| SQL Server            | Banco de dados         |
| AutoMapper            | Mapeamento de objetos  |
| FluentValidation      | Validações             |
| Swagger               | Documentação e testes  |
| Dependency Injection  | Injeção de dependência |
| Unit of Work          | Controle de transações |

---

# 🧱 Arquitetura

O projeto segue **Clean Architecture + DDD**, com separação clara de responsabilidades:

```
OnboardingSIGDB1/
├── src/
│   ├── OnboardingSIGDB1.Domain/
│   ├── OnboardingSIGDB1.Application/
│   ├── OnboardingSIGDB1.Infrastructure/
│   ├── OnboardingSIGDB1.API/
│   └── OnboardingSIGDB1.IoC/
|
└── OnboardingSIGDB1.sln
```

### 🔄 Dependências

```
Domain ← Application ← Infrastructure
                ↑
               API
```

---

# 📦 Funcionalidades

## 🏢 Empresa

* CRUD completo
* Filtros:

  * Nome (LIKE)
  * CNPJ (exato, sem máscara)
  * Data de fundação (intervalo)

### Regras:

* CNPJ salvo sem máscara
* CNPJ único
* CNPJ válido

---

## 👤 Funcionário

* CRUD completo
* Filtros:

  * Nome (LIKE)
  * CPF (exato, sem máscara)
  * Data de contratação (intervalo)

### Regras:

* CPF salvo sem máscara
* CPF único
* CPF válido
* Funcionário não pode mudar de empresa

---

## 💼 Cargo

* CRUD completo

---

## 🔗 Relacionamentos

### Empresa x Funcionário (1:N)

* Funcionário pertence a uma empresa
* Exclusão de empresa bloqueada se houver vínculo

---

### Funcionário x Cargo (N:N com histórico)

* Um funcionário pode ter vários cargos
* Histórico com data de vínculo
* Não permite cargos repetidos
* Retorna o último cargo vinculado

---

# ⚙️ Pré-requisitos

* .NET 8 SDK
* SQL Server
* EF Core CLI

```bash
dotnet tool install --global dotnet-ef
```

---

# ▶️ Como rodar o projeto

## 1. Clonar repositório

```bash
git clone <url-do-repositorio>
cd OnboardingSIGDB1
```

---

## 2. Configurar Secrets (IMPORTANTE)

Nunca suba dados sensíveis para o GitHub.

```bash
dotnet user-secrets init
```

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=OnboardingSIGDB1;Trusted_Connection=True;TrustServerCertificate=True"
```

```bash
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_SECRETA_AQUI"
```

---

## 3. Configurar DbContext

```csharp
options.UseSqlServer(connectionString);
```

---

## 4. Executar migrations

```bash
dotnet ef migrations add InitialCreate \
  --project src/OnboardingSIGDB1.Infrastructure \
  --startup-project src/OnboardingSIGDB1.API
```

```bash
dotnet ef database update \
  --project src/OnboardingSIGDB1.Infrastructure \
  --startup-project src/OnboardingSIGDB1.API
```

---

## 5. Rodar API

```bash
dotnet run --project src/OnboardingSIGDB1.API
```

---

## 6. Acessar Swagger

```
http://localhost:5000/swagger
```

---

# 🔐 Segurança

* Uso de **User Secrets**
* JWT configurável
* `.gitignore` protegendo arquivos locais

---

# 📌 Padrões Utilizados

* DDD (Domain Driven Design)
* Clean Architecture
* Repository Pattern
* Unit of Work
* Notification Pattern (sem Exceptions no domínio)
* FluentValidation

---

# 🧪 Testes

```bash
dotnet test
```

---

# 🗄️ Banco de Dados

* Utiliza **SQL Server**
* Migrations com EF Core (Code First)

### Exemplo de connection string:

```
Server=localhost;Database=OnboardingSIGDB1;Trusted_Connection=True;TrustServerCertificate=True
```

---

# 📌 Boas Práticas Aplicadas

* Separação de responsabilidades
* Código limpo
* Segurança de dados
* Arquitetura escalável

---

# 👨‍💻 Autor

Projeto desenvolvido por **Guilherme Borniotto** 🚀

---
