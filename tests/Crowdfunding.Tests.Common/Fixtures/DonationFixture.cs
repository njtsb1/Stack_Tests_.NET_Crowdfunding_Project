using Bogus;
using Bogus.DataSets;
using System;
using Xunit;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(DonationFixtureCollection))]
    public class DonationFixtureCollection : ICollectionFixture<DonationFixture>, ICollectionFixture<AddressFixture>, ICollectionFixture<CardCreditFixture>
    {
    }

    public class DonationFixture
    {
        public DonationViewModel DonationModelValid()
        {
            var faker = new Faker<DonationViewModel>("pt_BR");

            const int MIN_VALUE = 1;
            const int MAX_VALUE = 500;
            const int DECIMALS = 2;

            faker.RuleFor(c => c.Value, (f, c) => f.Finance.Amount(MIN_VALUE, MAX_VALUE, DECIMALS));
            
            var return = faker.Generate();

            return.Personaldata = PersonModelValid();

            return return;
        }

        public Donation DonationValid(bool emailInvalid = false, double? value = 5, bool maxLenghField = false)
        {            
            var faker = new Faker<Donation>("pt_BR");

            const int MIN_VALUE = 1;
            const int MAX_VALUE = 500;
            const int DECIMALS = 2;

            faker.CustomInstantiator(f => new Donation(Guid.Empty, Guid.Empty, Guid.Empty, value ?? (double)f.Finance.Amount(MIN_VALUE, MAX_VALUE, DECIMALS), 
                                                        PersonValid(emailInvalid, maxLenghField), null, null));

            return faker.Generate();
        }

        public DonationViewModel DonationModelInvalidValid()
        {
            return new DonationViewModel();
        }

        public Donation DonationInvalid(bool donationAnonymous = false)
        {
            var personInvalid = new Person(Guid.Empty, string.Empty, string.Empty, donationAnonymous, string.Empty);
            return new Donation(Guid.Empty, Guid.Empty, Guid.Empty, 0, personInvalid, null, null );
        }

        public Person PersonValid(bool emailInvalid = false,bool maxLenghField = false)
        {            
            var person = new Faker().Person;

            var faker = new Faker<Person>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Person(Guid.NewGuid(), person.FullName, string.Empty, false, maxLenghField ? f.Lorem.Sentence(501) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_Invalid" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate();
        }

        public PersonViewModel PersonModelValid(bool emailInvalid = false)
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<PersonViewModel>("pt_BR");

            faker.RuleFor(a => a.Name, (f, c) => f.Name.FirstName(gender));
            faker.RuleFor(a => a.Email, (f, c) => emailInvalid ? "EMAIL_Invalid" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate();
        }
    }
}