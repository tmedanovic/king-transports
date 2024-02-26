using KingTransports.TicketingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingTransports.AccountingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public ReportsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("saldo")]
        [Authorize(Policy = "accounting.read")]
        public decimal GetSaldo()
        {
            return _transactionService.GetSaldo();
        }
    }
}
