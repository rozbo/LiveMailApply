using Microsoft.Playwright;

namespace LiveAdd;

public static class Page
{
    public static SemaphoreSlim sem = new SemaphoreSlim(4);

    public static async Task Dodo(this IBrowserContext @context, char c)
    {
        try
        {
            await Page.sem.WaitAsync();
            var dict = new List<string>();
            for (var i = 'a'; i < 'z' + 1; i++)
            {
                dict.Add(i + "");
            }

            // 0.0
            for (var i = '0'; i < '9' + 1; i++)
            {
                dict.Add(i + "");
            }

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://account.live.com/AddAssocId?ru=&cru=&fl=");
            {
                foreach (var b in dict)
                {
                    var txt = "" + (char)c + b;
                    Console.WriteLine("try:" + txt);
                    var count = await page.Locator("#AssociatedIdLive").CountAsync();
                    if (count == 0)
                    {
                        await page.ReloadAsync();
                        Console.WriteLine("error:reload");
                    }

                    await page.Locator("#AssociatedIdLive").FillAsync(txt);
                    await page.Locator("#SubmitYes").ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                }
            }
        }
        finally
        {
            sem.Release();
        }
    }
}