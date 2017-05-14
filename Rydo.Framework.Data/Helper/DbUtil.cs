using Rydo.Framework.Data.Helper.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Rydo.Framework.Data.Helper
{
    public class DbUtil
    {
        private Dictionary<string, DbProviderFactory> mDicionarioProvider;

        protected DbUtil()
        {
            mDicionarioProvider = new Dictionary<string, DbProviderFactory>();
        }

        private IDefinicacaoConnectionString mDefinicacaoConnectionString = null;
        private IDefinicacaoConnectionString DefinicacaoConnectionString
        {
            get
            {
                if (mDefinicacaoConnectionString == null)
                    mDefinicacaoConnectionString = new DefinicacaoConnectionStringAppConfig();

                return mDefinicacaoConnectionString;
            }
        }

        #region Sigleton

        private static object syncLock = new object();

        private static DbUtil mInstancia = null;
        public static DbUtil Instancia
        {
            get
            {
                if (mInstancia == null)
                {
                    lock (syncLock)
                    {
                        if (mInstancia == null)
                        {
                            mInstancia = new DbUtil();
                        }
                    }
                }
                return mInstancia;
            }
        }

        #endregion Sigleton

        public IDbConnection ObterConnection()
        {
            var provider = ObterProvider(DefinicacaoConnectionString);
            var connection = provider.CreateConnection();

            connection.ConnectionString = DefinicacaoConnectionString.ConnectionString;
            connection.Open();

            return connection;
        }

        private DbProviderFactory ObterProvider(IDefinicacaoConnectionString defConn)
        {
            if (!mDicionarioProvider.ContainsKey(defConn.ProviderName))
                mDicionarioProvider.Add(defConn.ProviderName, DbProviderFactories.GetFactory(defConn.ProviderName));

            return mDicionarioProvider[defConn.ProviderName];
        }

    }
}
