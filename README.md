## 🛵 MotoRentAPI

## 📖 Visão Geral do Projeto

A **MotoRentAPI** é um serviço de backend construído com **ASP.NET Core** (C#) para gerenciar o aluguel e a gestão de motocicletas.

### ⚙️ Tecnologias Principais

- **Linguagem:** C#
- **Framework:** .NET 8 (ASP.NET Core)
- **Banco de Dados:** PostgreSQL
- **Mensageria:** RabbitMQ
- **Containerização:** Docker / Docker Compose

## 🚀 Como Rodar o Projeto

Este guia se concentra na execução da API **localmente**, utilizando o **Docker Compose** apenas para levantar os serviços de dependência (**PostgreSQL** e **RabbitMQ**).

### 1. Clonar o Repositório

Antes de tudo, clone o projeto para sua máquina local.

1.  Abra seu terminal e execute:

    ```bash
    git clone https://github.com/yasmine204/moto-api.git
    ```

2.  Navegue até o diretório raiz do projeto:

    ```bash
    cd moto-api
    ```

---

### 2. Iniciar Dependências (PostgreSQL e RabbitMQ)

O Docker Compose é usado para fornecer um ambiente de banco de dados e mensageria consistente.

1.  Certifique-se de que o **Docker** e o **Docker Compose** estejam instalados e em execução.
2.  No diretório raiz (`moto-api`), inicie os serviços de dependência:

    ```bash
    docker-compose up -d
    ```

### 3. Executar a API

1.  Navegue até a pasta da aplicação principal:

    ```bash
    cd MotoRentAPI
    ```

2.  Rode a aplicação usando o .NET CLI:

    ```bash
    dotnet run
    ```

3.  Após a inicialização, a API estará rodando. O console exibirá o endereço, mas o padrão será:
    - **Documentação (Swagger):** `http://localhost:5059/swagger`
