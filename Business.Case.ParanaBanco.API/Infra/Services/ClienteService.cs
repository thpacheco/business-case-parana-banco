using Business.Case.ParanaBanco.API.Domain.Interfaces;
using Business.Case.ParanaBanco.API.Infra.Repositories;
using Business.Case.ParanaBanco.API.Entities;
using AutoMapper;
using Business.Case.ParanaBanco.API.Domain.Dtos;
using Business.Case.ParanaBanco.API.Application.Dtos;

namespace Business.Case.ParanaBanco.API.Infra.Services
{
    public class ClienteService : IService
    {
        private readonly ClienteRepository _ClienteRepository;
        private readonly IMapper _mapper;
        public ClienteService(ClienteRepository ClienteRepository, IMapper mapper)
        {
            _ClienteRepository = ClienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cliente>> GetAll() => await _ClienteRepository.GetAll();
        public async Task<ClienteTelefoneDto> GetClientePorDDDouNumeroAsync(string ddd, string numero) => await _ClienteRepository.GetClientePorDDDouNumeroAsync(ddd, numero);
        public async Task<int> InsertCliente(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);

            return await _ClienteRepository.Save(cliente);
        }
        public async Task<bool> UpdateClienteAsync(int id, ClienteUpdateEmailDto clienteUpdateEmailDto)
        {
            var clienteUpdate = await _ClienteRepository.GetById(id);

            clienteUpdate.email = clienteUpdateEmailDto.email;

            var cliente = _mapper.Map<Cliente>(clienteUpdate);

            return await _ClienteRepository.UpdateEmailAsync(cliente);
        }
        public async Task<bool> UpdateClienteTelefoneAsync(int id, ClienteUpdateTelefoneDto updateTelefoneDto)
        {
            var clienteUpdate = await _ClienteRepository.GetByIdTelefone(id);

            clienteUpdate.ddd = updateTelefoneDto.ddd;
            clienteUpdate.numero = updateTelefoneDto.numero;

            var telefone = _mapper.Map<Telefone>(clienteUpdate);

            return await _ClienteRepository.UpdateTelefoneAsync(telefone);
        }
        public async Task<bool> DeleteClienteAsync(string email)
        {
            var clienteDelete = await _ClienteRepository.GetByEmail(email);

            var cliente = _mapper.Map<Cliente>(clienteDelete);

            return await _ClienteRepository.Delete(cliente);
        }
    }
}
