using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Crowdfunding.Tests.Common.Fixtures;
using Xunit;

namespace Crowdfunding.AutomatedUITests
{
	public class DonationTests : IDisposable, IClassFixture<DonationFixture>, 
                                               IClassFixture<AddressFixture>, 
                                               IClassFixture<CardCreditFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DonationFixture _donationFixture;
		private readonly AddressFixture _addressFixture;
		private readonly CardCreditFixture _cardCreditFixture;

		public DonationTests(DonationFixture donationFixture, AddressFixture addressFixture, CardCreditFixture cardCreditFixture)
        {
            _donationFixture = donationFixture;
            _addressFixture = addressFixture;
            _cardCreditFixture = cardCreditFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DonationUI_AccessScreenHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://Crowdfunding.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("Crowdfunding-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo displayed");
		}
		[Fact]
		public void DonationUI_CreationDonation()
		{
			//Arrange
			var donation = _donationFixture.DonationValid();
            donation.AddAddressBilling(_addressFixture.AddressValid());
            donation.AddPaymentMethod(_cardCreditFixture.CardCreditValid());
			_driverFactory.NavigateToUrl("https://Crowdfunding.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Donations/Create");
		}
	}
}