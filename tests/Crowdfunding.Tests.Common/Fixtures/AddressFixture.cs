using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Crowdfunding.Domain.Entities;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(AddressFixtureCollection))]
    public class AddressFixtureCollection : ICollectionFixture<AddressFixture>
    {
    }
    public class AddressFixture
    {
        public AddressViewModel AddressModelValid()
        {
            var address = new Faker().Address;

            var faker = new Faker<AddressViewModel>("pt_BR");

            faker.RuleFor(c => c.ZIPCODE, (f, c) => "11850-000");
            faker.RuleFor(c => c.City, (f, c) => address.City());
            faker.RuleFor(c => c.State, (f, c) => address.StateAbbr());
            faker.RuleFor(c => c.TextoEddress, (f, c) => address.StreetAddress());            

            return faker.Generate();
        }

        public Address AddressValid()
        {
            var address = new Faker("pt_BR").Address;
            
            var faker = new Faker<Address>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Address(Guid.NewGuid(), "11900-000", address.StreetAddress(false), string.Empty, address.City(), address.StateAbbr(), "16995811385", "100A"));

            return faker.Generate();
        }

        public Address AddressEmpty()
        {
            return new Address(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public Address AddressZIPCODETelephoneStateInvalid()
        {
            var address = new Faker("pt_BR").Address;

            var faker = new Faker<Address>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Address(Guid.NewGuid(), "11850-000", address.StreetAddress(false), string.Empty, address.City(), address.State(), "169958113859", "2005"));

            return faker.Generate();
        }

        public Address AddressMaxLength()
        {
            const string TEXT_WITH_MORE_THEN_250_CHARACTERES = "AHIUDHASHOIFJOASJPFPOKAPFOKPKQPOFKOPQKWPOFEMMVIMWPOVPOQWPMVPMQOPIPQMJEOIPFMOIQOIFMCOKQMEWVMOPMQEOMVOPMWQOEMVOWMEOMVOIQMOIVMQEHISUAHDUIHASIUHDIHASIUHDUIHIAUSHIDUHAIUSDQWMFMPEQPOGFMPWEMGVWEM CQPWEM,CPQWPMCEOWIMVOEWOINMMFWOIEMFOIMOIOWEMFOIEWMFOIWEMFOWEOIMF";

            var address = new Faker("pt_BR").Address;

            var faker = new Faker<Address>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Address(Guid.NewGuid(), "11900-000", TEXT_WITH_MORE_THEN_250_CHARACTERES, TEXT_WITH_MORE_THEN_250_CHARACTERES, TEXT_WITH_MORE_THEN_250_CHARACTERES, address.StateAbbr(), "16995811385", "1234567"));

            return faker.Generate();
        }

    }
}
