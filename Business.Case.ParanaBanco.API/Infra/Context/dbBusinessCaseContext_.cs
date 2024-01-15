using Business.Case.ParanaBanco.API.Domain.Static;
using Business.Case.ParanaBanco.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Business.Case.ParanaBanco.API.Infra.Context
{
    public class dbBusinessCaseContext_
    {
        public IDbConnection Connection => new SqlConnection(RuntimeConfig.ConnectStringSqlServer);
    }
}
