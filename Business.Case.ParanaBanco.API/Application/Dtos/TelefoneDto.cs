using Business.Case.ParanaBanco.API.Entities;
using Business.Case.ParanaBanco.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Business.Case.ParanaBanco.API.Application.Dtos
{
    public class TelefoneDto
    {
        [StringLength(2)]
        public string ddd { get; set; }
        
        [StringLength(10)]
        public string numero { get; set; }

        public TipoTelefone tipo { get; set; }
    }
}
