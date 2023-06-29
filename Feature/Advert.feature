Feature: Advertisement tests

Scenario: Verify available measurements
	Given I launch 'Desktop Chrome' for advertpage
	When  I navigate to website
	Then  I read the measurement headings
		| measurements |
		| Molar Mass   |
		| Size         |
		| Charge       |
		| Interactions |
		| Conformation |
		| Conjugation  |


	