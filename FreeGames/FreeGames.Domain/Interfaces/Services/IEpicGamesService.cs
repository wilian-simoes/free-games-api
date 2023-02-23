namespace FreeGames.Domain.Interfaces.Services
{
    public interface IEpicGamesService
    {
        Task<string> GetFreeGamesAsync(string userId);
        Task<object> ObterJsonJogosGratisAsync();
    }
}