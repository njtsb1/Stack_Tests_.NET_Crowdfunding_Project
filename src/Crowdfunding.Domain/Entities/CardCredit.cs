using FluentValidation;
using System;
using System.Globalization;
using Crowdfunding.Domain.Base;

namespace Crowdfunding.Domain.Entities
{
    public class CardCredit : Entity
    {
        private CardCredit() { }

        public CardCredit(string nameholder, string number, string validity, string cvv)
        {
            NameHolder = nameholder;
            NumberCardCredit = number;
            Validity = validity;
            CVV = cvv;
        }

        public string NameHolder { get; private set; }
        public string NumberCardCredit { get; private set; }
        public string Validity { get; private set; }
        public string CVV { get; private set; }

        public override bool Valid()
        {
            ValidationResult = new CardCreditValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CardCreditValidation : AbstractValidator<CardCredit>
    {
        private const int MAX_LENGTH_FIELDS = 150;

        public CardCreditValidation()
        {
            RuleFor(o => o.NameHolder)
              .NotEmpty().WithMessage("The Name field must be filled in")
              .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"The Name Holder field must have a maximum of {MAX_LENGTH_FIELDS} characters");

            RuleFor(o => o.NumberCardCredit)
                .NotEmpty().WithMessage("The Credit Card Number field must be filled in")
                .CreditCard().WithMessage("Invalid credit card number field");

            RuleFor(o => o.CVV)
                .NotEmpty().WithMessage("The CVV field must be completed")
                .Must(ValidateCVV)
                .WithMessage("Invalid CVV field");

            RuleFor(o => o.Validity)
                .NotEmpty().WithMessage("The Validity field must be filled in")
                .Must(v => ValidateExpirationField(v, out _)).WithMessage("Invalid credit card expiration date field")
                .Must(v => ValidateExpirationDate(v)).WithMessage("Expired credit card");
        }

        private bool ValidateCVV(string cvv)
        {
            if (string.IsNullOrEmpty(cvv)) return true;

            return cvv.Length >= 3 && cvv.Length <= 4 && int.TryParse(cvv, out _);
        }

        private bool ValidateExpirationField(string validity, out DateTime? date)
        {
            date = null;

            if (string.IsNullOrEmpty(validity)) return true;

            string[] monthYear = validity.Split("/");

            if (monthYear.Length == 2)
            {
                if (monthYear[0].Length <= 2 && monthYear[1].Length <= 4)
                {
                    int month, year;
                    if (int.TryParse(monthYear[0], out month) && int.TryParse(monthYear[1], out year))
                    {
                        year = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(year);
                        if (month >= 1 && month <= 12 && year >= 2000 && year <= 2099)
                        {
                            date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool ValidateExpirationDate(string validity)
        {
            if (ValidateExpirationField(validity, out DateTime? ExpirationDate) && ExpirationDate != null)
            {
                return DateTime.Now.Date <= ((DateTime)ExpirationDate).Date;
            }

            return true;
        }
    }
}