using AutoFixture;
using AutoMapper;
using Business.Case.ParanaBanco.API.Application.Dtos;
using Business.Case.ParanaBanco.API.Domain.Dtos;
using Business.Case.ParanaBanco.API.Domain.Static;
using Business.Case.ParanaBanco.API.Entities;
using Business.Case.ParanaBanco.API.Infra.Context;
using Business.Case.ParanaBanco.API.Infra.Repositories;
using Business.Case.ParanaBanco.API.Infra.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Business.Case.ParanaBanco.Teste
{
    public class ClienteSericeTest
    {
        private string _CONNECTSTRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\git\\Business.Case.ParanaBanco\\Business.case.ParanaBanco.DB\\BusinessCaseParanaBanco.mdf;Integrated Security=True";
      
        [Fact]
        public async Task DeveAtualizarClienteTelefoneCorretamente()
        {
            //Arrange
            var dbContextMock = new Mock<dbBusinessCaseContext>(new SqlConnection(_CONNECTSTRING));
            var clienteRepositoryMock = new Mock<ClienteRepository>(dbContextMock.Object);

            var mapperMock = new Mock<IMapper>();
            var clienteService = new ClienteService(clienteRepositoryMock.Object, mapperMock.Object);

            //Act
            int id = 2;
            var clienteDtoMock_ = new ClienteUpdateTelefoneDto
            {
                ddd = "11",
                numero = "979993131",
            };

            var result = await clienteService.UpdateClienteTelefoneAsync(1, clienteDtoMock_);

            // Assert
            Assert.True(result);
            clienteRepositoryMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
            clienteRepositoryMock.Verify(repo => repo.UpdateEmailAsync(It.IsAny<Cliente>()), Times.Once);
        }
        [Fact]
        public async Task DeveAtualizarClienteEmailCorretamente()
        {
            //Arrange
            var dbContextMock = new Mock<dbBusinessCaseContext>(new SqlConnection(_CONNECTSTRING));
            var clienteRepositoryMock = new Mock<ClienteRepository>(dbContextMock.Object);

            var mapperMock = new Mock<IMapper>();
            var clienteService = new ClienteService(clienteRepositoryMock.Object, mapperMock.Object);

            //Act
            int id = 2;
            var clienteDtoMock_ = new ClienteUpdateEmailDto
            {
                email = "exemplo@email.com",
            };

            var result = await clienteService.UpdateClienteAsync(1, clienteDtoMock_);

            // Assert
            Assert.True(result);
            clienteRepositoryMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
            clienteRepositoryMock.Verify(repo => repo.UpdateEmailAsync(It.IsAny<Cliente>()), Times.Once);
        }
        [Fact]
        public async Task DeveInserirClienteCorretamente()
        {
            //Arrange
            var dbContextMock = new Mock<dbBusinessCaseContext>(new SqlConnection(_CONNECTSTRING));
            var clienteRepositoryMock = new Mock<ClienteRepository>(dbContextMock.Object);

            var mapperMock = new Mock<IMapper>();
            var clienteService = new ClienteService(clienteRepositoryMock.Object, mapperMock.Object);

            //Act
            var clienteDtoMock = new Fixture().Create<ClienteDto>();

            var clienteDtoMock_ = new ClienteDto
            {
                Nome = "Exemplo Cliente",
                Email = "exemplo@email.com",
                Telefones = new List<TelefoneDto>
            {
                new TelefoneDto { ddd="11", numero = "123456789",tipo = API.Enums.TipoTelefone.Fixo },
                new TelefoneDto { ddd="12",numero = "987654321",tipo = API.Enums.TipoTelefone.Celular },
                new TelefoneDto { ddd="13",numero = "555111222",tipo = API.Enums.TipoTelefone.Fixo },
                new TelefoneDto { ddd="14",numero = "999888777",tipo = API.Enums.TipoTelefone.Celular },
                new TelefoneDto { ddd="15",numero = "444333222",tipo = API.Enums.TipoTelefone.Fixo }
            }
            };

            var result = await clienteService.InsertCliente(clienteDtoMock_);

            // Assert
            Assert.Equal(1, result);
            clienteRepositoryMock.Verify(repo => repo.Save(It.IsAny<Cliente>()), Times.Once);
        }
        [Fact]
        public async Task GetAll_ReturnsAllClientesAsync()
        {

            // Arrange
            var dbContextMock = new Mock<dbBusinessCaseContext>(new SqlConnection(_CONNECTSTRING));

            var clienteRepositoryMock = new Mock<ClienteRepository>(dbContextMock.Object);

            var mapperMock = new Mock<IMapper>();
            var clienteService = new ClienteService(clienteRepositoryMock.Object, mapperMock.Object);

            //Act
            var result = await clienteService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Cliente>>(result);
        }
        [Fact]
        public async Task GetClientePorDDDouNumeroAsync_ReturnsClienteTelefoneDto()
        {
            string ddd = "14";
            string numero = "9587195543";

            // Arrange
            var dbBusinessCaseContext = new dbBusinessCaseContext(new SqlConnection(_CONNECTSTRING));

            var clienteRepositoryMock = new Mock<ClienteRepository>(dbBusinessCaseContext);

            var mapperMock = new Mock<IMapper>();
            var clienteService = new ClienteService(clienteRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await clienteService.GetClientePorDDDouNumeroAsync(ddd, numero);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ClienteTelefoneDto>(result);
        }

        [Fact]
        public async Task DeleteClienteAsync_DeletesCliente()
        {
            // Arrange
            var dbBusinessCaseContext = new dbBusinessCaseContext(new SqlConnection(_CONNECTSTRING));

            var clienteRepositoryMock = new Mock<ClienteRepository>(dbBusinessCaseContext);

            var mapperMock = new Mock<IMapper>();
            var clienteService = new ClienteService(clienteRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await clienteService.DeleteClienteAsync("test2@example.com");

            // Assert
            Assert.True(result);
        }
    }
}