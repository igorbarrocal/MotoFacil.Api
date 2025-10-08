# 🏍️ MotoFacilAPI  

## 📌 Projeto  
**Disciplina:** *Advanced Business Development with .NET*  

👤 **Autores**  
- Igor Barrocal – RM555217  a
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

---

## 📄 Endpoints Principais  

### 👥 Usuários  
| Método | Endpoint        | Descrição            |  
|--------|----------------|----------------------|  
| GET    | `/usuarios`    | Listar todos os usuários (com paginação e HATEOAS) |  
| GET    | `/usuarios/{id}` | Buscar usuário por ID (com HATEOAS) |  
| POST   | `/usuarios`    | Criar novo usuário |  
| PUT    | `/usuarios/{id}` | Atualizar usuário |  
| DELETE | `/usuarios/{id}` | Remover usuário |  

### 🏍️ Motos  
| Método | Endpoint     | Descrição           |  
|--------|-------------|---------------------|  
| GET    | `/motos`    | Listar todas as motos (com paginação e HATEOAS) |  
| GET    | `/motos/{id}` | Buscar moto por ID (com HATEOAS) |  
| POST   | `/motos`    | Criar nova moto (modelo, vínculo ao usuário) |  
| PUT    | `/motos/{id}` | Atualizar moto |  
| DELETE | `/motos/{id}` | Remover moto |  

### 🔧 Serviços  
| Método | Endpoint        | Descrição            |  
|--------|----------------|----------------------|  
| GET    | `/servicos`    | Listar todos os serviços (com paginação e HATEOAS) |  
| GET    | `/servicos/{id}` | Buscar serviço por ID (com HATEOAS) |  
| POST   | `/servicos`    | Criar novo serviço (vinculado a uma moto e usuário) |  
| PUT    | `/servicos/{id}` | Atualizar serviço (reagendar data, etc.) |  
| DELETE | `/servicos/{id}` | Remover serviço |  

---

## 📝 Exemplos de Payloads  

### Criar Usuário

```json
POST /usuarios
{
  "nome": "João Silva",
  "email": "joao@email.com"
}
```

### Criar Moto

```json
POST /motos
{
  "placa": "ABC1234",
  "modelo": "MottuSport",
  "usuarioId": 1
}
```

> Modelos válidos: `"MottuSport"`, `"MottuE"`, `"MottuPop"`

### Criar Serviço

```json
POST /servicos
{
  "descricao": "Troca de óleo",
  "data": "2025-09-25T14:00:00Z",
  "usuarioId": 1,
  "motoId": 1
}
```

---

## 📝 Modelos dos Dados (Swagger/OpenAPI)  

Todos os endpoints têm modelos de dados detalhados, exemplos de payloads de requisição e resposta, e parâmetros descritos no Swagger.  
- Acesse [https://localhost:7150/swagger](https://localhost:7150/swagger) após rodar a API.

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

### 3️⃣ Execute as migrations  
```bash
dotnet ef database update
```

### 4️⃣ Rode a API  
```bash
dotnet run
```

Acesse o Swagger em:  
```
https://localhost:7150/swagger
```

---

## 🧪 Testes

Para rodar os testes (se houver):
```bash
dotnet test
```

