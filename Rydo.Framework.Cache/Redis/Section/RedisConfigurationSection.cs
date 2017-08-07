using Rydo.Framework.Cache.Configuration.Contracts;
using System.Configuration;

namespace Rydo.Framework.Cache.Redis.Section
{
    public class RedisConfigurationSection : ConfigurationSection, IConfigurationSection
    {
        private const string HOSTATTRIBUTENAME = "host";
        private const string PORTATTRIBUTENAME = "port";
        private const string PASSWORDATTRIBUTENAME = "password";
        private const string DATABASEIDATTRIBUTENAME = "databaseID";

        [ConfigurationProperty(HOSTATTRIBUTENAME, IsRequired = true)]
        public string Host
        {
            get { return this[HOSTATTRIBUTENAME].ToString(); }
        }

        [ConfigurationProperty(PORTATTRIBUTENAME, IsRequired = true)]
        public int Port
        {
            get { return (int)this[PORTATTRIBUTENAME]; }
        }

        [ConfigurationProperty(PASSWORDATTRIBUTENAME, IsRequired = false)]
        public string Password
        {
            get { return this[PASSWORDATTRIBUTENAME].ToString(); }
        }

        [ConfigurationProperty(DATABASEIDATTRIBUTENAME, IsRequired = false)]
        public long DatabaseId
        {
            get { return (long)this[DATABASEIDATTRIBUTENAME]; }
        }
    }
}
