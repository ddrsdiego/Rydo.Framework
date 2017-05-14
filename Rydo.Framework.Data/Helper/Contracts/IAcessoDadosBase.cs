using System;
using System.Collections.Generic;
using System.Data;

namespace Rydo.Framework.Data.Helper.Contracts
{
    public interface IAcessoDadosBase
    {
        void ExecuteNonQueryText(string commandText);
        void ExecuteNonQueryText(string commandText, dynamic parameters);

        #region ExecuteQueryText

        IEnumerable<T> ExecuteQueryText<T>(string commandText, Func<IDataReader, T> metodoConversao);
        IEnumerable<T> ExecuteQueryText<T>(string commandText, dynamic parameters);
        IEnumerable<T> ExecuteQueryText<T>(string commandText, dynamic parameters, Func<IDataReader, T> metodoConversao);

        #endregion

        #region ExecuteScalar

        T ExecuteScalar<T>(string commandText, Func<IDataReader, T> metodoConversao);
        T ExecuteScalar<T>(string commandText, dynamic parameters);
        T ExecuteScalar<T>(string commandText, dynamic parameters, Func<IDataReader, T> metodoConversao);

        #endregion ExecuteScalar
    }
}
