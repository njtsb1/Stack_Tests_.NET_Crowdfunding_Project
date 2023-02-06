using AutoMapper;
using System;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Service.AutoMapper
{
    public class CrowdfundingOnLineMappingProfile : Profile
    {
        public CrowdfundingOnLineMappingProfile()
        {   
            CreateMap<Person, PersonViewModel>();
            CreateMap<Donation, DonationViewModel>();
            CreateMap<Eddress, EddressViewModel>();
            CreateMap<Cause, CauseViewModel>();
            CreateMap<CardCredit, CardCreditViewModel>();

            CreateMap<Donation, DonorViewModel>()
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.Personaldata.Name))
                .ForMember(dest => dest.Anonymous, m => m.MapFrom(src => src.Personaldata.Anonymous))
                .ForMember(dest => dest.MessageSupport, m => m.MapFrom(src => src.Personaldata.MessageSupport))
                .ForMember(dest => dest.Value, m => m.MapFrom(src => src.Value))             
                .ForMember(dest => dest.DateTime, m => m.MapFrom(src => src.DateTime));

            CreateMap<PersonViewModel, Person>()
                .ConstructUsing(src => new Person(Guid.NewGuid(), src.Name, src.Email, src.Anonymous, src.MessageSupport));

            CreateMap<CardCreditViewModel, CardCredit>()
                .ConstructUsing(src => new CardCredit(src.NameHolder, src.NumberCardCredit, src.Validity, src.CVV));

            CreateMap<CauseViewModel, Cause>()
                .ConstructUsing(src => new Cause(Guid.NewGuid(), src.Name, src.City, src.State));

            CreateMap<AddressViewModel, Address>()
                .ConstructUsing(src => new Address(Guid.NewGuid(), src.ZipCode, src.TextoAddress, src.Complement, src.City, src.State, src.Telephone, src.Number));

            CreateMap<DonationViewModel, Donation>()
                .ForCtorParam("value", opt => opt.MapFrom(src => src.Value))
                .ForCtorParam("personaldata", opt => opt.MapFrom(src => src.Personaldata))
                .ForCtorParam("formPayment", opt => opt.MapFrom(src => src.FormPayment))
                .ForCtorParam("addressBilling", opt => opt.MapFrom(src => src.AddressBilling));
        }
    }
}