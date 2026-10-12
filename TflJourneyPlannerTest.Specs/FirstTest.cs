using Microsoft.Playwright;
using NUnit.Framework;
using FluentAssertions;

namespace TflJourneyPlannerTest.Specs;

public class FirstTest
{
    [Test]
    public async Task CanOpenTflWebsite()
    {
        // Initialize Playwright
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var page = await browser.NewPageAsync();

        // Go to TfL Plan a Journey page
        await page.GotoAsync("https://tfl.gov.uk/plan-a-journey");

        // Verify the title contains "Plan a journey"
        var title = await page.TitleAsync();
        title.Should().Contain("Plan a journey");
    }
}