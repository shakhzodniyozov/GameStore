using GameStore.BusinessLogic.Interfaces;
using GameStore.Data.Entities;
using System;

namespace GameStore.BusinessLogic.Services
{
    public class ImageService : IImageService
    {
        public ImageService()
        {
            if (!Directory.Exists(GameImagesPath))
            {
                Directory.CreateDirectory(GameImagesPath);
            }
        }

        private string StaticContentPath { get => Path.Combine(Environment.CurrentDirectory, "wwwroot"); }

        private string GameImagesPath { get => Path.Combine(StaticContentPath, "gameImages"); }

        public async Task<string> SetImageToGame(Game game, string base64)
        {
            string fileExtension = ParseFileExtension(base64);
            string fileName = game.Id.ToString();

            byte[] imageBytes = Convert.FromBase64String(base64.Substring(base64.IndexOf(',') + 1));

            using (FileStream stream = new(Path.Combine(GameImagesPath, $"{fileName}.{fileExtension}"), FileMode.Create))
            {
                await stream.WriteAsync(imageBytes);
            }

            return $"gameImages/{fileName}.{fileExtension}";
        }

        public void DeleteGameImage(Game game)
        {
            File.Delete(Path.Combine(StaticContentPath, game.ImagePath));
        }

        private string ParseFileExtension(string base64)
        {
            string head = base64.Split(';')[0];
            string extension = head.Substring(base64.IndexOf('/') + 1);

            return extension;
        }
    }
}
