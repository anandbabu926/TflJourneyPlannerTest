Feature: TfL Journey Planner Widget
  As a commuter
  I want to plan and customize my journeys using the TfL widget
  So that I can verify routes, travel times, and station accessibility

  Background:
    Given I am on the TfL Journey Planner page

    @scenario1
  Scenario: 1. Plan a valid journey using auto-complete and validate walking and cycling times
    When I type "Leice" into the From field and select "Leicester Square Underground Station"
    And I type "Coven" into the To field and select "Covent Garden Underground Station"
    And I click the Plan my Journey button
    Then a valid journey is planned successfully
    And I should see valid results for both walking and cycling time

    @scenario2
  Scenario: 2. Edit preferences to select least walking and update journey
    Given I have successfully planned a journey from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    When I click on Edit preferences
    And I select routes with least walking
    And I click update journey
    Then the journey results should update with the new preferences and show the updated time

    @scenario3
  Scenario: 3. Verify complete access information at Covent Garden station
    Given I have successfully planned a journey from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    When I click on Edit preferences
    And I select routes with least walking
    And I click update journey
    When I click on View Details for the journey
    Then I should verify the complete access information icons

    @scenario4
  Scenario: 4. Verify widget does not provide results for an invalid journey
    When I type "Unknown1" into the From field
    And I type "Unknown2" into the To field
    And I click the Plan my Journey button
    Then the widget should display an error message and not provide journey results

    @scenario5
  Scenario: 5. Verify widget cannot plan a journey with empty location fields
    When I leave the From and To fields empty
    And I click the Plan my Journey button
    Then the widget should prompt me with validation errors for the empty fields