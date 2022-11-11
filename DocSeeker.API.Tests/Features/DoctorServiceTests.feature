Feature: DoctorServiceTests
	As a Doctor in the app,
	I want to be able to register an account
	In order to be able to login and add information

	Background: 
		Given the Endpoint https://localhost:7263/api/v1/doctor

	@doctor-adding
	Scenario: Add new doctor account 
		When a new Doctor is registered
		  | firstName | lastName | dni      | password        | genre | birthday   | email             | cell1     | photo                  | area       | description          | patients | years | age | cost |
		  | Martin    | Martinez | 92734512 | quieroamisgatos | male  | 17/12/1977 | ernesto@gmail.com | 993212755 | profilePhoto.cloud.com | San Miguel | Doctor and nutrition | 4        | 10    | 41  | 300  |
		Then the response code is 200
		And a Doctor Resource is included in the Response Body
		  | firstName | lastName | dni      | password        | genre | birthday   | email             | cell1     | photo                  | area       | description          | patients | years | age | cost |
		  | Martin    | Martinez | 92734512 | quieroamisgatos | male  | 17/12/1977 | ernesto@gmail.com | 993212755 | profilePhoto.cloud.com | San Miguel | Doctor and nutrition | 4        | 10    | 41  | 300  |