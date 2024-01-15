using Business.Case.ParanaBanco.API.Enums;

namespace Business.Case.ParanaBanco.API.Application.Dtos
{
    public class ClienteTelefoneDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string ddd { get; set; }
        public string numero { get; set; }
        public TipoTelefone tipo { get; set; }
    }
}
