using Business.Case.ParanaBanco.API.Application;
using Business.Case.ParanaBanco.API.Domain.Dtos;
using Business.Case.ParanaBanco.API.Infra.Services;
using Microsoft.AspNetCore.Mvc;

namespace Business.Case.ParanaBanco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(ResponseBase.CreateSuccess(await _clienteService.GetAll()));
        }

        [HttpGet("{ddd}/{numero}")]
        public async Task<IActionResult> GetClientePorDDDouNumeroAsync(string ddd, string numero)
        {
            var result = await _clienteService.GetClientePorDDDouNumeroAsync(ddd, numero);
            if (result != null) return Ok(ResponseBase.CreateSuccess(result));
            return Ok(ResponseBase.CreateNotfound());
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] ClienteDto cliente)
        {
            try
            {

                var result = await _clienteService.InsertCliente(cliente);
                if (result > 0) return Ok(ResponseBase.CreateSuccess("Cliente cadastrado com sucesso!"));

                return BadRequest(ResponseBase.CreateError("Erro o criar novo cliente."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBase.CreateError(ex.Message));
            }
        }

        [HttpPut("email/{id}")]
        public async Task<IActionResult> UpdateEmailClienteAsync(int id, [FromBody] ClienteUpdateEmailDto clienteUpdateEmailDto)
        {
            try
            {
                var result = await _clienteService.UpdateClienteAsync(id, clienteUpdateEmailDto);
                if (result) return Ok(ResponseBase.CreateSuccess("Update efetuado com sucesso!"));

                return BadRequest(ResponseBase.CreateError("Erro o criar novo cliente."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBase.CreateError(ex.Message));
            }
        }
        [HttpPut("telefone/{id}")]
        public async Task<IActionResult> UpdateNumeroTelefoneAsync(int id, [FromBody] ClienteUpdateTelefoneDto updateTelefoneDto)
        {
            try
            {
                var result = await _clienteService.UpdateClienteTelefoneAsync(id, updateTelefoneDto);
                if (result) return Ok(ResponseBase.CreateSuccess("Update efetuado com sucesso!"));

                return BadRequest(ResponseBase.CreateError("Erro o criar novo cliente."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBase.CreateError(ex.Message));
            }
        }
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                var result = await _clienteService.DeleteClienteAsync(email);
                if (result) return Ok(ResponseBase.CreateSuccess("Registro excluído com sucesso!"));

                return BadRequest(ResponseBase.CreateError("Erro ao excluindo cliente."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBase.CreateError(ex.Message));
            }
        }
    }
}
