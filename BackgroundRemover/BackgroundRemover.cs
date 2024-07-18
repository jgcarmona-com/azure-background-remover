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
        try
        {
            const string apiVersion = "2023-02-01-preview";
            const string mode = "backgroundRemoval";

            var url = $"{_endpoint}computervision/imageanalysis:segment?api-version={apiVersion}&mode={mode}";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

            _logger.LogInformation($"Processing image: {imagePath}");
            Console.WriteLine($"Processing image: {imagePath}");

            using var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var image = await response.Content.ReadAsByteArrayAsync();
                var outputPath = Path.Combine(Path.GetDirectoryName(imagePath), $"{Path.GetFileNameWithoutExtension(imagePath)}_no_bg{Path.GetExtension(imagePath)}");
                await File.WriteAllBytesAsync(outputPath, image);
                _logger.LogInformation($"Background removed: {outputPath}");
                Console.WriteLine($"Background removed: {outputPath}");
            }
            else
            {
                _logger.LogError($"Failed to remove background: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError($"Access denied to the path: {imagePath}. Exception: {ex.Message}");
        }
        catch (IOException ex)
        {
            _logger.LogError($"I/O error when accessing the path: {imagePath}. Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred: {ex.Message}");
        }
    }

    public async Task RemoveBackgroundFromFolderAsync(string folderPath)
    {
        try
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles();

            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    fileInfo.Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                    fileInfo.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                    fileInfo.Extension.Equals(".bmp", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation($"Processing image: {fileInfo.FullName}");
                    await RemoveBackgroundAsync(fileInfo.FullName);
                }
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError($"Access denied to the folder: {folderPath}. Exception: {ex.Message}");
        }
        catch (IOException ex)
        {
            _logger.LogError($"I/O error when accessing the folder: {folderPath}. Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred: {ex.Message}");
        }
    }
}
