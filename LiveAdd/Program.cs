using LiveAdd;
using Microsoft.Playwright;

using var playwright = await Playwright.CreateAsync();
var chromium = playwright.Chromium;
var context = await chromium.LaunchPersistentContextAsync(@"e:\playwright_sg\", new()
{
    Headless = false
});

var page = await context.NewPageAsync();
await page.GotoAsync("https://account.live.com/AddAssocId?ru=&cru=&fl=");
// todo: finding.
Console.ReadLine();
// 然后继续
for (var c = 'v'; c < 'z' + 1; c++)
{
    context.Dodo(c);
}

Console.ReadLine();