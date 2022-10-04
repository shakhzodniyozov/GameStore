using GameStore.Data.Entities;

namespace GameStore.BusinessLogic.Interfaces
{
    public interface IImageService
    {
        void DeleteGameImage(Game game);
        Task<string> SetImageToGame(Game game, string base64);
    }
}