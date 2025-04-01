# Web API de Gerenciamento de Usuários e Tarefas

Este projeto foi desenvolvido como parte de um **teste técnico**. O objetivo é demonstrar habilidades no desenvolvimento de APIs RESTful utilizando **ASP.NET Core**, **Entity Framework Core** e boas práticas de arquitetura de software.

## 🚀 Objetivo

Criar uma Web API em ASP.NET Core com Entity Framework para gerenciar usuários e tarefas.

## 🛠 Requisitos

- **.NET 6+** ou superior.
- **Entity Framework Core** com banco de dados em memória (`InMemory`) ou SQL Server.
- Criar os seguintes endpoints:
  - **POST /usuarios**: Criar um usuário.
  - **GET /usuarios/{id}**: Buscar um usuário pelo ID.
  - **GET /usuarios**: Listar todos os usuários.
  - **POST /usuarios/{id}/tarefas**: Criar uma tarefa para um usuário.
  - **GET /usuarios/{id}/tarefas**: Listar todas as tarefas de um usuário.

## 📌 Regras de Negócio

- **Usuário**:
  - O usuário possui os campos **Nome** e **Email**.
  - **Email** deve ser único para cada usuário (validação de duplicidade).
  
- **Tarefa**:
  - Cada tarefa tem os seguintes campos:
    - **Título** 
    - **Descrição** 
    - **Status** (com opções: Pendente, Em Andamento, Concluído).
  
## 📋 Funcionalidades

- **Validações**:
  - Não permitir emails duplicados ao criar usuários.
  
- **Arquitetura**:
  - Utilizar **Dependency Injection** corretamente.
  - Utilizar **Repository Pattern** para acesso ao banco de dados.
  
## ⚠ Diferenciais

- **Autenticação**:
  - Implementação de **Autenticação JWT** para garantir segurança nas requisições.
  
- **Testes**:
  - **Testes unitários** para os serviços e controllers, garantindo maior cobertura e confiabilidade no sistema.
  
- **Documentação da API**:
  - Utilização do **Swagger** para documentar todos os endpoints da API de forma interativa e fácil de entender.
  
- **Cache**:
  - Implementação de **Cache** para melhorar a performance das requisições, reduzindo o tempo de resposta ao consultar dados repetidamente.

## 🔧 Tecnologias Utilizadas

- **ASP.NET Core 6+**: Framework para construção de APIs e serviços.
- **Entity Framework Core**: ORM para acesso e manipulação de dados no banco.
- **JWT**: Autenticação baseada em tokens.
- **Swagger**: Documentação interativa da API.
- **InMemory Database / SQL Server**: Armazenamento dos dados dos usuários e tarefas.
