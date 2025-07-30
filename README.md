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

## 🧩 Solution Architecture

<pre> +--------------------+ Kafka +------------------------+ SignalR / API +-----------------------+ | SensorSimulator | -----------------------> | Monitoring.Api | -------------------------------> | Monitoring.Client | | (Producer) | | (Consumer + DB + Hub) | | (HTML + JS + Chart) | +--------------------+ +------------------------+ +-----------------------+ | v +-----------+ | PostgreSQL | +-----------+ </pre>

## 📁 Project Structure
IoTMonitoring/
│
├── client/ # Web client (index.html, Chart.js, SignalR)
│ └── index.html
│
├── src/
│ ├── Monitoring.Api/ # ASP.NET Core Web API
│ │ ├── Data/ # EF Core DbContext
│ │ ├── Hubs/ # SignalR Hub
│ │ ├── Migrations/ # EF Core migrations
│ │ ├── Models/ # SensorData model
│ │ ├── Services/ # Kafka consumer service
│ │ └── appsettings.json
│ │
│ └── SensorSimulator/ # Kafka producer (console app)
│ └── Program.cs
│
├── docker-compose.yml # Docker Compose file for services
└── README.md # Project documentation


---

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



