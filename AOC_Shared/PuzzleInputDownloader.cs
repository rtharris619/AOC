namespace AOC_Shared;

public class PuzzleInputDownloader
{
    private string sessionCookie = string.Empty;

    public PuzzleInputDownloader()
    {
        GetSessionCookie();
    }

    private void GetSessionCookie()
    {
        EnvReader.Load(".env");
        sessionCookie = Environment.GetEnvironmentVariable("SESSION_COOKIE_SECRET") ?? string.Empty;
    }

    public async Task<string> DownloadPuzzleInputAsync(int year, int day)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");
        var response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
