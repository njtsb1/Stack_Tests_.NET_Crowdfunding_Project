using FluentValidation;
using System;
using Crowdfunding.Domain.Base;

namespace Crowdfunding.Domain.Entities
{
    public class Donation : Entity
    {
        private Donation() { }

        public Donation(Guid id, Guid personaldataId, Guid billingaddressId, double value,
                      Person personaldata, CardCredit formPayment, Address billingaddress)
        {
            Id = id;
            DateTime = DateTime.Now;

            PersonaldataId = personaldataId;
            BillingAddressId = billingaddressId;

            Value = value;

            Personaldata = personaldata;
            PaymentMethod = formPayment;
            BillingAddress = billingaddress;
        }

        public double Value { get; private set; }

        public Guid PersonaldataId { get; private set; }
        public Guid BillingAddressId { get; private set; }

        public DateTime DateTime { get; private set; }

        public Person Personaldata { get; private set; }
        public Endereco BillingAddress { get; private set; }
        public CardCredit PaymentMethod { get; private set; }

        public void UpdateDatePurchase()
        {
            DateTime = DateTime.Now;
        }

        public void AddPerson(Person person)
        {
            Personaldata = person;
        }

        public void AddBillingAddress(Address address) {
            BillingAddress = address;
        }
        public void AddPaymentMethod(CardCredit formPayment) {
            PaymentMethod = formPayment;
        }

        public override bool Valid()
        {
            ValidationResult = new DonationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DonationValidation : AbstractValidator<Donation>
    {
        public DonationValidation()
        {
            RuleFor(a => a.Value)
                .GreaterThanOrEqualTo(1).WithMessage("Minimum donation amount is $1.00")
                .LessThanOrEqualTo(5000).WithMessage("Maximum donation amount is $5,000.00");

            RuleFor(a => a.Personaldata).NotNull().WithMessage("Personal Data is mandatory").SetValidator(new PersonValidation());
            RuleFor(a => a.BillingAddress).NotNull().WithMessage("Billing Address is mandatory.").SetValidator(new AddressValidation());
            RuleFor(a => a.PaymentMethod).NotNull().WithMessage("The Payment Method is mandatory.").SetValidator(new CardCreditValidation());
        }
    }
}