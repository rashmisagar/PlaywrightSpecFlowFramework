 Feature: DuckDuckGo Search Functionality
   As a user
   I want to search for items on DuckDuckGo
   So that I can see the search results

   Scenario Outline: Search for a term on DuckDuckGo
     Given the user is on the DuckDuckGo home page
     When the user searches for '<SearchTerm>'
     Then the search results show '<Result>' as the first result

     Examples:
       | SearchTerm | Result     |
       | playwright | playwright |
       | specflow   | specflow   |
    