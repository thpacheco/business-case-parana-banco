using Business.Case.ParanaBanco.API.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Business.Case.ParanaBanco.API.Domain.Dtos
{
    public class ClienteDto
    {
        [StringLength(20)]
        public string Nome { get; set; }

        [StringLength(20)]
        public string Email { get; set; }
        public IEnumerable<TelefoneDto> Telefones { get; set; }
    }
}
