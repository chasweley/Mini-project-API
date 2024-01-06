using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    //Class for table Links in database
    public partial class Link
    {
        public Link()
        {
            PersonsInterestsLinks = new HashSet<PersonsInterestsLink>();
        }

        public int LinkId { get; set; }
        public string LinkToPage { get; set; } = null!;

        //One to many connection
        public virtual ICollection<PersonsInterestsLink> PersonsInterestsLinks { get; set; }
    }
}
