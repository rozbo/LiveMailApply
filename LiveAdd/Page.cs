using Microsoft.Playwright;

namespace LiveAdd;

public static class Page
{
    public static async Task Dodo(this IBrowserContext @context, char c)
    {
        var page = await context.NewPageAsync();
        await page.GotoAsync("https://account.live.com/AddAssocId?ru=&cru=&fl=");
        for (var i = 0; i < 10; i++)
        {
            for (var b = 'a'; b < 'z' + 1; b++)
            {
                var txt = "" + (char)c + i.ToString() + b;
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
}