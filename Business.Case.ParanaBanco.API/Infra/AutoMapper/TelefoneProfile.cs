using AutoMapper;
using Business.Case.ParanaBanco.API.Application.Dtos;
using Business.Case.ParanaBanco.API.Entities;

namespace Business.Case.ParanaBanco.API.Infra.AutoMapper
{
    public class TelefoneProfile : Profile
    {
        public TelefoneProfile()
        {
            CreateMap<Telefone, TelefoneDto>();
            CreateMap<TelefoneDto, Telefone>()
                .ForMember(cliente => cliente.idCliente, opt => opt.Ignore());
        }
    }
}
