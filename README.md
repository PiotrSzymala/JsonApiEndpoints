# JsonApiEndpoints

Get and synchronize Posts, comments, and users from API: https://jsonplaceholder.typicode.com

## Description

This project provides an API for fetching and synchronizing data with an external service. It's designed to be easy to set up and integrate with existing systems.

## Getting Started

### Dependencies

Listed below are the libraries/frameworks this project uses:

- Microsoft.AspNetCore.OpenApi (7.0.7)
- Microsoft.EntityFrameworkCore (7.0.13)
- Microsoft.EntityFrameworkCore.Tools (7.0.13)
- Newtonsoft.Json (13.0.3)
- Npgsql.EntityFrameworkCore.PostgreSQL (7.0.11)
- RestSharp (110.2.0)
- Swashbuckle.AspNetCore (6.5.0)

### Docker Setup

The project includes a `docker-compose.yml` file which sets up a PostgreSQL database container:

```yml
version: '3.1'

services:
db:
 image: postgres
 restart: always
 environment:
   POSTGRES_DB: uczelnia
   POSTGRES_USER: postgres
   POSTGRES_PASSWORD: YourPassword123
 ports:
   - "5432:5432"
 volumes:
   - my_dbdata:/var/lib/postgresql/data

volumes:
my_dbdata:
```
Run the following command to start the services: ```docker-compose up -d```

# Aim of the project  
This application was written to pass a class exercise in college.
  
# Used technologies
The project was created with:
* C#
* .NET
* Docker
* HTTP REST
