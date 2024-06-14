Feature: DuckDuckGo Search Functionality using JSON data
  As a user
  I want to search for items from data file on DuckDuckGo
  So that I can see the search results

  Scenario: Search for a term on DuckDuckGo using JSON data
    Given the user is on the DuckDuckGo home page
    When the user searches for a term from the data
    Then the search results should contain the term from the data
    And I navigate to the search result at index 0
    Then the element with selector "h1" should be present