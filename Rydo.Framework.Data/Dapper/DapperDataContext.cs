using Dapper;
using Rydo.Framework.Data.Helper;
using Rydo.Framework.Data.Helper.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rydo.Framework.Data.Dapper
{
    public class DapperDataContext : IDapperDataContext
    {
        public void ExecuteNonQueryText(string commandText, IEnumerable<dynamic> parameters)
        {
            using (var conn = DbUtil.Instancia.ObterConnection())
            {
                conn.Execute(commandText, parameters);
            }
        }

        public void ExecuteNonQueryTextAsync(string commandText)
        {
            ExecuteNonQueryTextAsync(commandText, null);
        }

        public void ExecuteNonQueryTextAsync(string commandText, dynamic parameters)
        {
            using (var conn = DbUtil.Instancia.ObterConnection())
            {
                conn.ExecuteAsync(commandText, (object)parameters);
            }
        }

        public IEnumerable<T> ExecuteQueryText<T>(string commandText)
        {
            return ExecuteQueryText<T>(commandText, null);
        }

        public IEnumerable<T> ExecuteQueryText<T>(string commandText, dynamic parameters)
        {
            IEnumerable<T> retorno = new List<T>();

            using (var conn = DbUtil.Instancia.ObterConnection())
            {
                retorno = conn.Query<T>(commandText, (object)parameters);
            }

            return retorno;
        }

        public Task<IEnumerable<T>> ExecuteQueryTextAsync<T>(string commandText)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ExecuteQueryTextAsync<T>(string commandText, dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string commandText, dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteScalarAsync<T>(string commandText)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteScalarAsync<T>(string commandText, dynamic parameters)
        {
            throw new NotImplementedException();
        }
    }
}
