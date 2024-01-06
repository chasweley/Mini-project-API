using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    //Class for table Interests in database
    public partial class Interest
    {
        public Interest()
        {
            PersonsInterests = new HashSet<PersonsInterest>();
        }

        public int InterestId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        //One to many connection
        public virtual ICollection<PersonsInterest> PersonsInterests { get; set; }
    }
}
