# KingTransports

KingTrasports is a simple example of a event-driven microservices arhitecture written in .NET 7

## Features

- Authentication and authorization (Identity server 4)
- API Gateway (Ocelot) with Consul service discovery
- Communication with MassTransit using RabbitMQ as message broker
- Entity framework code-first using PostgreSQL
- Global error handling and logging filter

## Running the project

KingTransports requires Docker and Docker compose for running PostgreSQL, RabbitMQ and Consul.

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

## Point of interest (pub/sub in action)

Calling `Ticketing / Create Ticket` in Ticketing postman collection should increase saldo in accounting service - `Accounting / Get saldo`

## Known issues

* ~~Consul not deregistering services~~ (fixed in [49484ca](https://github.com/tmedanovic/king-transports/commit/49484cab1007555e43f39296d42855f0b2c0801f)

## Missing features

* RBAC
* ~~MassTransit Transactional Outbox~~ (fixed in [61d1c3b](https://github.com/tmedanovic/king-transports/commit/61d1c3b20a2efa4c9252409eb74b2bdb98236c0b)
* Unique constraints and indexes
* Tenants
* Paging
* Caching
* Concurrency

