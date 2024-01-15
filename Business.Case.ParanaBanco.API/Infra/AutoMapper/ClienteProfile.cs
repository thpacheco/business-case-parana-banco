using AutoMapper;
using Business.Case.ParanaBanco.API.Domain.Dtos;
using Business.Case.ParanaBanco.API.Entities;
using System;

namespace Business.Case.ParanaBanco.API.Infra.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>()
                .ForMember(cliente => cliente.idCliente, opt => opt.Ignore());
        }
    }
}
