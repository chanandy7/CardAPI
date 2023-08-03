

using Microsoft.AspNetCore.Mvc;


namespace CardAPI.Controllers;

[ApiController]
[Route("api/home")]
public class HomeController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file)
    {
        string url = "https://api.openai.com/v1/audio/transcriptions";
        string model = "whisper-1";

        // Create the request body as a multipart form
        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
        formData.Add(new StringContent(model), "model");

        // Set the authorization header
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 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");

        // Send the POST request
        HttpResponseMessage response = await _httpClient.PostAsync(url, formData);

        // Check if the request was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the response content
            string responseContent = await response.Content.ReadAsStringAsync();
            return Ok(responseContent);
        }
        else
        {
            return BadRequest("Request failed with status code: " + response.StatusCode);
        }
    }
}