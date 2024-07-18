using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class BackgroundRemover
{
    private readonly string _endpoint;
    private readonly string _key;
    private readonly ILogger<BackgroundRemover> _logger;

    public BackgroundRemover(IConfiguration configuration, ILogger<BackgroundRemover> logger)
    {
        _endpoint = configuration["Azure:Endpoint"];
        _key = configuration["Azure:Key"];
        _logger = logger;
    }

    public async Task RemoveBackgroundAsync(string imagePath)
    {
        const string apiVersion = "2023-02-01-preview";
        const string mode = "backgroundRemoval";

        var url = $"{_endpoint}computervision/imageanalysis:segment?api-version={apiVersion}&mode={mode}";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

        _logger.LogInformation("Removing background from image...");

        var imageData = await File.ReadAllBytesAsync(imagePath);
        var content = new ByteArrayContent(imageData);

        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var image = await response.Content.ReadAsByteArrayAsync();
            var outputPath = Path.Combine(Path.GetDirectoryName(imagePath), $"{Path.GetFileNameWithoutExtension(imagePath)}_no_bg{Path.GetExtension(imagePath)}");
            await File.WriteAllBytesAsync(outputPath, image);
            _logger.LogInformation($"Results saved in {outputPath}");
        }
        else
        {
            _logger.LogError($"Background removal failed: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }
}
