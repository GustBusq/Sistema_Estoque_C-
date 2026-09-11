# SistemaEstoque

API REST em **C# / .NET** para gestão de estoque, com arquitetura em camadas (Domain, Data, Application, Api) e persistência em **SQLite** via **EF Core**.

## Arquitetura

```
SistemaEstoque/
├── Domain/
│   └── Models/          # Entidades de domínio (Produto, Estoque)
├── Data/                # Acesso a dados: DbContext + Repositórios (EF Core)
├── Application/         # Regras de negócio (ServiceEstoque)
├── Api/
│   ├── Controllers/     # Endpoints HTTP (ProdutoController, EstoqueController)
│   └── Dto/             # Contratos de entrada/saída da API
└── Program.cs           # Configuração da aplicação e injeção de dependência
```

Fluxo de dependências: `Api` → `Application` → `Data` → `Domain`. Cada camada só conhece a camada abaixo dela.

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (versão compatível com o `TargetFramework` do `.csproj`)

## Como rodar

1. Restaurar os pacotes (inclui EF Core + provider SQLite):
   ```bash
   dotnet restore
   ```
2. Rodar a aplicação:
   ```bash
   dotnet run
   ```
3. O banco SQLite (`app.db`) é criado automaticamente na primeira execução, junto com as tabelas `Produtos` e `Estoque`.

A connection string fica em `appsettings.json`, na chave `ConnectionStrings:Default`.

## Endpoints

### Produtos (`/api/produtos`)

| Método | Rota                          | Descrição                     |
|--------|-------------------------------|--------------------------------|
| GET    | `/api/produtos`                | Lista todos os produtos        |
| POST   | `/api/produtos`                | Cria um novo produto           |
| PUT    | `/api/produtos/{id}/preco`     | Atualiza o preço de um produto |
| PUT    | `/api/produtos/{id}/quantidade`| Atualiza a quantidade          |
| DELETE | `/api/produtos/{id}`           | Exclui um produto              |

### Estoque (`/api/estoque`)

| Método | Rota                | Descrição                                 |
|--------|---------------------|---------------------------------------------|
| GET    | `/api/estoque`       | Lista os registros de estoque (com produto) |
| POST   | `/api/estoque`       | Registra uma entrada de estoque             |
| DELETE | `/api/estoque/{id}`  | Exclui um registro de estoque               |

