namespace StationaryClash.Interfaces.Services
{
    public interface IGachaService
    {
        Task<bool> CanPullGacha(int id);
    }
}
