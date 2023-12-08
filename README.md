# StarFoodAPI

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) ![C#](https://img.shields.io/badge/C%23-7.0-blue.svg) ![Docker](https://img.shields.io/badge/Docker-4.20.0-blue.svg) ![MySQL](https://img.shields.io/badge/MySQL-8.0-blue.svg)

**Note: Este projeto está atualmente em desenvolvimento (WIP).**

O Projeto StarFood é um sistema de gerenciamento de restaurantes, projetado para simplificar as operações cotidianas de restaurantes, bistrôs e estabelecimentos de alimentos. Ele fornece uma plataforma completa para gerenciar menus, categorias de pratos, tipos de produtos, variações de produtos e muito mais. O projeto é desenvolvido usando a arquitetura Domain-Driven Design (DDD) e é altamente extensível e personalizável.

## Conteúdo

- [Introdução](#introdução)
- [Tecnologias](#tecnologias)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Executando o Projeto](#executando-o-projeto)
- [Contribuindo](#contribuindo)
- [Licença](#licença)

## Introdução

StarFoodAPI conta com os principais recursos abaixo:

### Gerenciamento de Cardápio:
Adicione, edite e remova pratos do menu do restaurante, incluindo informações detalhadas como nome, descrição, categoria e tipo de produto.

### Categorização de Pratos: 
Organize seus pratos em categorias personalizadas para facilitar a navegação e apresentação do menu.

### Variações de Produtos: 
Configure diferentes variações para pratos, como tamanhos, preços e disponibilidade.

### Restrições de Disponibilidade: 
Defina a disponibilidade de pratos para determinados horários ou dias da semana.

### Restrições de Restaurante: 
Personalize o sistema para atender às necessidades específicas do seu restaurante.

### API Restful: 
Acesse dados e funcionalidades por meio de uma API RESTful para integração com outros sistemas.

## Tecnologias

Liste as principais tecnologias que você usou no projeto. Por exemplo:

- ASP.NET Core
- Entity Framework Core
- MySQL
- Swagger
- Outras mais estão por vim...

## Configuração do Ambiente

Para começar a usar o projeto StarFood, siga estas etapas:

1. Clone o repositório para o seu ambiente de desenvolvimento local.

```shell
   git clone https://github.com/Ricardo-M-Dev/StarFoodWebAPI.git
```

2. Banco de Dados

Certifique-se de que você tenha um servidor MySQL instalado localmente ou em um container via Docker e crie um banco de dados vazio com o nome desejado. Em seguida, atualize a string de conexão no arquivo appsettings.json com suas credenciais:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=seu-banco-de-dados;User=seu-usuario;Password=sua-senha;"
}
```

Configure a string de conexão com o banco de dados no arquivo appsettings.json.

Execute as migrações do Entity Framework Core para criar as migrations:

```shell
Add-Migration NomeDaMigration
```

Em seguida, execute o comando para criar o banco com as entidades previamente configuradas:

```shell
Update-Database
```

Se você preferir usar Docker, você pode executar um contêiner MySQL facilmente. Primeiro, certifique-se de ter o Docker instalado em sua máquina. Em seguida, execute o seguinte comando para iniciar um contêiner MySQL:

```shell
docker run -d --name mysql-name -e MYSQL_ROOT_PASSWORD=sua-senha -e MYSQL_DATABASE=seu-banco-de-dados -p 3306:3306 mysql:latest
```
Isso criará um contêiner MySQL com uma senha root definida e um banco de dados vazio com o nome que você especificou. Depois é só atualizar a string de conexão no arquivo appsettings.json

## Executando o Projeto

Após realizada as configurações de Banco de Dados com sucesso, abra um terminal na raiz do projeto e execute o comando:

```shell
dotnet run
```

Ou simplesmente, abra o projeto no Visual Studio e execute pela IDE.

## Contribuindo

Contribuições são bem-vindas! Se você quiser contribuir para este projeto e melhorá-lo, siga estas etapas:

1. Faça um fork deste repositório.

2. Crie um novo branch com um nome descritivo da sua contribuição:

```shell
git checkout -b sua-contribuicao
```

3. Faça as alterações desejadas no código.

4. Certifique-se de testar suas alterações localmente para garantir que tudo funcione conforme o esperado.

5. Confirme suas alterações com mensagens de commit descritivas:

```shell
git commit -m "Adiciona funcionalidade XYZ"
```

6. Envie suas alterações para o seu fork:

```shell
git push origin sua-contribuicao
```

7. Abra um novo Pull Request para revisão.

O Pull Request será revisado e, se estiver tudo certo, mesclará suas contribuições ao projeto principal. Obrigado por sua ajuda!


## Licença

Este projeto é licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](LICENSE) para obter detalhes.