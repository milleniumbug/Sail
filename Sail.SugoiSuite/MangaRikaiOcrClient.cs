using System.Net.Http.Json;
using System.Text.Json;

namespace Sail.SugoiSuite;

public class MangaRikaiOcrClient(HttpClient client, string pathToMangaRikaiOcr)
{
    private SemaphoreSlim locker = new SemaphoreSlim(1, 1);

    public async Task<IEnumerable<Rectangle2D>> DetectTextBoxes(Stream pngFile)
    {
        try
        {
            await this.locker.WaitAsync();
            await using (var targetFile = File.Create(Path.Combine(pathToMangaRikaiOcr, "backendServer", "wholeImage.png")))
            {
                await pngFile.CopyToAsync(targetFile);
            }

            var response = await client.PostAsJsonAsync("/", new Request<string>(
                message: "detect all textboxes",
                content: "no content"));
            response.EnsureSuccessStatusCode();
            var result = JsonSerializer.Deserialize<int[][]>(await response.Content.ReadAsStreamAsync())
                         ?? throw new JsonException();
            return result.Select(rectCoordinates =>
                new Rectangle2D(
                    new Point2D(rectCoordinates[0], rectCoordinates[1]),
                    new Point2D(rectCoordinates[2], rectCoordinates[3])));
        }
        finally
        {
            this.locker.Release();
        }
    }
}