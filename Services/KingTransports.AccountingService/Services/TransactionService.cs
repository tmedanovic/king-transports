using AutoMapper;
using KingTransports.TicketingService.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using KingTransports.Common.Errors;
using KingTransports.Common.Events;
using KingTransports.AccountingService.DTOs;
using KingTransports.AccountingService.Entities;

namespace KingTransports.TicketingService.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public TransactionService(ITransactionRepository transactionRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactions()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

            return _mapper.Map<List<TransactionDTO>>(transactions);
        }

        public async Task<TransactionDTO> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);

            if (transaction == null)
            {
                throw new NotFound("transaction_not_found");
            }

            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<TransactionDTO> CreateTransactionAsync(CreateTransactionDTO createTransactionDTO)
        {
            Transaction transaction = new Transaction();
            transaction.Transactionid = Guid.NewGuid();
            transaction.Amount = createTransactionDTO.Amount;
            transaction.CreatedAt = createTransactionDTO.CreatedAt;

            await _transactionRepository.CreateTransaction(transaction);

            var tran = _mapper.Map<TransactionDTO>(transaction);
            var tranCreated = _mapper.Map<TransactionCreated>(tran);

            await _publishEndpoint.Publish(tranCreated);

            return tran;
        }

        public decimal GetSaldo()
        {
            return _transactionRepository.GetSaldo();
        }
    }
}
