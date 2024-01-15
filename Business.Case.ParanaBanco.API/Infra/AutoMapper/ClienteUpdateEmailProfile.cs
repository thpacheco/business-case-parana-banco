using AutoMapper;
using Business.Case.ParanaBanco.API.Application.Dtos;
using Business.Case.ParanaBanco.API.Domain.Dtos;
using Business.Case.ParanaBanco.API.Entities;
using System;

namespace Business.Case.ParanaBanco.API.Infra.AutoMapper
{
    public class ClienteUpdateEmailProfile : Profile
    {
        public ClienteUpdateEmailProfile()
        {
            CreateMap<Cliente, ClienteUpdateEmailDto>();
            CreateMap<ClienteUpdateEmailDto, Cliente>()
                .ForMember(cliente => cliente.idCliente, opt => opt.Ignore());

        }
    }
}
