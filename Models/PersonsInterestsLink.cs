using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    public partial class PersonsInterestsLink
    {
        public int PersonInterestLinkId { get; set; }
        public int LinkIdFk { get; set; }
        public int PersonsInterestsIdFk { get; set; }

        public virtual Link LinkIdFkNavigation { get; set; } = null!;
        //public virtual ICollection<Link> Links { get; set; }
        public virtual PersonsInterest PersonsInterestsIdFkNavigation { get; set; } = null!;
    }
}
