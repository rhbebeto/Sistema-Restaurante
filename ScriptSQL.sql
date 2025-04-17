-- Criar o banco de dados
CREATE DATABASE IF NOT EXISTS restaurante
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE restaurante;

-- Tabela de Usuários
CREATE TABLE IF NOT EXISTS usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Senha VARCHAR(255) NOT NULL,
    Setor VARCHAR(50) NOT NULL -- 'Cozinha', 'Copa', etc
);

-- Tabela de Produtos
CREATE TABLE IF NOT EXISTS produtos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Tipo VARCHAR(50) NOT NULL -- 'Prato' ou 'Bebida'
);

-- Tabela de Pedidos
CREATE TABLE IF NOT EXISTS pedidos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NomeSolicitante VARCHAR(255) NOT NULL,
    Mesa INT NOT NULL,
    DataHora DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(50) NOT NULL -- 'Em preparo', 'Pronto', 'Entregue'
);

-- Tabela de Itens do Pedido
CREATE TABLE IF NOT EXISTS pedidoitens (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PedidoId INT NOT NULL,
    ProdutoId INT NOT NULL,
    Quantidade INT NOT NULL DEFAULT 1,
    FOREIGN KEY (PedidoId) REFERENCES pedidos(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProdutoId) REFERENCES produtos(Id)
);
