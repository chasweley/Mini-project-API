using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    public partial class PersonsInterest
    {
        public PersonsInterest()
        {
            PersonsInterestsLinks = new HashSet<PersonsInterestsLink>();
        }

        public int PersonInterestId { get; set; }
        public int PersonIdFk { get; set; }
        public int InterestIdFk { get; set; }

        public virtual Interest InterestIdFkNavigation { get; set; } = null!;
        public virtual Person PersonIdFkNavigation { get; set; } = null!;
        public virtual ICollection<PersonsInterestsLink> PersonsInterestsLinks { get; set; }
    }
}
