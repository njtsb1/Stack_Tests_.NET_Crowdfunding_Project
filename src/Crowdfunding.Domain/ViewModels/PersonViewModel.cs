using System.ComponentModel;

namespace Crowdfunding.Domain.ViewModels
{
    public class PersonViewModel
    {
        private string _name { get; set; }
        public string Name
        {
            get { return Anonymous ? "Anonymous donation" : _name; }
            set { _name = value; }
        }

        public string Email { get; set; }

        [DisplayName("Anonymous donation")]
        public bool Anonymous { get; set; }

        public string MessageSupport { get; set; }
    }
}
