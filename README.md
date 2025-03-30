# ⚖️ PaxBalancer (C#) – Application-Layer HTTP Load Balancer

**PaxBalancer** is a lightweight, multi-threaded HTTP load balancer written from scratch in **C#**. It distributes incoming HTTP requests across multiple backend servers using a **round-robin algorithm**, and ensures **high availability** through periodic **health checks**.

This project was built to deepen understanding of networking, concurrency, and fault tolerance by implementing all core features manually using .NET sockets and threading.

---

## 🚀 Features

- 🔁 **Round-Robin Request Distribution**  
  Balances load evenly across all healthy backend servers.

- ❤️ **Health Checks**  
  Periodically checks `/health` endpoint on each backend. Unhealthy servers are removed from rotation and re-added when they recover.

- ⚡ **Multi-threaded Client Handling**  
  Each client connection is handled in its own thread, supporting high concurrency.

- 🧪 **Simulated Server Failure**  
  Backend servers can be configured to delay or crash after a number of requests to test load balancer fault handling.

---
# PaxBalancer System

This repository contains a complete setup for a load-balanced server-client system with the following components:

- **PaxBalanceProj** – Launches all server instances in parallel.
- **ServerProject** – Contains the server implementations.
- **LoadBalancer** – Routes client requests to server instances.
- **Client** – Sends requests to the load balancer.

## 🏃‍♂️ Running the Project

### 🧱 Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- Git CLI or any Git GUI

---

### 📥 1. Clone the Repository

- git clone https://github.com/your-username/PaxBalanceProj.git
- cd PaxBalanceProj

### 📥 2. Build All Projects

- dotnet build PaxBalanceProj.sln

### 📥 3. Build ServerProject and Copy exe
- If you built in Debug mode:
    ServerProject/bin/Debug/net6.0/ServerProject.exe
-  If you built in Release mode:
     ServerProject/bin/Release/net6.0/ServerProject.exe
- Copy the full path and update the exePath variable in Program.cs of the PaxBalanceProj.
This is used to launch backend servers in parallel from code.

### 📥 4. Start the Load Balancing System
- Run projects in this order:
   - Run PaxBalanceProj
   - Run LoadBalancer project
   - Run ClientProject

![image](https://github.com/user-attachments/assets/aeb9633d-3a04-46c4-9420-a5f37d6b9d29)
![image](https://github.com/user-attachments/assets/33f00576-5231-4866-ad27-a09e3e795a8e)
![image](https://github.com/user-attachments/assets/86e01c23-5b2c-4c26-9b2b-93b1874e7472)



