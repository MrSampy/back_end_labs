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
2. Build the Docker image for our backend application. Replace name with a suitable name for your image.
```bash
docker build --tag name .
```
3. Once the image is built, you can run the Docker container, mapping ports 5065 and 7292 from your host to port 80 in the container.
```bash
docker run -p 5065:80 -p 7292:80 name
```
4. Access your backend application in a web browser or via API calls: </br>
Web Application: Open a web browser and navigate to http://localhost:5065. </br>
API Endpoint: You can make API requests to http://localhost:7292. </br>
