# TcpApp - Sistemas Distribuídos (Uniplac)

Projeto desenvolvido para a disciplina de **Sistemas Distribuídos** da **Uniplac**, com o objetivo de implementar uma aplicação prática de comunicação entre cliente e servidor utilizando **sockets TCP** em **C#** com **Windows Forms**.

## 🎯 Objetivo

Simular uma aplicação distribuída com múltiplos clientes se conectando a um servidor central via protocolo TCP, utilizando interface gráfica para facilitar testes, interações e compreensão do fluxo de dados.

---

## 🖥️ Funcionalidades

### Servidor (`Av02Parte4-Server-UI`)
- Escuta conexões de múltiplos clientes simultaneamente.
- Exibe mensagens recebidas em tempo real.
- Lista usuários conectados.
- Envia mensagens de broadcast (para todos os clientes).
- Envia mensagens privadas para os clientes selecionados
- Lida com comandos específicos recebidos dos clientes.

### Cliente (`Av02Parte4`)
- Interface para digitar o nome do usuário e se conectar ao servidor.
- Exibe mensagens recebidas do servidor.
- Envia mensagens ao servidor (privadas ou comandos).
- Lista todos os usuários conectados no momento.

---

## 🔌 Comunicação TCP

A comunicação é feita utilizando **System.Net.Sockets**, onde:

- O **servidor** escuta conexões via `TcpListener`.
- Cada **cliente** se conecta usando `TcpClient`.
- Cada cliente que se conecta é manipulado em **uma thread separada**, permitindo múltiplas conexões simultâneas.
- A troca de mensagens usa codificação **UTF-8** com buffers fixos.

---

## ⚙️ Estrutura do Projeto

```plaintext
TcpApp-Sistemas-Distribuidos-Uniplac/
│
├── Av02Parte04.sln                # Solução principal
│
├── Av02Parte4-Server-UI/         # Projeto do Servidor
│   ├── Form1.cs                  # Interface principal do servidor
│   ├── TcpServer.cs              # Lógica principal de controle do servidor TCP
│
├── Av02Parte4/                   # Projeto do Cliente
│   ├── Form1.cs                  # Interface principal do cliente
│   ├── TcpClientHandler.cs       # Lógica do cliente para conexão e troca de mensagens
```
---
# Como Executar Localmente

## Clone o repositório:

```bash
git clone https://github.com/Ehmaines/TcpApp-Sistemas-Distribuidos-Uniplac.git
```

### Abra a solução no Visual Studio
- Arquivo: Av02Parte04.sln

### Execute primeiro o servidor:

- Compile e inicie o projeto Av02Parte4-Server-UI.

- Clique em "Start" para escutar conexões na porta especificada.

### Execute um ou mais clientes:

- Compile e inicie o projeto Av02Parte4.

### Envie mensagens e veja a interação.
### OBS: Porta de comunicação TCP: 8080
---
## Considerações Finais

### Este projeto demonstra conceitos como:

- Multithreading

- Sincronização de UI com Invoke

- Comunicação TCP

- Modelagem básica de mensagens

- Interação assíncrona

