using AutoMapper;
using FastBudjet.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FastBudjet.Web.Models.AccountsViewModel;
using FastBudjet.Web.TransactionViewModel;

namespace FastBudjet.Web.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            CreateMap<CreateViewModel, Account>();

            CreateMap<Account, AccountViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BalanceStr, opt => opt.MapFrom(src => src.Balance.ToString("c", CultureInfo.CreateSpecificCulture("uz-Latn-UZ"))))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .AfterMap((src, dest) => dest.BalanceStr = dest.BalanceStr.Substring(0, dest.BalanceStr.IndexOf(" soʻm")));


            CreateMap<Transaction, TransactViewModel>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.ToString("c", CultureInfo.CreateSpecificCulture("uz-Latn-UZ"))))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.AccountHistories.FirstOrDefault(x => x.TransactionId == src.Id).Summary.ToString("c", CultureInfo.CreateSpecificCulture("uz-Latn-UZ"))))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.ToString("dd.MM.yyyy")))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Income, opt => opt.MapFrom(src => src.Income))
                .ForMember(dest => dest.SendedOn, opt => opt.MapFrom(src => src.SendedOn.ToString("dd.MM.yyyy")))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))

                .AfterMap((src, dest) => dest.Amount = dest.Amount.Substring(0, dest.Amount.IndexOf(" soʻm")))
                .AfterMap((src, dest) => dest.Summary =  dest.Summary.Substring(0, dest.Summary.IndexOf(" soʻm")));
        }
    }
}
