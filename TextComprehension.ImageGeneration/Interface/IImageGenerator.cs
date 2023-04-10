using System.Threading.Tasks;
using TextComprehension.ImageGeneration.Models;

namespace TextComprehension.ImageGeneration.Interface
{
    public interface IImageGenerator
    {
        Task<ImageGenerationResponse> GenerateImageAsync(ImageGenerationRequest request);
    }
}