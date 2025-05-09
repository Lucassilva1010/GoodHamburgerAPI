# 🍔 Good Hamburger API

API para sistema de pedidos de hamburgueria desenvolvida em ASP.NET Core 8.0

## 📋 Descrição

Este projeto é um backend para sistema de pedidos de uma hamburgueria, com regras de negócio para cálculo automático de descontos conforme combinação de produtos.

## 🚀 Funcionalidades

- ✅ Listagem de produtos (sanduíches e extras)
- ✅ Criação de pedidos com cálculo automático de descontos
- ✅ Validação de regras de negócio
- ✅ Persistência em banco de dados em memória
- ✅ Documentação via Swagger

## 💰 Regras de Desconto

| Combinação de Itens          | Desconto |
|------------------------------|----------|
| Sanduíche + Batata + Refri   | 20%      |
| Sanduíche + Refri            | 15%      |
| Sanduíche + Batata           | 10%      |

## 🛠️ Tecnologias Utilizadas

- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- Swagger/OpenAPI
- In-Memory Database

## 📦 Estrutura do Projeto
GoodHamburgerAPI/
├── Controllers/
│ ├── OrdersController.cs
│ └── ProductsController.cs
├── Data/
│ └── AppDbContext.cs
├── Models/
│ ├── Order.cs
│ ├── OrderItem.cs
│ └── Product.cs
├── Program.cs
└── appsettings.json


## 🚀 Como Executar

1. Clone o repositório
   ```bash
   git clone https://github.com/seu-usuario/good-hamburger-api.git

Abra no Visual Studio 2022+
Execute o projeto (F5)
Acesse a documentação no Swagger:
https://localhost:{porta}/swagger

    https://localhost:{porta}/swagger

🔍 Endpoints
Produtos

    GET /api/products - Lista todos os produtos

    GET /api/products/sandwiches - Lista apenas sanduíches

    GET /api/products/extras - Lista apenas extras

Pedidos

    POST /api/orders - Cria novo pedido

    GET /api/orders - Lista todos os pedidos

    DELETE /api/orders/{id} - Remove um pedido
🤝 Contribuição

Contribuições são bem-vindas! Siga os passos:

    Faça um fork do projeto

    Crie uma branch para sua feature (git checkout -b feature/incrivel)

    Commit suas mudanças (git commit -m 'Adicionando feature incrível')

    Push para a branch (git push origin feature/incrivel)

    Abra um Pull Request

📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.
