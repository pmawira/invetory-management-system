# Inventory Management System API
## Overview

#### This is a RESTful API for managing inventory, built with ASP.NET Core, Entity Framework Core, and SQL Server. It supports CRUD operations for products and user authentication with JWT.

##  Prerequisites:

1. **.NET SDK >= 8.0** : Install the .NET Core SDK on your laptop. You can download it from the official .NET website (https://dotnet.microsoft.com/download).
2. **OpenSSL** : Install openssl from this link (https://community.chocolatey.org/packages/OpenSSL) 

--

# Minimum Requirements

The minimum laptop requirements depend on the resource demands of the containers you intend to run and the specific workloads of your .NET API. However, as a general guideline:

- **CPU** : A modern multi-core CPU (e.g., Intel i5 or AMD Ryzen 5) with at least 8 cores is recommended. More cores will provide better performance, especially if you have multiple containers running simultaneously.
- **RAM** : A minimum of 16 GB of RAM is advisable. Containers like SQL Server, Elasticsearch, and Grafana can consume a significant amount of memory. Having enough RAM will prevent performance bottlenecks.
- **Storage** : SSD (Solid State Drive) storage is recommended for better I/O performance, as databases (SQL Server and Elasticsearch) can be I/O intensive. Ensure you have enough storage space for your Docker containers and application code.
- **Operating System** : Running Docker for Linux containers on a Linux-based host (e.g., Ubuntu) can provide better performance and compatibility. However, Docker for Windows can also be used.

# Set up

## Step 1: Install IIS and Required Components
 - Open Control Panel → Programs → Turn Windows features on or off.
- Check Internet Information Services (IIS).
- Expand World Wide Web Services → Application Development Features, and ensure the following are checked:
- .NET Extensibility 4.8
 - ASP.NET 4.8
- ISAPI Extensions
- ISAPI Filters
- Expand Management Tools and check IIS Management Console.
- Click OK and restart your computer if required.

## Step 2: Install ASP.NET Core Hosting Bundle
- Download and install the .NET Hosting Bundle from (https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (Choose Hosting Bundle for your .NET version)

## Step 3: Get application file

- Location:  "git clone https://github.com/pmawira/invetory-management-system.git"
- Go to releases then Inventory.Management.System 1.0.0
- add unzip and copy the folder to C:\inetpub\ in your machine

## Step 4: Configure IIS
1. Open IIS Manager (inetmgr in Run).
2. Right-click Sites → Add Website.
3. Enter:
- Site Name: InventoryAPI
- Physical Path: <where you copied the application>
- Port: 5000 (or any available port)
- Application Pool: Create/select one using .NET CLR Version: No Managed Code
4. Click OK

## Step 5: Configure Firewall
- Open Windows Defender Firewall.
- Click Advanced settings → Inbound Rules.
- Add a New Rule for Port, allowing traffic on 5000.
## Step 6: Restore db
- Open appsettings.js and change serve to your sql server
- Location:  "git clone https://github.com/pmawira/invetory-management-system.git"
- Go to releases then InventoryManagementSystemDb
 and restore
## Step 7: Test with swagger

- Browse the site
- Register a user
- Login to get token
- Add at least one Product category
- To access end point that require authorization, click on the lock icon at the far end.
- Paste the token and click authorize

# Architectural decisions
 - CQRS
 - Repository pattern
 - DI

 # Assumptions

 - Product have unique names
 - Category have unique name
 - Changes sensitive change done by registered users

 # Improvement given more time
  - Implement Authentication use Identity server.
  - Improve performance using redis caching.
  - Implement queueing using rabbitmq
  - Cover more unit tests and integration tests

  # Find source code at:
  - Location: https://github.com/pmawira/invetory-management-system.git
  - Use git clone https://github.com/pmawira/invetory-management-system.git