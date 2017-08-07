using Rydo.Framework.Cache.Configuration.Contracts;
using Rydo.Framework.Cache.Redis.Section;
using System.Configuration;

namespace Rydo.Framework.Cache.Redis.Manager
{
    public class RedisConfigurationManager : IConfigurationManager
    {
        private const string SECTIONNAME = "RedisConfiguration";

        private static IConfigurationManager _instance;
        public static IConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RedisConfigurationManager();
                }
                return _instance;
            }
        }

        public IConfigurationSection Config
        {
            get
            {
                return (RedisConfigurationSection)ConfigurationManager.GetSection(SECTIONNAME);
            }
        }
    }
}
