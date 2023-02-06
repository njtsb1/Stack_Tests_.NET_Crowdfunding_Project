using System;
using Crowdfunding.Domain.Base;

namespace Crowdfunding.Domain.Entities
{
    public class Cause : Entity
    {
        private Cause() { }

        public Cause(Guid id, string name, string city, string state)
        {
            Id = id;
            Name = name;
            City = city;
            State = state;
        }

        public string Name { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public override bool Valid()
        {
            return true;
        }
    }
}
