# 🔌 IoTMonitoring — IoT Sensor Monitoring Platform with Kafka, PostgreSQL, and SignalR

**IoTMonitoring** is a demonstration IoT platform for collecting, processing, and visualizing data from distributed sensors (temperature and humidity). The system is built using **Apache Kafka**, **PostgreSQL**, **ASP.NET Core**, **SignalR**, and **Docker**, showcasing real-time streaming, data persistence, and interactive visualization.

---

## 🚀 Features

- 📡 Simulates distributed sensors that send JSON data to Kafka.
- 🔄 Consumes Kafka messages in ASP.NET Core and stores them in PostgreSQL.
- 📊 Displays real-time updates on a web interface using SignalR and Chart.js.
- 🕓 Provides historical sensor data via HTTP API for visualization.
- 🐳 Fully containerized with Kafka, Zookeeper, and PostgreSQL.

---


## 🧩 Architecture Overview

| Component            | Role                                                 | Communication                  |
|----------------------|------------------------------------------------------|---------------------------------|
| **SensorSimulator**  | Kafka producer that sends temperature and humidity data as JSON messages | → Kafka                         |
| **Kafka**            | Message broker used to deliver data between services | ↔ SensorSimulator / Monitoring.Api |
| **Monitoring.Api**   | Kafka consumer, saves data to PostgreSQL, sends data to clients via SignalR, exposes Web API | ↔ Kafka / PostgreSQL / SignalR |
| **PostgreSQL**       | Stores historical sensor data                        | ← Monitoring.Api                |
| **Monitoring.Client**| HTML + JavaScript frontend that displays data in real-time and loads history | ← SignalR / Web API            |


## 📁 Project Structure

| Path                                | Description                                      |
|-------------------------------------|--------------------------------------------------|
| `client/`                           | Web client (HTML + Chart.js + SignalR)          |
| └── `index.html`                    | Main frontend file                              |
|                                     |                                                  |
| `src/`                              | Source code for backend services                |
| ├── `Monitoring.Api/`               | ASP.NET Core Web API project                    |
| ├── ── `Data/`                      | Entity Framework Core DbContext                 |
| ├── ── `Hubs/`                      | SignalR hub for real-time updates               |
| ├── ── `Migrations/`                | EF Core migrations                              |
| ├── ── `Models/`                    | SensorData model class                          |
| ├── ── `Services/`                  | Kafka consumer background service               |
| ├── ── `appsettings.json`           | Application configuration                       |
| └── ── `Program.cs`                 | Entry point for the API                         |
|                                     |                                                  |
| └── `SensorSimulator/`             | Kafka producer (console app)                    |
| └── ── `Program.cs`                 | Sends simulated sensor data to Kafka            |
|                                     |                                                  |
| `docker-compose.yml`               | Docker Compose configuration file               |
| `README.md`                        | Project documentation (this file)               |


## ⚙️ Components

| Component           | Purpose                                                                  |
|---------------------|--------------------------------------------------------------------------|
| **Kafka**           | Message broker between sensors and the API                              |
| **Zookeeper**       | Cluster coordination and Kafka management                               |
| **PostgreSQL**      | Stores historical sensor data                                            |
| **SensorSimulator** | Console app that sends sensor JSON data to Kafka                        |
| **Monitoring.Api**  | ASP.NET Core service: Kafka consumer, DB saver, SignalR hub, Web API    |
| **Monitoring.Client**| Web interface: connects to SignalR and renders data with Chart.js      |

---

## 🐳 Quick Start

> Requirements: Docker and .NET 9 SDK

```bash
1. Clone the repository:
git clone https://github.com/your-profile/IoTMonitoring.git
cd IoTMonitoring

2. Start the infrastructure:
```bash
docker-compose up -d

3. Run the sensor simulator:
```bash
cd src/SensorSimulator
dotnet run

4. Run the Web API:
```bash
cd src/Monitoring.Api
dotnet run

5. Open the client:
Open client/index.html in your browser.



