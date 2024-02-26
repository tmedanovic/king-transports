using AutoMapper;
using KingTransports.AccountingService.DTOs;
using KingTransports.AccountingService.Entities;

namespace KingTransports.TicketingService.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Transaction, TransactionDTO>();
        }
    }
}