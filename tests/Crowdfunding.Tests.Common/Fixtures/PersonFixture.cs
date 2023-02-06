using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using Xunit;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(PersonFixtureCollection))]
    public class PersonFixtureCollection : ICollectionFixture<PersonFixture>
    {
    }

    public class PersonFixture
    {
        public Person PersonModelValid(bool emailInvalid = false)
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.RuleFor(c => c.MessageSupport, (f, c) => f.Lorem.Sentence(30));
            faker.RuleFor(c => c.Name, (f, c) => f.Name.FirstName(gender));
            faker.RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_INValid" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate();
        }

        public IEnumerable<Person> PersonModelValid(int qtd, bool emailInvalid = false)
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.RuleFor(c => c.Name, (f, c) => f.Name.FirstName(gender));
            faker.RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_INValid" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate(qtd);
        }

        public IEnumerable<Person> PersonValid(int qtd, bool emailInvalid = false, bool maxLenghField = false)
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Person(Guid.NewGuid(), f.Name.FirstName(gender), string.Empty, false, maxLenghField ? f.Lorem.Sentence(300) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_INValid" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate(qtd);
        }

        public Person PersonValid(bool emailInvalid = false, bool maxLenghField = false)
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var faker = new Faker<Person>("pt_BR");

            faker.CustomInstantiator(f =>
                 new Person(Guid.NewGuid(), f.Name.FirstName(gender), string.Empty, false, maxLenghField ? f.Lorem.Sentence(300) : f.Lorem.Sentence(30)))
                .RuleFor(c => c.Email, (f, c) => emailInvalid ? "EMAIL_INValid" : f.Internet.Email(c.Name.ToLower(), c.Name.ToLower()));

            return faker.Generate();
        }

        public Person PersonVazia()
        {
            return new Person(Guid.Empty, string.Empty, string.Empty, false, string.Empty);
        }

        public Person PersonMaxLenth()
        {
            const string TEXT_WITH_MORE_THEN_150_CHARACTERES = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            return new Person(Guid.NewGuid(), TEXT_WITH_MORE_THEN_150_CHARACTERES, TEXT_WITH_MORE_THEN_150_CHARACTERES, false, "AA");
        }
    }
}