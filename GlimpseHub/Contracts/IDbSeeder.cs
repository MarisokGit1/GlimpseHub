
namespace GlimpseHub.Contracts
{
    public interface IDbSeeder
    {
        Task<bool> DBHasDataAsync();
        Task SeedAllDataAsync();
    }
}
