using Business.Case.ParanaBanco.API.Enums;
using Dapper.Contrib.Extensions;

namespace Business.Case.ParanaBanco.API.Entities
{
    [Table("Telefones")]
    public class Telefone
    {
        [Key]
        public int id { get; set; }
        public int idCliente { get; set; }
        public string ddd { get; set; }
        public string numero { get; set; }
        public TipoTelefone tipo { get; set; }
    }
}
