Feature: Swag lab tests

Scenario: Verify swag lab log in
	Given I launch 'Desktop Chrome' for homepage
	Then  I navigate to Swag labs
	When  I enter the username
		| userName        |
		| locked_out_user |
		| standard_user   |
	And   I enter the password 'secret_sauce'
	Then  I login to homepage