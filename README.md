# Desafio Backend Senior Easynvest

## Como executar o projeto ?
Precisamos ter o [Docker](https://www.docker.com) instalado na máquina,logo execute os seguintes passos:

1. Abra o console na pasta raiz da aplicação onde baixou o repositorio

2. Execute os testes unitários ```docker-compose -f docker-compose.test.yml up --build```

   ![Image 1](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/1.PNG)
   
   Apos a execucao voce vai ver que os testes unitarios e de integração rodaram.
   
   ![Image 2](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/2.PNG)

3. Execute agora o seguinte comando para que a API seja inicializada ```docker-compose up -d --build```

   ![Image 3](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/3.PNG)
   
   Apos a execução do comando voce vai ver a aplicação rodando.
   
   ![Image 4](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/4.PNG)
   
4. No seu browser acesse a url http://localhost:5000/index.html para acessar a api.

   ![Image 5](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/5.PNG)
   
   Ou se preferir pode acessar na seguinte url http://localhost:5000/api/investimentos.
   
   ![Image 6](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/6.PNG)
   
- Caso precise deletar as imagens e containers, execute o seguintes comandos:
	- ```docker-compose rm -s -f``` (Tirar container parados)
	- ```docker-compose down --rmi all``` (Para e remove os recursos existentes)

## Url da API na nuvem
Podemos tambem acessar a seguinte url para acessar na API : 
- https://desafio-investimentos.herokuapp.com/ 
- ou https://desafio-investimentos.herokuapp.com/api/investimentos

![Image 7](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/7.PNG)

![Image 8](https://github.com/EstebanAdao/Challenge.Easynvest/blob/main/images/8.PNG)
   
## Detalhes sobre a solução

Sobre o design pattern foi usado o Repository pattern para separar a camada de dados com a camada de negocio, ja que vai facilitar em qualquer 
momento mudar o banco de dados por qualquer outro sem impactar tanto o codigo, assim como usar varios bancos simultaneamente isto diminui o acoplamento
entre classes e evita a duplicidade. Desta forma vamos deixar as coisas mais organizadas, melhor definidas, melhor separadas e criaremos uma barreira
entre a aplicação e os dados.

A arquitetura da aplicação esta baseada no "Onion Arquitecture" ou tambem chamado de "Clean Arquitecture", onde usa o principio de inversão de dependencia e principios do DDD.
Esta arquitetura de 4 camadas tem algumas variações adaptadas ao projeto.
Os componentes dela são:
- Application, onde esta a API e as controller.
- Service, onde tem os servicos da aplicação.
- Infrastructure, que é por onde se acessa aos serviços da nuvem e banco de dados.
- Domain, onde contem as interfaces dos serviços, entidades, etc.

Para a parte dos testes foi usado o XUNIT por ser parte do .Net Foundation. Para os testes unitarios foram feitos os testes da camada de Infrastructure,
Application e Service, cobrindo os caminhos princiais e de possiveis falhas no retorno dos dados por parte do endpoint externo.
E na parte dos testes de integração foi implementado o teste para a camada Application.

Para cachear os dados, usei o MemoryCache, que é um cache que fica armazenado na memoria do servidor ja que a solução proposta nao demanda uma estrutura
mais robusta, mas se for o caso daria para usar um cache distribuido como o Redis, que daria para implmentar ele por meio de uma imagem junto com docker-compose. 

O Docker foi escolhido para facilitar o desenvolvimento, teste e implantanção da API em produção independente de ter ou nao instalado as dependencias na maquina,
ja que o aplicativo e suas dependencias estao isoladas o que permite que o aplicativo rode em qualquer sistema operacionl com linux ou windows
, alem que esta solução permite migrar o codigo para qualquer outro servico de cloud.

## Tecnologias que foram utilizadas no projeto:

- [.NET 5](https://docs.microsoft.com/pt-br/dotnet/core/dotnet-five)
	- ASP.NET WebApi Core
- [xUnit](https://xunit.github.io/)
- [Swagger UI](https://swagger.io/swagger-ui/)
- [Docker Container](https://www.docker.com/)


## Informações pessoais

Esteban Adao Mondragón Perea – [@EstebanAdao](https://www.linkedin.com/in/esteban-adao-mondrag%C3%B3n-perea-4294a56b/) – mondragon1669@gmail.com

[https://github.com/estebanadao/github-link](https://github.com/EstebanAdao/)
