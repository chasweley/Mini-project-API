using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    public partial class Interest
    {
        public Interest()
        {
            PersonsInterests = new HashSet<PersonsInterest>();
        }

        public int InterestId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<PersonsInterest> PersonsInterests { get; set; }
    }
}
