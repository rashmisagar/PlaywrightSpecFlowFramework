Feature: DuckDuckGo Search Functionality using Database
  As a user
  I want to search for items from data file on DuckDuckGo
  So that I can see the search results

  Scenario: Search for a random term from the database and verify the results
    Given the user is on the DuckDuckGo home page
    When the user searches for a random term from the database
    Then the search results show the random term from the database as the first result