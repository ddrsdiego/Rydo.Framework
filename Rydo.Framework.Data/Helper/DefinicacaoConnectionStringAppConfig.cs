using Rydo.Framework.Data.Helper.Contracts;
using System;
using System.Configuration;

namespace Rydo.Framework.Data.Helper
{
    public class DefinicacaoConnectionStringAppConfig : IDefinicacaoConnectionString
    {
        private string mConnectionStringName = string.Empty;
        private const string DEFAULTCONNECTIONSTRING = "DefaultConnectionString";

        public DefinicacaoConnectionStringAppConfig()
        {
            mConnectionStringName = ObterConnectionStringName();

            Name = ConfigurationManager.ConnectionStrings[mConnectionStringName].Name;
            ProviderName = ConfigurationManager.ConnectionStrings[mConnectionStringName].ProviderName;
            ConnectionString = ConfigurationManager.ConnectionStrings[mConnectionStringName].ConnectionString;
        }

        public string ConnectionString { get; private set; }
        public string Name { get; private set; }
        public string ProviderName { get; private set; }

        private string ObterConnectionStringName()
        {
            if (ConfigurationManager.AppSettings[DEFAULTCONNECTIONSTRING] == null)
                throw new InvalidOperationException();

            return ConfigurationManager.AppSettings[DEFAULTCONNECTIONSTRING].ToString();
        }
    }
}
