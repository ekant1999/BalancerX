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
![image](https://github.com/user-attachments/assets/91063bc1-bb78-4ce1-aea4-5db290fcb96f)
![image](https://github.com/user-attachments/assets/235ab3dc-0b92-402f-8a70-a1743bde45f3)
![image](https://github.com/user-attachments/assets/b44f4402-bf53-4684-92c3-ab22c70f4660)


