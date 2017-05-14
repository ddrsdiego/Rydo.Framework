namespace Rydo.Framework.Data.Helper.Contracts
{
    public interface IDefinicacaoConnectionString
    {
        string ConnectionString { get; }
        string Name { get; }
        string ProviderName { get; }
    }
}
