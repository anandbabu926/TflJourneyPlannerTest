using FluentAssertions;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using TflJourneyPlannerTest.Specs.Pages;
using static Microsoft.Playwright.Assertions;

namespace TflJourneyPlannerTest.Specs.StepDefinitions;

[Binding]
public class JourneyPlannerSteps
{
    private static IPlaywright? _playwright;
    private static IBrowser? _browser;
    private IPage? _page;
    private JourneyPlannerPage? _journeyPage;

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        _page = await _browser.NewPageAsync();
        _journeyPage = new JourneyPlannerPage(_page);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
    }

    [Given(@"I am on the TfL Journey Planner page")]
    public async Task GivenIAmOnTheTflJourneyPlannerPage()
    {
        await _journeyPage!.GotoAsync();
    }

    [When(@"I type ""(.*)"" into the From field and select ""(.*)""")]
    public async Task WhenITypeIntoTheFromFieldAndSelect(string partial, string full)
    {
        await _journeyPage!.EnterFromLocation(partial, full);
    }

    [When(@"I type ""(.*)"" into the To field and select ""(.*)""")]
    public async Task WhenITypeIntoTheToFieldAndSelect(string partial, string full)
    {
        await _journeyPage!.EnterToLocation(partial, full);
    }

    [When(@"I click the Plan my Journey button")]
    public async Task WhenIClickThePlanMyJourneyButton()
    {
        await _journeyPage!.ClickPlanJourney();
    }

    [Then(@"a valid journey is planned successfully")]
    public async Task ThenAValidJourneyIsPlannedSuccessfully()
    {
        await _page!.WaitForURLAsync("**/plan-a-journey/results*");
        _page.Url.Should().Contain("results");
    }

    [Then(@"I should see valid results for both walking and cycling time")]
    public async Task ThenIShouldSeeValidResultsForBothWalkingAndCyclingTime()
    {
        var walkingSection = _page.Locator(".multi-modals");
        await Expect(walkingSection).ToBeVisibleAsync();
    }

    [Given(@"I have successfully planned a journey from ""(.*)"" to ""(.*)""")]
    public async Task GivenIHaveSuccessfullyPlannedAJourneyFromTo(string from, string to)
    {
        await _journeyPage!.GotoAsync();
        await _journeyPage.EnterFromLocation("Leicester Square", from);
        await _journeyPage.EnterToLocation("Covent Garden", to);
        await _journeyPage.ClickPlanJourney();
        await _page!.WaitForURLAsync("**/plan-a-journey/results*");
    }

    [When(@"I click on Edit preferences")]
    public async Task WhenIClickOnEditPreferences()
    {
        await _journeyPage!.ClickEditPreferences();
    }

    [When(@"I select routes with least walking")]
    public async Task WhenISelectRoutesWithLeastWalking()
    {
        await _journeyPage!.SelectLeastWalking();
    }

    [When(@"I click update journey")]
    public async Task WhenIClickUpdateJourney()
    {
        await _journeyPage!.ClickUpdateJourney();
    }

    [Then(@"the journey results should update with the new preferences and show the updated time")]
    public async Task ThenTheJourneyResultsShouldUpdate()
    {
        var preferencesText = _page!.Locator(".carousel-wrap");
        await Expect(preferencesText).ToBeVisibleAsync();
    }

    [When(@"I click on View Details for the journey")]
    public async Task WhenIClickOnViewDetailsForTheJourney()
    {
        await _journeyPage!.ClickViewDetails();
    }

    [Then(@"I should verify the complete access information icons")]
    public async Task ThenIShouldVerifyTheCompleteAccessInformationIcons()
    {
        // Verify access icons (stairs, lift, walking icons shown in challenge spec)
        var accessIconsContainer = _page!.Locator("#option-1-content .access-information").Nth(1);
        await Expect(accessIconsContainer.First).ToBeVisibleAsync();
        (await accessIconsContainer.CountAsync()).Should().BeGreaterThan(0);
    }

    [When(@"I type ""(.*)"" into the From field")]
    public async Task WhenITypeIntoTheFromField(string invalidText)
    {
        await _journeyPage!.EnterRawFromLocation(invalidText);
    }

    [When(@"I type ""(.*)"" into the To field")]
    public async Task WhenITypeIntoTheToField(string invalidText)
    {
        await _journeyPage!.EnterRawToLocation(invalidText);
    }

    [Then(@"the widget should display an error message and not provide journey results")]
    public async Task ThenTheWidgetShouldDisplayAnErrorMessage()
    {
        var infoMessageLocator = _page.Locator(".from-results .info-message");
        string messageText = await infoMessageLocator.InnerTextAsync();
        Console.WriteLine(messageText); 
    }

    [When(@"I leave the From and To fields empty")]
    public async Task WhenILeaveTheFromAndToFieldsEmpty()
    {
        // Leaving inputs blank intentionally
        await Task.CompletedTask;
    }

    [Then(@"the widget should prompt me with validation errors for the empty fields")]
    public async Task ThenTheWidgetShouldPromptMeWithValidationErrors()
    {
        var fromErrorMessages = _page!.Locator("#InputFrom-error");
        var toErrorMessages = _page!.Locator("#InputTo-error");
        string fromErrorMessagesText = await fromErrorMessages.InnerTextAsync();
        string toErrorMessagesText = await toErrorMessages.InnerTextAsync();
        Console.WriteLine(fromErrorMessagesText); 
        Console.WriteLine(toErrorMessagesText);
        (await fromErrorMessages.First.IsVisibleAsync()).Should().BeTrue();
        (await toErrorMessages.First.IsVisibleAsync()).Should().BeTrue();
    }
}