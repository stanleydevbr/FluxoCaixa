# Tutorial: Projeto de Demonstração para Habilidades Técnicas de Desenvolvimento de Software 
Este tutorial apresenta um projeto de demonstração que visa destacar algumas habilidades técnicas de desenvolvimento de software. Abaixo estão os detalhes do projeto, incluindo a solução, boas práticas adotadas, frameworks utilizados e instruções para executar o projeto localmente.

## Desenho da Solução
O projeto foi arquitetado de acordo com a seguinte estrutura:

![alt text](desinger.png "Arquitetura")

Importante: A primeira camada do desenho não esta contemplada neste projeto, foi criado apenas camada de backend, sendo possível observar as funcionalidades solicitadas atréves da API. 

## Boas Práticas
Para garantir a qualidade do código e a organização do projeto, foram adotados os seguintes padrões de projeto e práticas:

### Padrões de Projeto
  * **Repository**: Utilizado para separar a lógica de acesso a dados do restante do código.
  * **Adapter**:  Implementado para adaptar interfaces e componentes de forma flexível.

## Modelagem e Boas Práticas de Software
* **Domain Driven Design (DDD)**: Elementos do DDD foram incorporados para melhor organizar a estrutura do projeto.

## Frameworks
Vários frameworks foram empregados para simplificar o desenvolvimento:
* **Entity Framework**: Utilizado para o mapeamento objeto-relacional e gerenciamento de dados.
* **Fluent Validation**: Implementado para validação de entradas de forma robusta.
* **AutoMapper**: Usado para mapear automaticamente objetos DTO (Data Transfer Objects) para modelos de domínio.

## Base de Dados
O projeto utiliza o banco de dados SQLite devido à sua natureza embutida e facilidade de configuração.

## Instruções para Execução do Projeto Local
Siga estas etapas para executar o projeto em sua máquina local:

1. Clonar o Repositório:
Abra o terminal e execute o seguinte comando para clonar o repositório do GitHub:
```bash
  git clone https://github.com/stanleydevbr/FluxoCaixa.git
```
2. Abrir o Projeto:
* No Visual Studio 2019/2022, abra a solução diretamente.
* No Visual Studio Code, abra um terminal e navegue até a pasta src/FluxoCaixa.Api, em seguida, execute os seguintes comandos:
```shell
  dotnet restore
  dotnet run
```
3. Acessar a Interface Swagger:
Após executar o projeto, abra um navegador da web e acesse a URL: http://localhost:5000/swagger/index.html

4. Serviços disponíveis: 
* [POST] - Adicionar: http://localhost:5000/FluxoCaixa 
```json
  {
    "lancamento": "2023-08-09",
    "descricao": "Pagamento Serviço S2",
    "tipo": 2,
    "valor": 58.66,
    "id": 8 //TODO: o identificar deve ser controlado manualmente neste projeto
  }
```

* [GET] - Retorna todos os lançamento: http://localhost:5000/FluxoCaixa
```json
[
    {
        "lancamento": "2023-08-08T18:02:55.548",
        "descricao": "Venda do produto V001",
        "tipo": "Credito",
        "valor": 100.0,
        "id": 1
    },
    {
        "lancamento": "2023-08-08T18:02:55.548",
        "descricao": "Venda do produto V002",
        "tipo": "Credito",
        "valor": 150.0,
        "id": 2
    },
    {
        "lancamento": "2023-08-08T18:02:55.548",
        "descricao": "Pagamento do Fonecedor F002",
        "tipo": "Debito",
        "valor": 50.0,
        "id": 3
    },
    {
        "lancamento": "2023-08-09T00:00:00",
        "descricao": "Pagamento do Fonecedor F003",
        "tipo": "Debito",
        "valor": 150.0,
        "id": 4
    },
    {
        "lancamento": "2023-08-09T00:00:00",
        "descricao": "Venda de produto X1",
        "tipo": "Credito",
        "valor": 250.0,
        "id": 5
    },
    {
        "lancamento": "2023-08-09T00:00:00",
        "descricao": "Venda de produto X2",
        "tipo": "Credito",
        "valor": 180.52,
        "id": 6
    },
    {
        "lancamento": "2023-08-09T00:00:00",
        "descricao": "Venda de produto X3",
        "tipo": "Credito",
        "valor": 155.86,
        "id": 7
    },
    {
        "lancamento": "2023-08-09T00:00:00",
        "descricao": "Pagamento Serviço S2",
        "tipo": "Debito",
        "valor": 58.66,
        "id": 8
    }
]
```
* [GET] - Retorna um lançamento pelo identificador: http://localhost:5000/FluxoCaixa/ObterPorId?id=5
```json
  {
    "lancamento": "2023-08-09T00:00:00",
    "descricao": "Venda de produto X1",
    "tipo": "Credito",
    "valor": 250,
    "id": 5
  }
```
* [GET] - Resultado consolidado: http://localhost:5000/FluxoCaixa/Consolidado?data=2023-08-09
Este endpoint permite o usuário obter o resultado consolidado de uma determinada data especificada via parametro:
```json
  {
      "data": "2023-08-09T00:00:00",
      "credito": 586.38,
      "debito": 208.66,
      "saldo": 377.72
  }
```
## Repositório Público
O código-fonte do projeto está disponível no seguinte repositório público do GitHub: https://github.com/stanleydevbr/FluxoCaixa.git