namespace Rydo.Framework.Cache.Configuration.Contracts
{
    public interface IConfigurationSection
    {
        string Host { get; }
        int Port { get; }
        string Password { get; }
        long DatabaseId { get; }
    }
}
