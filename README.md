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
