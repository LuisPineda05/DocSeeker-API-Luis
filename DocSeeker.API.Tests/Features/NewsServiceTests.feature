Feature: NewsServiceTests
As a user in the app,
I want to read and post relevant news about healthcare
In order to be informed

	Background: 
		Given the Endpoint https://localhost:7263/api/v1/new
		
	@news-adding
	Scenario: Add an article to the news section
		When a news article is posted
		  | image         | title                  | description                             | info                  | views |
		  | img.cloud.com | Coronavirus Statistics | Chronological Events of The Coronavirus | Important Information | 55    | 
		Then the response code returned for the news article is 200
		And a News Resource is included in the Response Body
		  | image         | title                  | description                             | info                  | views |
		  | img.cloud.com | Coronavirus Statistics | Chronological Events of The Coronavirus | Important Information | 55    |