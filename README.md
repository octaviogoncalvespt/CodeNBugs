# <B>Projecto CLOUD-78343/2024</B>

## Informação do Projeto

Projeto final da Academia de Desenvolvimento de Software da Rumos. Desenvolvido com .net 8.0 no Visual Studio 2022.

O projeto CLOUD-78343/2024 consiste no desenvolvimento de uma solução de software cloud para entusiastas em botânica que inclui a venda online de flores, outras plantas e produtos para seu cultivo.

Funcionalidades do projecto:

– Área de BackOffice para gestão de utilizadores, categorias, artigos e encomendas.  
– Área de “desafio” onde será colocada uma foto por dia de uma planta ou flor e os utilizadores são convidados identificar a planta ou flor. <-- <B>Não implementado</B>  
– Área para upload de foto livre onde os utilizadores pedem ajuda à comunidade para identificar a flor ou planta.  
– Compra online de plantas, flores ou outros produtos para o cultivo.  
• Carrinho de compras.  
• Sistema de envio de encomendas para uma empresa que será responsável pela entrega das encomendas <-- <B>Não implementado</B>

<B>O projeto está divido em 4 camadas:</B>

– PlantEShop - Interface dos utilizadores  
– DataAccess - Acesso a dados (Databases)  
– DataHub - Acesso a dados (Modelos e relações entre tabelas da DB)  
– Repo - Lógica da App

Também foram utilizados os seguintes recursos da cloud (Azure):

–Azure SQL DataBase (Database)  
–Azure WebApp (Host da WebApp)  
–Azure BlobStorage (Armazenamento de imagens)  
–Azure DevOps Pipelines (CI/CD)  
–Docker Image (Deployment em container)  

<B>Utilização</B>
Para facilitação de estudo, foi criada uma conta de Administrador (seeded na DB) com o Login:  
Username: admin@codenbugs.com
Password: Plants@1234?

