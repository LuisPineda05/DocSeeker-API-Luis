Feature: PatientServiceTests
As a Patient in the app,
I want to register an account
In order to be able to login and use the web application
	
    Background: 
        Given the Endpoint https://localhost:7263/api/v1/patient
		
    @patient-adding
    Scenario: Add new patient account
        When a Post Request is sent
          | firstName | lastName | dni      | password        | genre | birthday   | email             | cell1     | photo                  | area | description | patients | years | age | cost |
          | Ernesto   | Ramirez  | 92734512 | quieroamisgatos | male  | 17/12/1977 | ernesto@gmail.com | 993212755 | profilePhoto.cloud.com |      |             |          |       |     |      |
        Then the response code returned is 200
        And a Patient Resource is included in the Response Body
          | id | firstName | lastName | dni      | password        | genre | birthday   | email             | cell1     | photo                  | area | description | patients | years | age | cost |
          | 4  | Ernesto   | Ramirez  | 92734512 | quieroamisgatos | male  | 17/12/1977 | ernesto@gmail.com | 993212755 | profilePhoto.cloud.com |      |             |          |       |     |      |
          
    @patient-adding
    Scenario: Delete patient
        When a Request is sent to delete Patient with id 17
        Then the response code returned is 200
 