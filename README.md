# App-Cozinha

Um sistema web em ASP.NET Core MVC para gestão eficiente de pedidos de restaurante, com autenticação de usuários e controle por setor.

## Descrição

O **App‑Cozinha** permite que garçons registrem pedidos selecionando itens do cardápio, indicando quantidade, mesa e nome do solicitante. Cada pedido é exibido em dashboards separados pelos setores **Cozinha** (pratos) e **Copa** (bebidas), com capacidade de atualizar o status (“Em preparo”, “Pronto”, “Entregue”) em tempo real. O sistema também oferece uma seção de histórico para consulta de todos os pedidos finalizados.

## Funcionalidades

- **Cadastro de Pedidos**  
  - Seleção de produtos do cardápio (pratos e bebidas)  
  - Definição de quantidade, mesa e nome do solicitante  
- **Dashboards por Setor**  
  - Cozinha: exibe apenas pedidos com itens do tipo “Prato”  
  - Copa: exibe apenas pedidos com itens do tipo “Bebida”  
- **Atualização de Status**  
  - Dropdown interativo para alternar entre “Em preparo”, “Pronto” e “Entregue”  
  - Atualização via AJAX para experiência sem recarga de página  
- **Autenticação de Usuários**  
  - Login protegido por cookies e roles (Admin, Cozinha, Copa)  
- **Histórico de Pedidos**  
  - Listagem de todos os pedidos com status “Entregue”  

## Tecnologias

- **Backend**: C#, ASP.NET Core MVC, Entity Framework Core  
- **Banco de Dados**: MySQL (padrão), com migrações EF Core  
- **Frontend**: Bootstrap 5, Bootstrap Icons  
- **Autenticação**: Cookie Authentication com Claims & Roles  

## Instalação e Execução

1. Clone o repositório  
   ```bash
   git clone https://github.com/SEU_USUARIO/app-cozinha.git
   cd app-cozinha

  🛠️ Configuração do Banco de Dados (MySQL via XAMPP)
Siga os passos abaixo para configurar o banco de dados do sistema localmente usando o XAMPP:

1. 🔥 Iniciando o XAMPP
Abra o XAMPP Control Panel.

Inicie os serviços:

✅ Apache

✅ MySQL

2. 💻 Acessando o phpMyAdmin
No navegador, acesse: http://localhost/phpmyadmin

Clique em "Nova" ou "New" no menu lateral esquerdo.

Dê o nome restaurante para o banco de dados e clique em Criar.

3. 📄 Executando o Script SQL
Com o banco restaurante selecionado, vá até a aba "Importar".

Clique em "Escolher arquivo" e selecione o script SQL com nome de ScriptSQL.sql (fornecido neste projeto).

Clique em "Executar" para criar todas as tabelas necessárias:

usuarios

produtos

pedidos

pedidoitens

✅ O script já cria as chaves primárias, relacionamentos e constraints entre as tabelas.

4. 🔗 Conectando o Banco ao Sistema
No seu projeto ASP.NET (C#), configure a string de conexão no appsettings.json:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=restaurante;uid=root;pwd=;"
}

Use uid=root e pwd=; (senha em branco) se você não configurou uma senha para o MySQL no XAMPP.
