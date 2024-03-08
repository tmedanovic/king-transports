using KingTransports.AccountingService.DTOs;
using KingTransports.Common.Events;
using KingTransports.TicketingService.Services;
using MassTransit;

namespace KingTransports.AccountingService.Consumers;
public class TicketCreatedConsumer : IConsumer<TicketCreated>
{
    private readonly ITransactionService _transactionService;

    public TicketCreatedConsumer(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task Consume(ConsumeContext<TicketCreated> ticketCreated)
    {
        Console.WriteLine("Consuming ticket created " + ticketCreated.Message.TicketId);

        var transaction = new CreateTransactionDTO()
        {
            Amount = ticketCreated.Message.Price,
            CreatedAt = ticketCreated.Message.IssuedAt
        };

        await _transactionService.CreateTransactionAsync(transaction);
    }
}