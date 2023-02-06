using FluentValidation;
using System;
using System.Collections.Generic;
using Crowdfunding.Domain.Base;

namespace Crowdfunding.Domain.Entities
{
    public class Person : Entity
    {
        public Person() { }

        public Person(Guid id, string name, string email, bool anonymous, string messageSupport)
        {
            Id = id;
            _name = name;
            Email = email;
            Anonymous = anonymous;
            MessageSupport = messageSupport;
        }

        private string _name;

        public string Name
        {
            get { return Anonymous ? Email : _name; }
            private set { _name = value; }
        }

        public bool Anonymous { get; private set; }
        public string MessageSupport { get; private set; }

        public string Email { get; private set; }
        public ICollection<Donation> Donations { get; set; }

        public override bool Valid()
        {
            ValidationResult = new PersonValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PersonValidation : AbstractValidator<Person>
    {
        private const int MAX_LENGTH_FIELDS = 150;
        private const int MAX_LENGTH_MASSAGE = 500;

        public PersonValidation()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Name field is mandatory.")
                .When(a => a.Anonymous == false)
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage("The Name field must have a maximum of 150 characters.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("Email field is required.")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"The Email field must have at most {MAX_LENGTH_FIELDS} characters.");

            RuleFor(a => a.Email).EmailAddress()
                .When(a => !string.IsNullOrEmpty(a.Email))
                .When(a => a?.Email?.Length <= MAX_LENGTH_FIELDS)
                .WithMessage("Email field is invalid.");

            RuleFor(a => a.MessageSupport)                
                .MaximumLength(MAX_LENGTH_MASSAGE).WithMessage($"The Support Message field must have a maximum of {MAX_LENGTH_MASSAGE} characters.");
        }
    }
}
