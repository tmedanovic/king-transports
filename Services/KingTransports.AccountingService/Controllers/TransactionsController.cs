using KingTransports.AccountingService.DTOs;
using KingTransports.TicketingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingTransports.TicketingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Authorize(Policy = "accounting.read")]
        public async Task<ActionResult<List<TransactionDTO>>> GetAllTransactionsAsync()
        {
            var tickets = await _transactionService.GetAllTransactionsAsync();
            return tickets;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "accounting.read")]
        public async Task<ActionResult<TransactionDTO>> GetTransactionByIdAsync(Guid id)
        {
            var ticket = await _transactionService.GetTransactionByIdAsync(id);
            return ticket;
        }
    }
}