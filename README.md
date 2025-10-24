# 🏍️ MotoFacilAPI  

## 📌 Projeto  
**Disciplina:** *Advanced Business Development with .NET*  

👤 **Autores**  
- Igor Barrocal – RM555217  
- Cauan da Cruz – RM558238  

---

## 📖 Descrição  

O **MotoFacilAPI** é uma **API RESTful** construída em **.NET 8**, voltada para o gerenciamento de usuários, motos e serviços realizados nas motos.  
A arquitetura segue os princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e **Clean Code**, proporcionando:  

✅ Baixo acoplamento  
✅ Alta manutenibilidade  
✅ Escalabilidade  

---

## ⚙️ Funcionalidades  

- 👥 **Gerenciamento de Usuários** (CRUD completo, entidade rica, Value Object para e-mail)  
- 🏍️ **Gerenciamento de Motos** (CRUD completo, incluindo vínculo com usuário, enum para modelo da moto: `MottuSport`, `MottuE`, `MottuPop`)  
- 🔧 **Gerenciamento de Serviços** realizados nas motos (CRUD completo, regras de reagendamento)  
- 📦 **Validação de dados** via DTOs e entidades  
- 📑 **Documentação interativa** com Swagger/OpenAPI (descrição de endpoints, parâmetros e exemplos)  
- 🗄️ **Persistência de dados** com Entity Framework Core + Migrations  
- 🧩 **Paginação** nos endpoints de listagem (parâmetros `page`, `pageSize`, retorno `totalCount`)  
- 🔗 **HATEOAS** (links de navegação nos retornos das entidades)  
- 🔒 **Boas práticas REST**: status code adequado, payloads claros, uso correto dos verbos HTTP  
- 🚦 **Health Check**: pronto para monitoramento (`GET /health`)  
- 🛡️ **API segura com JWT**  
- 🤖 **Endpoint inteligente com ML.NET** (`POST /api/v1/ml/precisamanutencao`)  
- 🧪 **Testes automatizados unitários e integração (xUnit + WebApplicationFactory)**

---

## 📂 Estrutura do Projeto  
src/

┣ 📂 Api — Controllers REST, validações de entrada  
┣ 📂 Application — Serviços de aplicação, DTOs  
┣ 📂 Domain — Entidades, enums, Value Objects, Interfaces de Repositório  
┗ 📂 Infrastructure — Persistência de dados, repositórios  

---

## 🚀 Tecnologias Utilizadas  

- [.NET 8](https://dotnet.microsoft.com/)  
- **C#**  
- **Entity Framework Core**  
- **Oracle** (configurável via *appsettings.json*)  
- **Swagger/OpenAPI**  
- **ML.NET**  
- **xUnit**  

---

## 🛡️ Autenticação JWT

Antes de acessar os recursos protegidos, obtenha um token JWT:

**Requisição:**
```http
POST /api/v1/auth/login
Content-Type: application/json

{
  "email": "usuario@email.com",
  "senha": "qualquer"
}
```

**Resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiraEm": "2025-10-08T15:00:00Z"
}
```

No Swagger, clique em **"Authorize"** e cole o token no formato:
```
Bearer {token}
```
Agora todos os endpoints protegidos ficarão disponíveis.

---

## 🚦 Health Check

Verifique se a API está online:

```http
GET /health
```
Resposta esperada: **HTTP 200 OK** — ideal para monitoramento (Kubernetes, Azure, etc).

---

## 🤖 Predição ML.NET

Faça uma predição de necessidade de manutenção da moto:

**Requisição:**
```http
POST /api/v1/ml/precisamanutencao
Content-Type: application/json

{
  "quilometragem": 15000,
  "mesesDesdeUltimaRevisao": 14,
  "numeroServicosUltimoAno": 2
}
```

**Resposta:**
```json
{
  "precisaManutencao": true,
  "score": 0.92
}
```

> Observação: atualmente a lógica usa um modelo "dummy" (regra simples) implementado com ML.NET — pronto para trocar por um modelo treinado posteriormente.

---

## 📝 Exemplos de Payloads  

### Criar Usuário
```http
POST /api/v1/usuarios
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao@email.com"
}
```

### Criar Moto
```http
POST /api/v1/motos
Content-Type: application/json

{
  "placa": "ABC1234",
  "modelo": "MottuSport",
  "usuarioId": 1
}
```

### Criar Serviço
```http
POST /api/v1/servicos
Content-Type: application/json

