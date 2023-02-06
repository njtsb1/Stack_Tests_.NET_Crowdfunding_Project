namespace Crowdfunding.Domain.ViewModels
{
    public class DonationViewModel
    {
        public decimal Value { get; set; }

        public PersonViewModel Personaldata { get; set; }
        public AddressViewModel AddressBilling { get; set; }
        public CardCreditViewModel PaymentMethod { get; set; }
    }
}
