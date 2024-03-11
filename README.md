# KingTransports

KingTrasports is a simple example of a event-driven microservices arhitecture written in .NET 7

## Features

- Authentication and authorization (Identity server 4)
- API Gateway (Ocelot) with Consul service discovery
- Communication with MassTransit using RabbitMQ as message broker
- Entity framework code-first using PostgreSQL
- Global error handling, validation and logging filter
- Angular 17 and postman collection for consuming services

## Running the project

KingTransports requires Docker and Docker compose for running PostgreSQL, RabbitMQ and Consul.

```
docker-compose -f docker-compose.yml up
```

## Consuming services

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
### Using angular 17 client (Code is basic and a bit messy for now!)

```
npm install
ng serve
```
![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/angular_client_home.png?raw=true)

### Using postman

You can consume service by importing `King transports.postman_collection.json` into Postman.

**Using authentication:**

![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postman_1.png?raw=true)

Click go to authorization

![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postaman_2.png?raw=true)

Click Get New Access Token

![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postman_3.png?raw=true)

![alt text](https://github.com/tmedanovic/king-transports/blob/main/Instructions/postman_4.png?raw=true)

Click Use token

## Point of interest (pub/sub in action)

Calling `Ticketing / Create Ticket` in Ticketing postman collection should increase saldo in accounting service - `Accounting / Get saldo`

## Known issues

* ~~Consul not deregistering services~~ - fixed in [49484ca](https://github.com/tmedanovic/king-transports/commit/49484cab1007555e43f39296d42855f0b2c0801f)

## Missing features

* ~~RBAC~~ - example in [a894d01](https://github.com/tmedanovic/king-transports/commit/a894d017541fe95971c0c4597e1dff5a902b3263)
* ~~MassTransit Transactional Outbox~~ - fixed in [61d1c3b](https://github.com/tmedanovic/king-transports/commit/61d1c3b20a2efa4c9252409eb74b2bdb98236c0b)
* Unique constraints and indexes
* Tenants
* ~~Paging~~ - example in [9e82b55](https://github.com/tmedanovic/king-transports/commit/9e82b553b76a8d0c84852c8a05cdf714518ffef7)
* Caching
* Concurrency

