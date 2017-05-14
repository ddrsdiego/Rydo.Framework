using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rydo.Framework.Data.Helper.Contracts
{
    public interface IDapperDataContext
    {
        void ExecuteNonQueryText(string commandText, IEnumerable<dynamic> parameters);

        T ExecuteScalar<T>(string commandText);
        T ExecuteScalar<T>(string commandText, dynamic parameters);

        IEnumerable<T> ExecuteQueryText<T>(string commandText);
        IEnumerable<T> ExecuteQueryText<T>(string commandText, dynamic parameters);

        void ExecuteNonQueryTextAsync(string commandText);
        void ExecuteNonQueryTextAsync(string commandText, dynamic parameters);

        Task<T> ExecuteScalarAsync<T>(string commandText);
        Task<T> ExecuteScalarAsync<T>(string commandText, dynamic parameters);

        Task<IEnumerable<T>> ExecuteQueryTextAsync<T>(string commandText);
        Task<IEnumerable<T>> ExecuteQueryTextAsync<T>(string commandText, dynamic parameters);
    }
}
