using Bogus;
using Xunit;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(CardCreditFixtureCollection))]
    public class CardCreditFixtureCollection : ICollectionFixture<CardCreditFixture>
    {
    }
    public class CardCreditFixture
    {
        public CardCreditViewModel CardCreditModelValid()
        {
            var cardCredit = new Faker().Finance;

            var faker = new Faker<CardCreditViewModel>("pt_BR");

            faker.RuleFor(c => c.CVV, (f, c) => cardCredit.CreditCardCvv());
            faker.RuleFor(c => c.NameHolder, (f, c) => f.Person.FullName);
            faker.RuleFor(c => c.NumberCardCredit, (f, c) => cardCredit.CreditCardNumber());
            faker.RuleFor(c => c.Validity, (f, c) => "12/29");

            return faker.Generate();
        }

        public CardCredit CardCreditValid()
        {
            var cardCredit = new Faker("pt_BR").Finance;
            var person = new Faker("pt_BR").Person;

            var faker = new Faker<CardCredit>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CardCredit(person.FullName, cardCredit.CreditCardNumber(), "12/29", cardCredit.CreditCardCvv()));

            return faker.Generate();
        }

        public CardCredit CardCreditEmpty()
        {
            return new CardCredit(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public CardCredit CardCreditNumberValidityCVVInvalid()
        {
            var cardCredit = new Faker("pt_BR").Finance;
            var person = new Faker("pt_BR").Person;

            var faker = new Faker<CardCredit>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CardCredit(person.FullName, "21125684", "12/28", "312q"));

            return faker.Generate();
        }

        public CardCredit CardCreditValidityExpirada()
        {
            var cardCredit = new Faker("pt_BR").Finance;
            var person = new Faker("pt_BR").Person;

            var faker = new Faker<CardCredit>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CardCredit(person.FullName, cardCredit.CreditCardNumber(), "06/19", cardCredit.CreditCardCvv()));

            return faker.Generate();
        }

        public CardCredit CardCreditNameHolderMaxLengthInvalid()
        {
            const string TEXT_WITH_MORE_THEN_150_CHARACTERES = "AHIUDHASHOIFJOASJPFPOKAPFOKPKQPOFKOPQKWPOFEMMVIMWPOVPOQWPMVPMQOPIPQMJEOIPFMOIQOIFMCOKQMEWVMOPMQEOMVOPMWQOEMVOWMEOMVOIQMOIVMQEHISUAHDUIHASIUHDIHASIUHDUIHIAUSHIDUHAIUSDQWMFMPEQPOGFMPWEMGVWEM";
            var cardCredit = new Faker("pt_BR").Finance;

            var faker = new Faker<CardCredit>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CardCredit(TEXT_WITH_MORE_THEN_150_CHARACTERES, cardCredit.CreditCardNumber(), "12/29", cardCredit.CreditCardCvv()));

            return faker.Generate();
        }

    }
}
