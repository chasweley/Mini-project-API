using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    public partial class Link
    {
        public Link()
        {
            PersonsInterestsLinks = new HashSet<PersonsInterestsLink>();
        }

        public int LinkId { get; set; }
        public string LinkToPage { get; set; } = null!;

        public virtual ICollection<PersonsInterestsLink> PersonsInterestsLinks { get; set; }
    }
}
