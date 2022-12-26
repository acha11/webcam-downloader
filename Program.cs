HttpClient c = new HttpClient();

try
{
    await Download("https://www.mtstirling.com.au/cam/c01_000M.jpg", "mt-stirling");
    await Download("https://s90.ipcamlive.com/streams/5al1s8lhqb9ijpnbd/snapshot.jpg", "inverloch-slsc");
}
catch (Exception ex)
{
    Console.WriteLine($"Top-level exception: {ex.ToString()}");
}

async Task Download(string url, string filenamePrefix)
{
    try
    {
        var response = await c.GetAsync(url);

        var content = await response.Content.ReadAsByteArrayAsync();

        DateTime now = DateTime.Now;

        string folderPath = Path.Combine(filenamePrefix, now.ToString("yyyy"));

        Directory.CreateDirectory(folderPath);

        await File.WriteAllBytesAsync(
            Path.Combine(
                folderPath,
                $"{filenamePrefix}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.jpg"
            ),
            content);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception while downloading {url}: {ex.ToString()}");
    }
}