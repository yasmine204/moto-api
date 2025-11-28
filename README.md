# 🛵 MotoRentAPI

## 📖 Visão Geral do Projeto

A **MotoRentAPI** é um serviço de backend construído com **ASP.NET Core** (C#) para gerenciar o aluguel e a gestão de motocicletas.

### ⚙️ Tecnologias Principais

- **Linguagem:** C#
- **Framework:** .NET 8 (ASP.NET Core)
- **Banco de Dados:** PostgreSQL
- **Mensageria:** RabbitMQ
- **Containerização:** Docker / Docker Compose

## 🚀 Como Rodar o Projeto

Este guia se concentra na execução da API **localmente**, utilizando o **Docker Compose** apenas para levantar os serviços de dependência (PostgreSQL e RabbitMQ).

### 1. Iniciar Dependências (PostgreSQL e RabbitMQ)

O Docker Compose é usado aqui para fornecer um ambiente de banco de dados e mensageria consistente.

1.  Navegue até o diretório raiz onde se encontra o `docker-compose.yml`:

    ```bash
    cd moto-api
    ```

2.  Inicie o docker
    ```bash
    docker-compose up -d
    ```

### 2. Executar a API

1.  Navegue até a pasta MotoRentAPI

    ```bash
    cd MotoRentAPI
    ```

2.  Rode a aplicação:

    ```bash
    dotnet run
    ```

3.  O Swagger da API estará acessível no endereço exibido no console
    - **Documentação (Swagger):** `http://localhost:[5059]/swagger`
