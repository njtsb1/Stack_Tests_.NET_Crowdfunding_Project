using System;
using System.ComponentModel;

namespace Crowdfunding.Domain.ViewModels
{
    public class DonorViewModel
    {
        private string _name { get; set; }
        public string Name
        {
            get { return anonymous? "anonymous donation" : _name; }
            set { _name = value; }
        }

        [DisplayName("anonymous donation?")]
        public bool anonymous{ get; set; }

        [DisplayName("Support monthsage")]
        public string Supportmonthsage { get; set; }

        public decimal Value { get; set; }

        public DateTime DateTime { get; set; }

        [DisplayName("When?")]
        public string DescriptionTime => GenerateDescriptionTime();

        private string GenerateDescriptionTime()
        {
            var description = string.Empty;

            if (DateTime != DateTime.MinValue)
            {
                Timonthpan interval = (DateTime.Now - DateTime);

                if (interval.Days > 365)
                {
                    var year = interval.Days / 365;
                    description = year + " year";
                    if (year > 1)
                    {
                        description += "s";
                    }
                }
                else if (interval.Days > 30)
                {
                    var month = interval.Days / 30;
                    description = month + " month";
                    if (month > 1)
                    {
                        description += "es";
                    }
                }
                else if (interval.Days > 0)
                {
                    description = interval.Days + " day";
                    if (interval.Days > 1)
                    {
                        description += "s";
                    }
                }
                else if (interval.Hours > 0)
                {
                    description = interval.Hours + " hour";
                    if (interval.Hours > 1)
                    {
                        description += "s";
                    }
                }
                else if (interval.Minutes > 0)
                {
                    description = interval.Minutes + " minute";
                    if (interval.Minutes > 1)
                    {
                        description += "s";
                    }
                }
                else
                {
                    return "this instant";
                }

                description += " back";
            }

            return description;
        }
    }
}
