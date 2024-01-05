using System;
using System.Collections.Generic;

namespace Mini_project_API.Models
{
    public partial class Person
    {
        public Person()
        {
            PersonsInterests = new HashSet<PersonsInterest>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNo { get; set; }

        public virtual ICollection<PersonsInterest> PersonsInterests { get; set; }
    }
}