{
  "descricao": "Troca de óleo",
  "data": "2025-09-25T14:00:00Z",
  "usuarioId": 1,
  "motoId": 1
}
```

---

## 🛠️ Como Executar Localmente  

### 1️⃣ Clone o repositório  
```bash
git clone https://github.com/igorbarrocal/MotoFacil-API.git
cd MotoFacil-API
```

### 2️⃣ Configure o banco de dados  
A string de conexão está em `appsettings.Development.json` ou `appsettings.json`.  
Por padrão, está configurado para Oracle:

```json
"ConnectionStrings": {
  "Oracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=RMxxxxxx;Password=xxxxxx;"
}
```
Altere conforme seu ambiente.

> Se não quiser usar Oracle localmente para desenvolvimento/testes, pode trocar o provider (ex.: SQLite/InMemory) no Startup/Program para facilitar execução.

### 3️⃣ Execute as migrations  
```bash
dotnet ef database update
```

### 4️⃣ Rode a API  
```bash
dotnet run --project MotoFacil-API/MotoFacil-API.csproj
```

Acesse o Swagger em:  
```
https://localhost:7150/swagger
```

---

## 🧪 Testes

Para rodar todos os testes (unitários e integração):

```bash
dotnet test
```

Comandos úteis:
- Restaurar pacotes: dotnet restore
- Compilar: dotnet build
- Rodar testes de um projeto específico: dotnet test MotoFacilAPI.Tests

### Testes de integração
- Os testes usam WebApplicationFactory<Program> (Program parcial) para testar endpoints como `/health`.

### Testes unitários
- xUnit + Moq são usados para testes de serviços.  
- Importante: alguns mocks precisam retornar uma Task quando métodos assíncronos são mockados (ex.: AddAsync). Se você tiver um mock que apenas faz Callback, adicione também .Returns(Task.CompletedTask) para evitar await em null.

Exemplo corrigido do teste problemático (MotoServiceTests):
```csharp
mockRepo.Setup(r => r.AddAsync(It.IsAny<Moto>()))
    .Callback<Moto>(m => m.GetType().GetProperty("Id")!.SetValue(m, 123))
    .Returns(Task.CompletedTask);
```

---

## ⚠️ Avisos conhecidos / Como solucionar problemas de NuGet

- NU1603 (ex.: Microsoft.AspNetCore.Mvc.Testing >= 8.0.9) — geralmente ocorre quando NuGet resolve uma patch diferente. Para eliminar o aviso, alinhe a versão no csproj para a versão efetivamente resolvida (ex.: 8.0.10) ou use uma faixa de versão compatível.

- NU1901 — vulnerabilidade reportada em um pacote (ex.: Moq). Recomendações:
  1. Liste pacotes vulneráveis:
     ```bash
     dotnet list package --vulnerable
     ```
  2. Atualize o pacote para uma versão corrigida (se houver):
     ```bash
     dotnet add MotoFacilAPI.Tests package Moq --version <versao-corrigida>
     ```
     ou
     ```bash
     dotnet add MotoFacilAPI.Tests package NSubstitute
     ```
     (caso deseje migrar de framework de mocking)
  3. Depois, rode:
     ```bash
     dotnet restore
     dotnet build
     dotnet test
     ```
  4. Se sua CI falhar por políticas de segurança, escolha uma versão sem advisory ou altere a biblioteca de mocking.

- Se o projeto não compilar:
  - Confira TargetFramework (net8.0) e versões de pacotes (compatíveis com .NET 8).
  - Verifique se o projeto de testes referencia corretamente o projeto principal no csproj.

---

## 📄 Endpoints Principais (resumo)  

Base: /api/v1

### 👥 Usuários  
- GET /usuarios  
- GET /usuarios/{id}  
- POST /usuarios  
- PUT /usuarios/{id}  
- DELETE /usuarios/{id}  

### 🏍️ Motos  
- GET /motos  
- GET /motos/{id}  
- POST /motos  
- PUT /motos/{id}  
- DELETE /motos/{id}  

### 🔧 Serviços  
- GET /servicos  
- GET /servicos/{id}  
- POST /servicos  
- PUT /servicos/{id}  
- DELETE /servicos/{id}  

### 🤖 ML  
- POST /ml/precisamanutencao


- Atualização (ou sugestão) de versão do Moq para eliminar o alerta NU1901, ou migração para NSubstitute.

Quer que eu crie o PR com essas mudanças apontando para a branch `main`?
