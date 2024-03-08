using KingTransports.AccountingService.DTOs;
using KingTransports.Common.Events;
using KingTransports.TicketingService.Services;
using MassTransit;

namespace KingTransports.AccountingService.Consumers;
public class TicketRefundedConsumer : IConsumer<TicketRefunded>
{
    private readonly ITransactionService _transactionService;

    public TicketRefundedConsumer(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task Consume(ConsumeContext<TicketRefunded> ticketRefunded)
    {
        Console.WriteLine("Consuming ticket refund " + ticketRefunded.Message.TicketId);

        var transaction = new CreateTransactionDTO()
        {
            Amount = ticketRefunded.Message.Price * -1,
            CreatedAt = DateTime.UtcNow
        };

        await _transactionService.CreateTransactionAsync(transaction);
    }
}