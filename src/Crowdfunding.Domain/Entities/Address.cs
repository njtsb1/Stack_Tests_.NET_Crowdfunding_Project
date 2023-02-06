using FluentValidation;
using System;
using System.Collections.Generic;
using Crowdfunding.Domain.Base;

namespace Crowdfunding.Domain.Entities
{
    public class Address : Entity
    {
        public Address() { }

        public Address(Guid id, string zipecode, string textAddress, string complement, string city, string state, string telephone, string number)
        {
            Id = id;
            ZipeCode = zipecode;
            TextAddress = textAddress;
            Complement = complement;
            City = city;
            State = state;
            Telephone = telephone;
            Number = number;
        }
        
        public string ZipeCode { get; private set; }
        public string TextAddress { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Telephone { get; private set; }

        public ICollection<Donation> Donations { get; set; }

        public override bool Valid()
        {
            ValidationResult = new AddressValidation().Validate( this);
            return ValidationResult.IsValid;
        }
    }

    public class AddressValidation : AbstractValidator<Address>
    {
        private const int MAX_LENGHT_ADDRESS = 250;
        private const int MAX_LENGHT_COMPLEMENT = 250;
        private const int MAX_LENGHT_CITY = 150;
        private const int MAX_LENGHT_NUMBER = 6;

        public AddressValidation()
        {
            RuleFor(o => o.ZipeCode)
                .NotEmpty().WithMessage("The zip code field must be filled in")
                .Must(ValidateCep).WithMessage("Invalid zip code field");

            RuleFor(o => o.TextAddress)
                .NotEmpty().WithMessage("The Address field must be filled in")
                .MaximumLength(MAX_LENGHT_ADDRESS).WithMessage($"The Address field must have at most {MAX_LENGHT_ADDRESS} characters");

            RuleFor(o => o.Number)
                .NotEmpty().WithMessage("The Number field must be filled in")
                .MaximumLength(MAX_LENGHT_NUMBER).WithMessage($"The Number field must have at most {MAX_LENGHT_NUMBER} characters");

            RuleFor(o => o.City)
                .NotEmpty().WithMessage("City field must be filled")
                .MaximumLength(MAX_LENGHT_CITY).WithMessage($"The City field must have at most {MAX_LENGHT_CITY} characters");

            RuleFor(o => o.State)                
                .Length(2).WithMessage("Invalid State field");

            RuleFor(o => o.Telephone)
                .NotEmpty().WithMessage("The Telephone field must be filled in")
                .Must(ValidateTelephone).WithMessage("Invalid telephone field");

            RuleFor(o => o.Complement)
                .MaximumLength(MAX_LENGHT_COMPLEMENT).WithMessage($"The Complement field must have the maximum {MAX_LENGHT_COMPLEMENT} characters");
        }

        private bool ValidateZipeCode(string zipecode)
        {
            if (string.IsNullOrEmpty(zipecode)) return true;

            return zipecode.Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Length == 8;
        }

        private bool ValidateTelephone(string telephone)
        {
            if (string.IsNullOrEmpty(telephone)) return true;

            var size = telephone.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty)
                .Replace("-", string.Empty).Length;

            return size >= 10 && size <= 11;
        }
    }
}