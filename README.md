# ApiCoreEstudo
API criada para estudo da versão 3.1 do .NetCore 3.1 
- Logging com Elmah.io para acompanhamento (com HealthChecks), 
- HealthChecks também com UI
- Swagger para documentação
- Testes de versionamento de API
- JWT com utilização de roles e claims para validação de acesso

Para a configuração será necessário ter instalado:
- Visual Studio instalado, preferencialmente o 2019.
- SDK 3.1 do Core

# Configuração

1. Clone o projetoS
2. Abra a solution e caso não esteja, defina o DevIO.Api como o seu "Startup Project"
3. Criar um database localdb, o nome que eu utilizei foi "MinhaApiCore", mas pode ser utilizado outro nome, apenas
  lembrar de alterar a ConnectionString dentro do appsettings.json
4. Abrir o Package Manager Console e definir o DevIO.Data como o projeto Default e rode o comando
    
    ``` Update-Database -verbose -Context MeuDbContext ```
    
5. Ainda no Package Manager Console altere o projeto Default para DevIO.Api e rode o comando

    ``` Update-Database -verbose -Context ApplicationDbContext ```

O Passo 4 e 5 irão gerar as tabelas necessárias para o sistema.

6. Basta rodar a aplicação e ele irá abrir na tela do swagger

7. Crie uma nova conta pelo endpoint: /api/v1/nova-conta

8. Para ter acesso aos outros endpoins, será necessáro fazer 3 inserts na tabela: AspNetUserClaims

    8.1 Pegue o UserId que foi gerado no endpoint do passo 7.
    
    8.2 E insira:
    
``` 
ClaimType     ClaimValue
Fornecedor    Adicionar.Atualizar,Excluir
Produto       Adicionar,Atualizar,Excluir
Endereco      Adicionar,Atualizar,Excluir
```
