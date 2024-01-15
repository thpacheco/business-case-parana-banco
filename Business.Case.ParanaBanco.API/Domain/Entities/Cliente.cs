using Dapper.Contrib.Extensions;

namespace Business.Case.ParanaBanco.API.Entities
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        [Write(false)]
        public virtual List<Telefone> Telefones { get; set; }
    }
}
