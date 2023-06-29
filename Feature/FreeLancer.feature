Feature: FreeLancer
	Test Freelancer site

Scenario: Verify Freelancer search engine
	Given I launch 'Desktop Chrome' for freelancer page
	When  I navigate to freelancer website
	Then  I verify the search engine

Scenario: Verify hiring of freelancers
	Given I launch 'Desktop Chrome' for freelancer page
	When  I navigate to freelancer website
	Then  I hire a freelancer