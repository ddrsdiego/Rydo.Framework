using Rydo.Framework.Data.Helper.Contracts;
using System;
using System.Collections.Generic;
using System.Data;

namespace Rydo.Framework.Data.SqlServer
{
    public class AcessoDadosSqlServer : IAcessoDadosBase
    {
        public void ExecuteNonQueryText(string commandText)
        {
            throw new NotImplementedException();
        }

        public void ExecuteNonQueryText(string commandText, dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteQueryText<T>(string commandText, dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteQueryText<T>(string commandText, Func<IDataReader, T> metodoConversao)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteQueryText<T>(string commandText, dynamic parameters, Func<IDataReader, T> metodoConversao)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string commandText, dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string commandText, Func<IDataReader, T> metodoConversao)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string commandText, dynamic parameters, Func<IDataReader, T> metodoConversao)
        {
            throw new NotImplementedException();
        }
    }
}
