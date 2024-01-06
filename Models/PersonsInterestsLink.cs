using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    //Class for table PersonsInterestsLink in database where you can connect specific persons
    //specific interest to links 
    public partial class PersonsInterestsLink
    {
        public int PersonInterestLinkId { get; set; }
        public int LinkIdFk { get; set; }
        public int PersonsInterestsIdFk { get; set; }

        //Foreign key connections
        public virtual Link LinkIdFkNavigation { get; set; } = null!;
        public virtual PersonsInterest PersonsInterestsIdFkNavigation { get; set; } = null!;
    }
}
