using AutoMapper;
using KingTransports.AccountingService.DTOs;
using KingTransports.AccountingService.Entities;
using KingTransports.Common.Events;

namespace KingTransports.TicketingService.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, TransactionCreated>();
        }
    }
}