# Backend Labs

# Lab 1

Welcome to the first lab of our backend development series! In this lab, we will set up a Docker container to run our backend application.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- Docker: [Install Docker](https://docs.docker.com/get-docker/)

## Getting Started

To get started with this lab, follow these steps:

1. Clone this repository to your local machine.

```bash
git clone https://github.com/MrSampy/back_end_labs/
cd back_end_labs/Lab1/Lab1
```
2. Build the Docker images defined in your docker-compose.yml file.
```bash
docker-compose build
```
3. Start your application using docker-compose up.
```bash
docker-compose up
```
4. Access your backend application in a web browser or via API calls: </br>
Web Application: Open a web browser and navigate to http://localhost:5065. </br>
API Endpoint: You can make API requests to http://localhost:7292. </br>

5. Or you can access first lab by link https://lab1-n3a9.onrender.com.

# Lab 2

Welcome to the second lab of our backend development series! In this lab, we will set up a Docker container to run our backend application.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- Docker: [Install Docker](https://docs.docker.com/get-docker/)

## Getting Started

To get started with this lab, follow these steps:

1. Clone this repository to your local machine.

```bash
git clone https://github.com/MrSampy/back_end_labs/
cd back_end_labs/Lab2/Lab2
```
2. Build the Docker images defined in your docker-compose.yml file.
```bash
docker-compose build
```
3. Start your application using docker-compose up.
```bash
docker-compose up
```
4. Access your backend application in a web browser or via API calls: </br>
Web Application: Open a web browser and navigate to http://localhost:7261. </br>
API Endpoint: You can make API requests to http://localhost:5031. </br>

5. Or you can access first lab by link https://lab2-1t33.onrender.com.

6. Access postman enviroment using link https://www.postman.com/galactic-water-326171/workspace/lab-2/overview.


# Lab 3

Welcome to the third lab of our backend development series! In this lab, we will set up a Docker container to run our backend application.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- Docker: [Install Docker](https://docs.docker.com/get-docker/)

## Getting Started

<b>Variant: IM-12 -> 12 -> 12 % 3 = 0 -> Облік доходів</b>

To get started with this lab, follow these steps:

1. Clone this repository to your local machine.

```bash
git clone https://github.com/MrSampy/back_end_labs/
cd back_end_labs/Lab3/Lab3
```

1.1.To use local db change db connection string to "PostgreSQLConnection" in Startup.cs
```cs
string connection = Configuration.GetConnectionString("PostgreSQLConnection")!;
```

2. Build the Docker images defined in your docker-compose.yml file.
```bash
docker-compose build
```
3. Start your application using docker-compose up.
```bash
docker-compose up
```
4. Access your backend application in a web browser or via API calls: </br>
Web Application: Open a web browser and navigate to http://localhost:7013. </br>
API Endpoint: You can make API requests to http://localhost:5175. </br>

5. Or you can access first lab by link https://lab3-7kg4.onrender.com.

6. Access postman enviroment using link .
