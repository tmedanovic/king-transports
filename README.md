# KingTransports

KingTrasports is a simple example of a event-driven microservices arhitecture written in .NET 7

## Features

- Authentication and authorization (Identity server 4)
- API Gateway (Ocelot) with Consul service discovery
- Communication with MassTransit using RabbitMQ as msaage broker
- Entity framework code-first using PostgreSQL
- Filters for global error handling and logging

## Running the project

KingTransports requires Docker and Docker compose for running PostgreSQL, MongodDB, RabbitMQ and Consul.

```
docker-compose -f docker-compose.yml up
```

## Consuming service

You can consume service by importing `King transports.postman_collection.json` into Postman.

**Using authentication:**

![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postman_1.png?raw=true)

Click go to authorization

![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postaman_2.png?raw=true)

Click Get New Access Token

![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postman_3.png?raw=true)

Login using one of test users:

```
        private readonly static List<SampleUser> sampleUsers = new List<SampleUser>()
        {
            new SampleUser()
            {
                Email = "admin@email.com",
                Password = "Administrator!1",
                Roles = new string[] {"admin"}
            },
            new SampleUser()
            {
                Email = "conductor@email.com",
                Password = "Conductor!1",
                Roles = new string[] {"conductor"}
            },
             new SampleUser()
            {
                Email = "ticketseller@email.com",
                Password = "Ticketseller!1",
                Roles = new string[] { "ticket-seller" }
            },
        };
```
![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postman_4.png?raw=true)

Click Use token
