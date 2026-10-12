# TfL Journey Planner Test Automation Framework

An automated UI test suite built for Transport for London's (TfL) **Journey Planner** widget (`https://tfl.gov.uk/plan-a-journey`), developed as part of the Senior Test Analyst coding challenge.

---

## 🚀 Introduction

This framework is built using **C# (.NET)**, **Playwright for .NET** (Chromium driver) for fast and reliable browser automation, **SpecFlow/Reqnroll** for BDD Gherkin syntax execution, **NUnit** as the test runner, and **FluentAssertions** for expressive assertions. It implements the **Page Object Model (POM)** design pattern to ensure high maintainability, clear separation of concerns, and reusable component logic.

---

## 📋 Prerequisites & Requirements

Before running the test suite, ensure you have the following installed on your development machine:
1. **.NET SDK** (.NET 8.0 or higher recommended). Verify installation via terminal:
   ```bash
   dotnet --version
   ```
2. **Visual Studio Code** (or Visual Studio Community) with the C# and C# Dev Kit extensions.
3. **Playwright Browser Binaries** (automatically managed, but browser drivers can be explicitly installed via):
   ```bash
   dotnet build
   pwsh bin/Debug/netX.X/playwright.ps1 install
   ```

---

## ⚙️ How to Build and Run the Tests

1. Clone or download the repository to your local machine.
2. Open your terminal in the root solution directory (`TflJourneyPlannerTest`).
3. Restore dependencies and build the solution:
   ```bash
   dotnet build
   ```
4. Execute the test suite:
   ```bash
   dotnet test
   ```
   *(To run a specific tagged scenario, use test filters such as `dotnet test --filter "TestCategory=scenario1"`).*

---

## 🧪 Implemented Automation Scenarios (Gherkin Syntax)

The framework covers the core mandatory test scenarios requested in the challenge:
1. **Valid Journey & Auto-Complete:** Verifies planning a journey from "Leicester Square Underground Station" to "Covent Garden Underground Station" using auto-complete dropdown selections, validating both walking and cycling times.
2. **Preference Customization:** Edits journey preferences to select *least walking*, updates the journey, and validates results against modified criteria.
3. **Station Accessibility Inspection:** Expands journey details to verify complete access information icons (stairs, lifts, walking pathways) at Covent Garden Underground Station.
4. **Invalid Locations Handling:** Verifies that entering invalid stations throws appropriate field validation errors and prevents incorrect results.
5. **Empty Fields Validation:** Verifies that submitting blank input fields prompts required location validation warnings.

---

## 🔍 Additional Scenarios for Future Enhancement

As a public-facing commuter component, the widget would benefit from the inclusion of the following additional test cases:

### Functional Enhancements:
* **Future Date & Time Planning:** Verify journey calculations when selecting specific future dates and off-peak/peak hours (e.g., next Tuesday at 08:30 AM).
* **Saved Favourites ("My Journeys" & "Recents"):** Test saving a planned route to favorites and verifying its quick-access persistence across browser sessions.
* **Accessibility Filters:** Validate route adjustments when selecting step-free access options (e.g., wheelchair accessibility or step-free to train).

### Non-Functional Enhancements:
* **Performance / Load Testing:** Measure widget response times and auto-complete dropdown latency under simulated high concurrency during peak morning commuter hours (07:30 - 09:00).
* **Accessibility (a11y) Auditing:** Automated screen-reader compatibility (ARIA labels) and full keyboard-only navigation (Tab/Enter keyflows) across all input components.
* **Cross-Browser & Responsive Testing:** Validate layout integrity, touch-target sizing, and auto-complete behavior across mobile viewports (iOS Safari, Android Chrome) as well as desktop browsers.

---

## 🤖 Disclaimer on the Use of AI

*Responsible Use of AI Disclosure:* Artificial intelligence tools (such as large language models) were utilized during the development of this framework to assist with structuring code syntax, troubleshooting Playwright asynchronous promise resolutions, and drafting the project documentation and README structure. All test scripts, locators, and assertions have been manually reviewed, refined, and validated against the live TfL website.