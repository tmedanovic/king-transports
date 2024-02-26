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
        public async Task<ActionResult<List<TransactionDTO>>> GetAllTransactions()
        {
            var tickets = await _transactionService.GetAllTransactions();
            return tickets;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "accounting.read")]
        public async Task<ActionResult<TransactionDTO>> GetTransactionById(Guid id)
        {
            var ticket = await _transactionService.GetTransactionById(id);
            return ticket;
        }
    }
}