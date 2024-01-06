using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    //Class for table PersonsInterests in database where you can connect
    //specific persons to interests
    public partial class PersonsInterest
    {
        public PersonsInterest()
        {
            PersonsInterestsLinks = new HashSet<PersonsInterestsLink>();
        }

        public int PersonInterestId { get; set; }
        public int PersonIdFk { get; set; }
        public int InterestIdFk { get; set; }

        //Foreign key connections
        public virtual Interest InterestIdFkNavigation { get; set; } = null!;
        public virtual Person PersonIdFkNavigation { get; set; } = null!;
        
        //One to many connection
        public virtual ICollection<PersonsInterestsLink> PersonsInterestsLinks { get; set; }
    }
}
