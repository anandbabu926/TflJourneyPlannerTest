using Microsoft.Playwright;

namespace TflJourneyPlannerTest.Specs.Pages;

public class JourneyPlannerPage
{
    private readonly IPage _page;

    public JourneyPlannerPage(IPage page)
    {
        _page = page;
    }

    // Locators
    private ILocator FromInput => _page.Locator("#InputFrom");
    private ILocator ToInput => _page.Locator("#InputTo");
    private ILocator PlanJourneyButton => _page.Locator("#plan-journey-button");
    private ILocator EditPreferencesToggle => _page.Locator(".edit-preferences");
    private ILocator LeastWalkingRadio => _page.Locator("label[for='JourneyPreference_2']");
    private ILocator UpdateJourneyButton => _page.Locator("input[value='Update journey']").Nth(1);
    private ILocator ViewDetailsButton => _page.Locator("#option-1-content").Locator(".view-hide-details").First;
    private ILocator AccessIcons => _page.Locator(".access-information a");

    // Actions
    public async Task GotoAsync()
    {
        // Use DOMContentLoaded instead of waiting for all background trackers to finish
        await _page.GotoAsync("https://tfl.gov.uk/plan-a-journey", new PageGotoOptions 
        { 
            WaitUntil = WaitUntilState.DOMContentLoaded, 
            Timeout = 30000 
        });
 
        // Handle cookie banner if it appears
        var cookieButton = _page.Locator("#CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
        try
        {
            // Wait up to 5 seconds for the cookie banner to render and become visible
            await cookieButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await cookieButton.ClickAsync();
            
            // Wait briefly for the overlay to fully disappear from the DOM/foreground
            await _page.WaitForTimeoutAsync(500);
        }
        catch
        {
            // If the cookie banner didn't pop up, catch the timeout and continue safely
        }

        // Explicitly wait for the input field to be visible, ensuring the widget is interactive
        await FromInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
    }

    // public async Task EnterFromLocation(string partialText, string fullStationName)
    // {
    //     await FromInput.FillAsync(partialText);
    //     // Wait for auto-complete options to load and select the matching one
    //     var option = _page.Locator($".ui-menu-item-wrapper:text('{fullStationName}')");
    //     await option.WaitForAsync();
    //     await option.ClickAsync();
    // }

    // public async Task EnterToLocation(string partialText, string fullStationName)
    // {
    //     await ToInput.FillAsync(partialText);
    //     var option = _page.Locator($".ui-menu-item-wrapper:text('{fullStationName}')");
    //     await option.WaitForAsync();
    //     await option.ClickAsync();
    // }
    public async Task EnterFromLocation(string partialText, string fullStationName)
    {
        await FromInput.ClickAsync();
        await FromInput.FillAsync(partialText);
        
        // Wait for the auto-complete dropdown option matching the station name to appear and click it
        var suggestion = _page.Locator($"text={fullStationName}").First;
        await suggestion.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await suggestion.ClickAsync();
    }

    public async Task EnterToLocation(string partialText, string fullStationName)
    {
        await ToInput.ClickAsync();
        await ToInput.FillAsync(partialText);
        
        // Wait for the auto-complete dropdown option matching the station name to appear and click it
        var suggestion = _page.Locator($"text={fullStationName}").First;
        await suggestion.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await suggestion.ClickAsync();
    }

    public async Task EnterRawFromLocation(string text)
    {
        await FromInput.FillAsync(text);
    }

    public async Task EnterRawToLocation(string text)
    {
        await ToInput.FillAsync(text);
    }

    public async Task ClickPlanJourney()
    {
        await PlanJourneyButton.ClickAsync();
    }

    public async Task ClickEditPreferences()
    {
        await EditPreferencesToggle.ClickAsync();
    }

    public async Task SelectLeastWalking()
    {
        await LeastWalkingRadio.ClickAsync();
    }

    public async Task ClickUpdateJourney()
    {
        // Click the update preferences button
        await UpdateJourneyButton.ClickAsync();
    }

    public async Task ClickViewDetails()
    {
        await ViewDetailsButton.ClickAsync();
    }
}