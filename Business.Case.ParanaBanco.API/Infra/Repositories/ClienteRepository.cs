using Business.Case.ParanaBanco.API.Infra.Base;
using Business.Case.ParanaBanco.API.Infra.Context;
using Business.Case.ParanaBanco.API.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Business.Case.ParanaBanco.API.Application.Dtos;

namespace Business.Case.ParanaBanco.API.Infra.Repositories
{
    public class ClienteRepository : IRepository
    {
        public readonly dbBusinessCaseContext _context;

        public ClienteRepository(dbBusinessCaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            string sql = "Select b.*,ba.* from Cliente b inner join Telefones ba on ba.idCliente = b.idCliente";

            var clienteDictionary = new Dictionary<int, Cliente>();

            return await _context.Connection.QueryAsync<Cliente, Telefone, Cliente>(sql, (cliente, telefones) =>
              {
                  if (!clienteDictionary.TryGetValue(cliente.idCliente, out Cliente cliEntry))
                  {
                      cliEntry = cliente;
                      cliEntry.Telefones = new List<Telefone>();
                      clienteDictionary.Add(cliEntry.idCliente, cliEntry);

                  }
                  if (telefones != null) cliEntry.Telefones.Add(telefones);
                  cliEntry.Telefones = cliEntry.Telefones.Distinct().ToList();

                  return cliEntry;
              }, splitOn: "idCliente");
        }
        public async Task<ClienteTelefoneDto> GetClientePorDDDouNumeroAsync(string ddd, string numero)
        {
            string sql = "Select b.*,ba.* from Cliente b inner join Telefones ba on ba.idCliente = b.idCliente where ba.ddd = @ddd and ba.numero = @numero";

            return await _context.Connection.QueryFirstOrDefaultAsync<ClienteTelefoneDto>(sql, new { ddd, numero });
        }

        public async Task<int> Save(Cliente cliente)
        {
            try
            {
                var idCliente = await _context.Connection.InsertAsync(cliente);

                if (cliente.Telefones != null && cliente.Telefones.Any())
                {
                    cliente.Telefones.ForEach(item =>
                    {
                        item.idCliente = idCliente;
                        try
                        {
                            _context.Connection.InsertAsync(item);
                        }
                        catch (Exception ex)
                        {

                            throw new InvalidOperationException("Ocorreu um erro ao realizar essa operação");
                        }

                    });

                };

                return idCliente;
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao realizar essa operação");
            }
        }
        public async Task<Cliente> GetById(int id)
        {
            return await _context.Connection.GetAsync<Cliente>(id);
        }
        public async Task<Telefone> GetByIdTelefone(int idCliente)
        {
            string sql = "Select * from Telefones where idCliente = @idCliente";

            return await _context.Connection.QueryFirstOrDefaultAsync<Telefone>(sql, new { idCliente });
        }
        public async Task<Cliente> GetByEmail(string email)
        {
            string sql = "Select * from Cliente where email = @email";

            return await _context.Connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { email });
        }
        public async Task<bool> UpdateEmailAsync(Cliente cliente)
        {
            try
            {
                return await _context.Connection.UpdateAsync(cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> UpdateTelefoneAsync(Telefone telefone)
        {
            try
            {
                return await _context.Connection.UpdateAsync(telefone);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> Delete(Cliente cliente)
        {
            try
            {
                return await _context.Connection.DeleteAsync(new Cliente { idCliente = cliente.idCliente});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> CountClientes()
        {
            try
            {
                return await _context.Connection.ExecuteScalarAsync<int>("select count(*) from Clientes");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
